using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Seeker.Gamebook;

namespace Seeker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Gamebooks();
        }

        public void Gamebooks(bool toMain = false)
        {
            Game.Data.Clean();
            PageClean();

            MainScroll.BackgroundColor = Output.Constants.BACKGROUND;

            Text.Children.Add(Output.Interface.Text("Выберите книгу:", defaultParams: true));

            Game.Data.Actions = null;

            foreach (string game in List.GetBooks())
            {
                Description gamebook = List.GetDescription(game);

                Options.Children.Add(Output.Interface.GamebookImage(gamebook));
                Options.Children.Add(Output.Buttons.GamebookButton(gamebook, Gamebook_Click));

                Output.Interface.GamebookDisclaimerAdd(gamebook, ref Options);
            }

            Output.Interface.Footer(ref Footer, Settings_Click);

            UpdateStatus();

            if (toMain)
                MainScroll.ScrollToAsync(MainScroll, ScrollToPosition.Start, true);
        }

        private void Gamebook_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            string gamebook = List.GetBooksNameByTitle(button.Text);

            Game.Continue.CurrentGame(gamebook);
            Game.Xml.GameLoad(gamebook);
            GamepageSettings();

            Paragraph(0);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            PageClean();

            Output.Settings.Add(ref Action);
            Options.Children.Add(Output.Buttons.CloseSettings((s, args) => Gamebooks(toMain: true)));
        }

        public void Paragraph(int id, bool reload = false, bool loadGame = false, string optionName = "")
        {
            bool startOfGame = (id == Game.Data.StartParagraph);

            Game.Router.Clean();
            PageClean();

            if (startOfGame)
                Game.Data.Protagonist();

            Game.Paragraph paragraph = null;

            if ((Game.Data.CurrentParagraphID != id) || startOfGame || loadGame)
            {
                paragraph = Game.Data.Paragraphs.Get(id, Game.Data.XmlParagraphs[id]);

                Game.Data.CurrentParagraph = paragraph;
                Game.Data.CurrentParagraphID = id;
            }
            else
                paragraph = Game.Data.CurrentParagraph;

            Game.Xml.AllTextParse(ref Text, id, optionName);

            if ((paragraph.Images != null) && (paragraph.Images.Count > 0))
            {
                foreach (string image in paragraph.Images.Keys.ToList())
                {
                    Text.Children.Add(Output.Interface.IllustrationImage(image));

                    if (!String.IsNullOrEmpty(paragraph.Images[image]))
                        Text.Children.Add(Output.Interface.Text(paragraph.Images[image]));
                }
            }

            if (!loadGame && !reload)
            {
                Game.Option.Trigger(paragraph.Trigger);
                Game.Option.Trigger(paragraph.RemoveTrigger, remove: true);

                if ((paragraph.Modification != null) && (paragraph.Modification.Count > 0))
                    foreach (Abstract.IModification modification in paragraph.Modification)
                        modification.Do();
            }

            bool gameOver = false;

            if ((paragraph.Actions != null) && (paragraph.Actions.Count > 0))
            {
                int index = 0;

                foreach (Abstract.IActions action in paragraph.Actions)
                {
                    if (action.Name == "Option")
                    {
                        Action.Children.Add(AddOptionButton(action.Option, ref gameOver));
                        AddAftertext(ref Action, action.Option.Aftertext, action.Option.Aftertexts);
                    } 
                    else
                    {
                        StackLayout actionPlace = Output.Interface.ActionPlace();

                        foreach (View enemy in Output.Interface.Represent(action.Do(out _, "Representer")))
                            actionPlace.Children.Add(enemy);

                        actionPlace.Children.Add(Output.Buttons.Action(action.Button, Action_Click, action.IsButtonEnabled()));

                        Action.Children.Add(actionPlace);

                        Game.Router.AddAction(action.Button, index);
                        Game.Router.AddActionsPlaces(index, actionPlace);

                        AddAftertext(ref Action, action.Aftertext, action.Aftertexts);
                    }

                    index += 1;
                }
            }

            int optionCount = 0;

            foreach (Game.Option option in paragraph.Options)
            {
                bool mustBeVisible = OptionVisibility(option.Aftertext);

                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf) && !mustBeVisible)
                    continue;

                Button button = AddOptionButton(option, ref gameOver);

                Options.Children.Add(button);

                if (!String.IsNullOrEmpty(option.Input))
                    Options.Children.Add(AddInputField(option, button));

                AddAftertext(ref Options, option.Aftertext, option.Aftertexts);

                optionCount += 1;
            }

            if ((id == Game.Data.StartParagraph) && Game.Continue.IsGameSaved())
                Options.Children.Add(Output.Buttons.Additional("Продолжить предыдущую игру", Continue_Click));

            else if ((id > Game.Data.StartParagraph) && (Game.Data.Actions != null) && !(gameOver && (optionCount == 1)))
            {
                foreach (string buttonName in Game.Healing.List())
                    AddAdditionalButton(buttonName, HealingButton_Click);

                foreach (string buttonName in Game.Data.Actions.StaticButtons())
                    AddAdditionalButton(buttonName, StaticButton_Click);
            }

            if (Game.Settings.GetValue("SystemMenu") > 0)
                Options.Children.Add(SystemMenu());

            if (!reload)
                MainScroll.ScrollToAsync(MainScroll, ScrollToPosition.Start, true);

            UpdateStatus();
            CheckGameOver();

            Game.Option.Trigger(paragraph.LateTrigger);

            if (id != Game.Data.StartParagraph)
                Game.Continue.Save();
        }

        private bool OptionVisibility(string Aftertext)
        {
            if (!String.IsNullOrEmpty(Aftertext))
                return true;

            int bySetting = Game.Settings.GetValue("DisabledOption");

            if (bySetting > 0)
                return (bySetting == 1);

            return Game.Data.Constants.ShowDisabledOption();
        }

        private void AddAftertext(ref StackLayout layout, string text, List<Output.Text> texts)
        {
            if (!String.IsNullOrEmpty(text))
                layout.Children.Add(Output.Interface.Text(text));

            if (texts == null)
                return;

            foreach (Output.Text aftertext in texts)
                layout.Children.Add(Output.Interface.Text(aftertext));
        }

        private Entry AddInputField(Game.Option option, object button)
        {
            Game.Router.InputResponse = option.Input;

            return Output.Interface.Field(button, InputChange);
        }

        public StackLayout SystemMenu()
        {
            StackLayout systemLayout = Output.Interface.SystemMenu();

            systemLayout.Children.Add(Output.Buttons.System("Выйти",
                (object sender, EventArgs e) => System.Diagnostics.Process.GetCurrentProcess().Kill()));

            systemLayout.Children.Add(Output.Buttons.System("На главную",
                (object sender, EventArgs e) => this.Gamebooks(toMain: true)));

            return systemLayout;
        }

        private void InputChange(object sender, EventArgs e)
        {
            Entry field = sender as Entry;

            if (field.Text.ToLower() == Game.Router.InputResponse.ToLower())
            {
                field.IsVisible = false;
                Button button = (Button)field.BindingContext;
                button.IsVisible = true;
            }
        }

        private Button AddOptionButton(Game.Option option, ref bool gameOver)
        {
            Output.Buttons.EmptyOptionTextFuse(option);

            Game.Router.AddDestination(option.Text, option.Destination, option.Do);
            gameOver = (option.Destination == 0);

            return Output.Buttons.Option(option, Option_Click);
        }

        private void AddAdditionalButton(string name, EventHandler eventHandler) =>
            Options.Children.Add(Output.Buttons.Additional(name, eventHandler));

        private void UpdateStatus()
        {
            Status.Children.Clear();
            AdditionalStatus.Children.Clear();

            List<string> statuses = (Game.Data.Actions == null ? null : Game.Data.Actions.Status());

            if ((statuses == null) || Game.Data.Constants.GetParagraphsWithoutStatuses().Contains(Game.Data.CurrentParagraphID))
            {
                StatusBorder.IsVisible = false;
                MainGrid.RowDefinitions[1].Height = 0;

                Status.IsVisible = false;
                MainGrid.RowDefinitions[2].Height = 0;
            }
            else
            {
                Status.IsVisible = true;

                MainGrid.RowDefinitions[2].Height = 30;
                Status.BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBar));

                foreach (Label status in Output.Interface.StatusBar(statuses))
                    Status.Children.Add(status);

                if (!String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBorder)))
                {
                    StatusBorder.BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBorder));
                    StatusBorder.IsVisible = true;
                    MainGrid.RowDefinitions[1].Height = 1;
                }
            }

            List<string> additionalStatuses = (Game.Data.Actions == null ? null : Game.Data.Actions.AdditionalStatus());

            if ((additionalStatuses == null) || Game.Data.Constants.GetParagraphsWithoutStatuses().Contains(Game.Data.CurrentParagraphID))
            {
                AdditionalStatus.IsVisible = false;
                MainGrid.ColumnDefinitions[1].Width = 0;
            }
            else
            {
                string backgroundColor = Game.Data.Constants.GetColor(Game.Data.ColorTypes.AdditionalStatus);

                MainGrid.ColumnDefinitions[1].Width = 20;
                AdditionalStatus.BackgroundColor = (String.IsNullOrEmpty(backgroundColor) ? Color.LightGray : Color.FromHex(backgroundColor));
                AdditionalStatus.IsVisible = true;

                foreach (Output.VerticalText status in Output.Interface.AdditionalStatusBar(additionalStatuses))
                    AdditionalStatus.Children.Add(status);
            }
        }

        private void GamepageSettings()
        {
            string backColor = Game.Data.Constants.GetColor(Game.Data.ColorTypes.Background);

            if (!String.IsNullOrEmpty(backColor))
                MainScroll.BackgroundColor = Color.FromHex(backColor);
            else
                MainScroll.BackgroundColor = Color.White;
        }

        private void CheckGameOver()
        {
            if ((Game.Data.Actions == null) || !Game.Data.Actions.GameOver(out int toEndParagraph, out string toEndText))
                return;

            Game.Router.Clean();
            Options.Children.Clear();

            Game.Router.AddDestination(toEndText, toEndParagraph);

            Options.Children.Add(Output.Buttons.GameOver(toEndText, Option_Click));
        }
               
        private void Action_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int actionIndex = Game.Router.FindAction(b.Text);

            StackLayout actionPlace = Game.Router.FindActionsPlaces(actionIndex);

            actionPlace.Children.Clear();

            List<string> actionResult = Game.Data.CurrentParagraph.Actions[actionIndex].Do(out bool reload, Trigger: true);

            if (reload)
                Paragraph(Game.Data.CurrentParagraphID, reload: true);
            else
            {
                foreach (Label action in Output.Interface.Actions(actionResult))
                    actionPlace.Children.Add(action);

                UpdateStatus();
                CheckGameOver();
            }
        }

        private void StaticButton_Click(object sender, EventArgs e)
        {
            bool reload = Game.Data.Actions.StaticAction((sender as Button).Text);

            if (reload)
                Paragraph(Game.Data.CurrentParagraphID, reload: true);
        }
        
        private void HealingButton_Click(object sender, EventArgs e)
        {
            Game.Healing.Use((sender as Button).Text);

            Paragraph(Game.Data.CurrentParagraphID, reload: true);
        }

        private void Option_Click(object sender, EventArgs e) =>
            Paragraph(Game.Router.FindDestination((sender as Button).Text), optionName: (sender as Button).Text);

        private void Continue_Click(object sender, EventArgs e) => Paragraph(Game.Continue.Load(), loadGame: true);

        private void PageClean()
        {
            Text.Children.Clear();
            Action.Children.Clear();
            Options.Children.Clear();
            Footer.Children.Clear();
        }
    }
}

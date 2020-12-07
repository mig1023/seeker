using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Content.Res;
using Seeker.Gamebook;
using System.Xml;

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

        public void Gamebooks()
        {
            Text.Children.Clear();
            Game.Router.Clean();
            Options.Children.Clear();

            Text.Children.Add(Output.Interface.Text("Выберите книгу:", defaultParams: true));

            Game.Data.Actions = null;

            foreach (string gamebook in List.GetBooks())
            {
                Description gamebookDescr = List.GetDescription(gamebook);

                Image illustration = new Image
                {
                    Source = gamebookDescr.Illustration,
                    Aspect = Aspect.AspectFill
                };
                Options.Children.Add(illustration);

                Button button = Output.Interface.GamebookButton(gamebook);
                button.Clicked += Gamebook_Click;
                Options.Children.Add(button);

                Label disclaimer = Output.Interface.GamebookDisclaimer(gamebook);
                Options.Children.Add(disclaimer);
            }

            UpdateStatus();
        }

        private void Gamebook_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            Game.Continue.CurrentGame(b.Text);
            Game.Data.GameLoad(b.Text);
            GamepageSettings();

            Paragraph(0);
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            Paragraph(Game.Continue.Load(), loadGame: true);
        }

        public void Paragraph(int id, bool reload = false, bool loadGame = false)
        {
            bool startOfGame = (id == 0);

            Game.Router.Clean();

            Text.Children.Clear();
            Action.Children.Clear();
            Options.Children.Clear();

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

            string text = Game.Data.XmlParagraphs[id]["Text"].InnerText;
            Text.Children.Add(Output.Interface.Text(text));

            if (!String.IsNullOrEmpty(paragraph.Image))
            {
                Image illustration = new Image
                {
                    Source = paragraph.Image,
                    Aspect = Aspect.AspectFit
                };
                Text.Children.Add(illustration);
            }

            Game.Option.Trigger(paragraph.Trigger);
            Game.Option.Trigger(paragraph.RemoveTrigger, remove: true);

            if (!reload && (paragraph.Modification != null) && (paragraph.Modification.Count > 0))
                foreach(Abstract.IModification modification in paragraph.Modification)
                    modification.Do();

            if ((paragraph.Actions != null) && (paragraph.Actions.Count > 0))
            {
                int index = 0;

                foreach (Abstract.IActions action in paragraph.Actions)
                {
                    StackLayout actionPlace = Output.Interface.ActionPlace();

                    foreach (View enemy in Output.Interface.Represent(action.Do(out _, "Representer")))
                        actionPlace.Children.Add(enemy);

                    Button button = Output.Buttons.Action(action.ButtonName, action.IsButtonEnabled());
                    button.Clicked += Action_Click;
                    actionPlace.Children.Add(button);

                    Action.Children.Add(actionPlace);

                    Game.Router.AddAction(action.ButtonName, index);
                    Game.Router.AddActionsPlaces(index, actionPlace);

                    index += 1;

                    if (!String.IsNullOrEmpty(action.Aftertext))
                        Action.Children.Add(Output.Interface.Text(action.Aftertext));
                }
            }

            bool gameOver = false;
            int optionCount = 0;

            foreach (Game.Option option in paragraph.Options)
            {
                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf) && !Game.Data.ShowDisabledOption)
                    continue;

                Button button = Output.Buttons.Option(option);

                if (button == null)
                    continue;

                Game.Router.AddDestination(option.Text, option.Destination, option.Do);

                gameOver = (option.Destination == 0 ? true : false);

                button.Clicked += Option_Click;

                Options.Children.Add(button);

                optionCount += 1;
            }

            if ((id == 0) && Game.Continue.IsGameSaved())
            {
                Button button = Output.Buttons.Additional("Продолжить ранее начатую игру");
                button.Clicked += Continue_Click;
                Options.Children.Add(button);
            }
            else if ((id > 0) && (Game.Data.Actions != null) && !(gameOver && (optionCount == 1)))
                foreach(string buttonName in Game.Data.Actions.StaticButtons())
                {
                    Button button = Output.Buttons.Additional(buttonName);
                    button.Clicked += StaticButton_Click;
                    Options.Children.Add(button);
                }

            if (!reload)
                MainScroll.ScrollToAsync(MainScroll, ScrollToPosition.Start, true);

            UpdateStatus();
            CheckGameOver();

            Game.Option.Trigger(paragraph.LateTrigger);

            if (id != 0)
                Game.Continue.Save();
        }

        private void UpdateStatus()
        {
            List<string> statuses = (Game.Data.Actions == null ? null : Game.Data.Actions.Status());

            Status.Children.Clear();

            if (statuses == null)
            {
                StatusBorder.IsVisible = false;
                MainGrid.RowDefinitions[1].Height = 0;

                Status.IsVisible = false;
                MainGrid.RowDefinitions[2].Height = 0;
            }
            else
            {
                MainGrid.RowDefinitions[2].Height = 40;
                Status.BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBar));

                foreach (Label status in Output.Interface.StatusBar(statuses))
                    Status.Children.Add(status);

                Status.IsVisible = true;

                if (!String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusFont)))
                {
                    StatusBorder.BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusFont));
                    StatusBorder.IsVisible = true;
                    MainGrid.RowDefinitions[1].Height = 1;
                }
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

            Button button = Output.Interface.GameOverButton(toEndText);
            Game.Router.AddDestination(toEndText, toEndParagraph);
            button.Clicked += Option_Click;

            Options.Children.Add(button);
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
            Button b = sender as Button;

            bool reload = Game.Data.Actions.StaticAction(b.Text);

            if (reload)
                Paragraph(Game.Data.CurrentParagraphID, reload: true);
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Paragraph(Game.Router.FindDestination((sender as Button).Text));
        }
    }
}

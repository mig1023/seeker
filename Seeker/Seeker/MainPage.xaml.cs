using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Content.Res;
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

        public void Gamebooks()
        {
            Text.Children.Clear();
            Game.Router.Clean();
            Options.Children.Clear();

            Text.Children.Add(Game.Interface.Text("Выберите книгу:", defaultParams: true));

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

                Button button = Game.Interface.GamebookButton(gamebook);
                button.Clicked += Gamebook_Click;
                Options.Children.Add(button);

                Label disclaimer = Game.Interface.GamebookDisclaimer(gamebook);
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
            Paragraph(Game.Continue.Load());
        }

        public void Paragraph(int id, bool reload = false)
        {
            Game.Router.Clean();

            Text.Children.Clear();
            Action.Children.Clear();
            Options.Children.Clear();

            if (id == 0)
                Game.Data.Protagonist();

            Game.Paragraph paragraph = Game.Data.Paragraphs.Get(id);

            Game.Data.CurrentParagraph = paragraph;
            Game.Data.CurrentParagraphID = id;
                        
            Text.Children.Add(Game.Interface.Text(Game.Data.TextOfParagraphs.ContainsKey(id) ? Game.Data.TextOfParagraphs[id] : String.Empty));

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
                foreach(Interfaces.IModification modification in paragraph.Modification)
                    modification.Do();

            if ((paragraph.Actions != null) && (paragraph.Actions.Count > 0))
            {
                int index = 0;

                foreach (Interfaces.IActions action in paragraph.Actions)
                {
                    StackLayout actionPlace = Game.Interface.ActionPlace();

                    foreach (View enemy in Game.Interface.Represent(action.Do(out _, "Representer")))
                        actionPlace.Children.Add(enemy);

                    Button button = Game.Interface.ActionButton(action.ButtonName, action.IsButtonEnabled());
                    button.Clicked += Action_Click;
                    actionPlace.Children.Add(button);

                    Action.Children.Add(actionPlace);

                    Game.Router.AddAction(action.ButtonName, index);
                    Game.Router.AddActionsPlaces(index, actionPlace);

                    index += 1;

                    if (!String.IsNullOrEmpty(action.Aftertext))
                        Action.Children.Add(Game.Interface.Text(action.Aftertext));
                }
            }

            foreach (Game.Option option in paragraph.Options)
            {
                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf) && !Game.Data.ShowDisabledOption)
                    continue;

                Button button = Game.Interface.OptionButton(option);

                if (button == null)
                    continue;

                Game.Router.AddDestination(option.Text, option.Destination, option.Do);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }

            if ((id == 0) && Game.Continue.IsGameSaved())
            {
                Button button = Game.Interface.AdditionalButton("ПРОДОЛЖИТЬ РАНЕЕ НАЧАТУЮ ИГРУ");

                button.Clicked += Continue_Click;

                Options.Children.Add(button);
            }
            else if ((id > 0) && (Game.Data.Actions != null))
                foreach(string buttonName in Game.Data.Actions.StaticButtons())
                {
                    Button button = Game.Interface.AdditionalButton(buttonName);

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

                foreach (Label status in Game.Interface.StatusBar(statuses))
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

            Button button = new Button()
            {
                Text = toEndText,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Option))
            };

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
                foreach (Label action in Game.Interface.Actions(actionResult))
                    actionPlace.Children.Add(action);

                UpdateStatus();
                CheckGameOver();
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Paragraph(Game.Router.FindDestination((sender as Button).Text));
        }
    }
}

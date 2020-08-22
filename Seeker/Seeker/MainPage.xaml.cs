using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Content.Res;

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
            this.Text.Text = "Выберите книгу:";

            Game.Router.Clean();
            Options.Children.Clear();

            Gamebook.List.Init();
            Game.Data.Actions = null;

            foreach (string gamebook in Gamebook.List.GetBooks())
            {
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
            Game.Data.Load(b.Text);
            Paragraph(0);
        }

        public void Paragraph(int id, bool reload = false)
        {
            Game.Router.Clean();

            Options.Children.Clear();
            Action.Children.Clear();

            if (id == 0)
                Game.Data.Protagonist();

            Game.Paragraph paragraph = Game.Data.Paragraphs.Get(id);

            Game.Data.CurrentParagraph = paragraph;
            Game.Data.CurrentParagraphID = id;

            this.Text.Text = (Game.Data.TextOfParagraphs.ContainsKey(id) ? Game.Data.TextOfParagraphs[id] : String.Empty);

            Game.Option.OpenOption(paragraph.OpenOption);

            if ((paragraph.Modification != null) && (paragraph.Modification.Count > 0))
                foreach(Interfaces.IModification modification in paragraph.Modification)
                    modification.Do();

            if ((paragraph.Actions != null) && (paragraph.Actions.Count > 0))
            {
                int index = 0;

                foreach (Interfaces.IActions action in paragraph.Actions)
                {
                    StackLayout actionPlace = Game.Interface.ActionPlace();

                    foreach (Label enemy in Game.Interface.Represent(action.Do(out _, "Representer")))
                        actionPlace.Children.Add(enemy);

                    Button button = Game.Interface.ActionButton(action.ButtonName, action.IsButtonEnabled());
                    button.Clicked += Action_Click;
                    actionPlace.Children.Add(button);

                    Action.Children.Add(actionPlace);

                    Game.Router.AddAction(action.ButtonName, index);
                    Game.Router.AddActionsPlaces(index, actionPlace);

                    index += 1;

                    if (!String.IsNullOrEmpty(action.Aftertext))
                        Action.Children.Add(Game.Interface.Aftertext(action.Aftertext));
                }
            }

            foreach (Game.Option option in paragraph.Options)
            {
                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf))
                    continue;

                Button button = Game.Interface.OptionButton(option);

                if (button == null)
                    continue;

                Game.Router.AddDestination(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }

            if (!reload)
                MainScroll.ScrollToAsync(MainScroll, ScrollToPosition.Start, true);

            UpdateStatus();
            CheckGameOver();
        }

        private void UpdateStatus()
        {
            List<string> statuses = (Game.Data.Actions == null ? null : Game.Data.Actions.Status());

            Status.Children.Clear();

            if (statuses == null)
                Status.IsVisible = false;
            else
            {
                foreach (Label status in Game.Interface.StatusBar(statuses))
                    Status.Children.Add(status);

                Status.IsVisible = true;
            }           
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

            List<string> actionResult = Game.Data.CurrentParagraph.Actions[actionIndex].Do(out bool reload, openOption: true);

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
            Button b = sender as Button;
            Paragraph(Game.Router.FindDestination(b.Text));
        }
    }
}

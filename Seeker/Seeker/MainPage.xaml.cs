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

        public void Paragraph(int id)
        {
            Game.Router.Clean();
            Options.Children.Clear();
            Action.Children.Clear();

            Action.IsVisible = false;

            if (id == 0)
                Game.Data.Protagonist();

            Game.Paragraph paragraph = Game.Data.Paragraphs.Get(id);

            Game.Data.CurrentParagraph = paragraph;

            this.Text.Text = (Game.Data.TextOfParagraphs.ContainsKey(id) ? Game.Data.TextOfParagraphs[id] : String.Empty);

            if (!String.IsNullOrEmpty(paragraph.OpenOption))
                Game.Data.OpenedOption.Add(paragraph.OpenOption);

            if (paragraph.Modification != null)
                Game.Data.CurrentParagraph.Modification.Do();

            if (paragraph.Action != null)
            {
                foreach (Label enemy in Game.Interface.Represent(Game.Data.CurrentParagraph.Action.Do("Representer")))
                    Action.Children.Add(enemy);

                Button button = Game.Interface.ActionButton(paragraph.Action.ButtonName);
                button.Clicked += Action_Click;
                Action.Children.Add(button);

                Action.IsVisible = true;
            }

            string buttonsColor = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Main);

            foreach (Game.Option option in paragraph.Options)
            {
                string color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Main);

                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.OpenedOption.Contains(option.OnlyIf))
                    continue;
                else if (!String.IsNullOrEmpty(option.OnlyIf))
                    color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Option);

                Button button = Game.Interface.OptionButton(option);

                if (button == null)
                    continue;

                Game.Router.Add(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }

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
            bool gameOver = (Game.Data.Actions == null ? false : Game.Data.Actions.GameOver());

            if (gameOver)
            {
                Game.Router.Clean();
                Options.Children.Clear();

                Button button = new Button()
                {
                    Text = "Начать сначала",
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Option))
                };

                Game.Router.Add("Начать сначала", 0);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }
        }

        private void Action_Click(object sender, EventArgs e)
        {
            Action.Children.Clear();

            List<string> actionResult = Game.Data.CurrentParagraph.Action.Do();

            foreach (Label action in Game.Interface.Actions(actionResult))
                Action.Children.Add(action);

            UpdateStatus();
            CheckGameOver();
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Paragraph(Game.Router.Find(b.Text));
        }
    }
}

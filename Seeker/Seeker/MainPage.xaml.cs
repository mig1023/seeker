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
                Button button = new Button()
                {
                    Text = gamebook,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex("#4682B4")
                };

                button.Clicked += Gamebook_Click;

                Options.Children.Add(button);

                Label disclaimer = new Label();
                disclaimer.Text = String.Format("© {0}", Gamebook.List.GetDescription(gamebook).Disclaimer);
                disclaimer.HorizontalTextAlignment = TextAlignment.Start;
                disclaimer.Margin = new Thickness(0, 0, 0, 8);

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
                foreach (string s in Game.Data.CurrentParagraph.Action.Do("Representer"))
                {
                    Label enemy = new Label();
                    enemy.Text = s.Replace("\n", System.Environment.NewLine);
                    enemy.HorizontalTextAlignment = TextAlignment.Center;
                    Action.Children.Add(enemy);
                }

                Button button = new Button()
                {
                    Text = paragraph.Action.ButtonName,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Action))
                };

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

                if (!String.IsNullOrEmpty(option.OnlyIf))
                {
                    if (!Game.Data.OpenedOption.Contains(option.OnlyIf))
                        continue;
                    else
                        color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Option);
                }

                Button button = new Button()
                {
                    Text = option.Text,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(color)
                };

                Game.Router.Add(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }

            UpdateStatus();
            CheckGameOver();
        }

        private void UpdateStatus()
        {
            List<Label> statuses = (Game.Data.Actions == null ? null : Game.Data.Actions.Status());

            if (statuses == null)
            {
                Status.IsVisible = false;

                return;
            }
            else
            {
                Status.Children.Clear();

                foreach (Label status in statuses)
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

            foreach (string actionLine in actionResult)
            {
                Label actions = new Label();

                string text = actionLine;

                if (text.Contains("BIG|"))
                    actions.FontSize = 22;
                else
                    actions.FontSize = 10;

                if (text.Contains("BAD|"))
                    actions.TextColor = Color.Red;

                if (text.Contains("GOOD|"))
                    actions.TextColor = Color.Green;

                if (text.Contains("BOLD|"))
                    actions.FontAttributes = FontAttributes.Bold;

                if (text.Contains("HEAD|"))
                {
                    actions.HorizontalTextAlignment = TextAlignment.Center;
                    actions.FontAttributes = FontAttributes.Bold;
                }
                else
                    actions.HorizontalTextAlignment = TextAlignment.Start;

                foreach (string r in new List<string> { "BIG", "GOOD", "BAD", "HEAD", "BOLD" })
                    text = text.Replace(String.Format("{0}|", r), String.Empty);

                actions.Text = text;
                
                Action.Children.Add(actions);
            }

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

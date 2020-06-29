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

            string buttonsColor = Game.Buttons.NextColor();

            foreach (string gamebook in Gamebook.List.Get())
            {
                Button button = new Button()
                {
                    Text = gamebook,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(buttonsColor)
                };

                button.Clicked += Gamebook_Click;

                Options.Children.Add(button);
            }
        }

        private void Gamebook_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Game.Data.Load(Gamebook.List.Find(b.Text));
            Paragraph(0);
        }

        public void Paragraph(int id)
        {
            if (!Gamebook.BlackCastleDungeon.Paragraphs.ContainsKey(id))
                return;

            Game.Paragraph paragraph = Gamebook.BlackCastleDungeon.Paragraphs[id];

            this.Text.Text = (Game.Data.Paragraphs.ContainsKey(id) ? Game.Data.Paragraphs[id] : String.Empty);

            if (!String.IsNullOrEmpty(paragraph.OpenOption))
                Game.Data.OpenedOption.Add(paragraph.OpenOption);

            Game.Router.Clean();
            Options.Children.Clear();

            string buttonsColor = Game.Buttons.NextColor();

            foreach (Game.Option option in paragraph.Options)
            {
                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.OpenedOption.Contains(option.OnlyIf))
                    continue;

                Button button = new Button()
                {
                    Text = option.Text,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(buttonsColor)
                };

                Game.Router.Add(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Paragraph(Game.Router.Find(b.Text));
        }
    }
}

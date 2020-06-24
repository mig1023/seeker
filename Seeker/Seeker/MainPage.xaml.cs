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
            this.Text.Text = "Choose the book:";

            Game.Router.Clean();
            Options.Children.Clear();

            string buttonsColor = Game.Buttons.NextColor();

            foreach (string gamebook in DependencyService.Get<Other.IAssets>().GetAllFromAssets())
            {
                if (!gamebook.Contains(".xml"))
                    continue;

                string gamebookName = gamebook.Remove(gamebook.Length - 4);

                Button button = new Button()
                {
                    Text = gamebookName,
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
            Game.Data.Load("BlackCastleDungeon.xml");
            Paragraph(0);
        }

        public void Paragraph(int id)
        {
            if (!Gamebook.BlackCastleDungeon.Paragraphs.ContainsKey(id))
                return;

            Game.Paragraph paragraph = Gamebook.BlackCastleDungeon.Paragraphs[id];

            this.Text.Text = (Game.Data.Paragraphs.ContainsKey(id) ? Game.Data.Paragraphs[id] : String.Empty);

            Game.Router.Clean();
            Options.Children.Clear();

            string buttonsColor = Game.Buttons.NextColor();

            foreach (Game.Option option in paragraph.Options)
            {
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

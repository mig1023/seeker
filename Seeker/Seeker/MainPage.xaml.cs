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
            this.Title.Text = "Choose the book:";
            this.Text.Text = String.Empty;

            Game.Router.Clean();
            Options.Children.Clear();

            foreach (string gamebook in DependencyService.Get<Other.IAssets>().GetAllFromAssets())
            {
                if (!gamebook.Contains(".xml"))
                    continue;

                string gamebookName = gamebook.Remove(gamebook.Length - 4);

                Button button = new Button()
                {
                    Text = gamebookName,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex("#2196F3")
                };

                button.Clicked += Gamebook_Click;

                Options.Children.Add(button);
            }
        }

        private void Gamebook_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Gamebook.Data.LoadGameBook(b.Text + ".xml");
            Paragraph(1);
        }

        public void Paragraph(int id)
        {
            if (!Gamebook.Data.Paragraphs.ContainsKey(id))
                return;

            Gamebook.Paragraph paragraph = Gamebook.Data.Paragraphs[id];

            this.Title.Text = paragraph.Title;
            this.Text.Text = paragraph.Text;

            Game.Router.Clean();
            Options.Children.Clear();

            foreach (Gamebook.Option option in paragraph.Options)
            {
                Button button = new Button()
                {
                    Text = option.Text,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex("#2196F3")
                };

                Game.Router.Add(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int? newParagraph = Game.Router.Find(b.Text);

            if (newParagraph != null)
                Paragraph(newParagraph ?? 0);
        }
    }
}

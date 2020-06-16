using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Seeker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private static Dictionary<string, int> tmpRouter = new Dictionary<string, int>();

        public MainPage()
        {
            InitializeComponent();

            GoToParagraph(1);
        }

        public void GoToParagraph(int id)
        {
            Gamebook.Paragraph paragraph = Gamebook.Data.DummyParagraphs[id];

            this.Title.Text = paragraph.Title;
            this.Text.Text = paragraph.Text;

            tmpRouter.Clear();

            Options.Children.Clear();

            foreach (Gamebook.Option option in paragraph.Options)
            {
                Button button = new Button()
                {
                    Text = option.Text,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex("#2196F3")
                };

                tmpRouter.Add(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int? newParagraph = null;

            foreach(KeyValuePair<string, int> k in tmpRouter)
                if (k.Key == b.Text)
                    newParagraph = k.Value;

            if (newParagraph != null)
                GoToParagraph(newParagraph ?? 0);
        }
    }
}

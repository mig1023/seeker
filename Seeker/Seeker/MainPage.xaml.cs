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

            foreach(Gamebook.Option option in paragraph.Options)
            {
                Button button = new Button()
                {
                    Text = option.Text,
                };

                Options.Children.Add(button);
            }
        }
    }
}

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

            Paragraph(1);
        }

        public void Paragraph(int id)
        {
            if (!Gamebook.Data.DummyParagraphs.ContainsKey(id))
                return;

            Gamebook.Paragraph paragraph = Gamebook.Data.DummyParagraphs[id];

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

        async void OnAppearing(object sender, EventArgs args)
        {
            base.OnAppearing();
            await UpdateFileList();
        }

        async void Save(object sender, EventArgs args)
        {
            string filename = fileNameEntry.Text;
            
            if (String.IsNullOrEmpty(filename)) return;
            
            if (await DependencyService.Get<Other.IFile>().ExistsAsync(filename))
            {
                bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                if (isRewrited == false) return;
            }
            
            await UpdateFileList();
        }
        async void FileSelect(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;
            
            string filename = (string)args.SelectedItem;
            textEditor.Text = await DependencyService.Get<Other.IFile>().LoadTextAsync((string)args.SelectedItem);
            
            fileNameEntry.Text = filename;
            filesList.SelectedItem = null;

        }
        async Task UpdateFileList()
        {
            filesList.ItemsSource = await DependencyService.Get<Other.IFile>().GetFilesAsync();
            filesList.SelectedItem = null;
        }
    }
}

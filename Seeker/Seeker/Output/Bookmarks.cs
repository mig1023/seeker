using Xamarin.Forms;

namespace Seeker.Output
{
    class Bookmarks
    {
        public static void Add(ref StackLayout bookmarks)
        {
            bookmarks.Children.Add(Interface.Text("Сделать новую закладку:", defaultParams: true));

            Entry field = new Entry
            {
                Placeholder = "Название закладки",
                //BindingContext = binding,
                FontFamily = Interface.TextFontFamily(),
            };

            bookmarks.Children.Add(field);
            bookmarks.Children.Add(Buttons.Bookmark((s, args) => Save(field.Text), Constants.BOOKMARK_SAVE));
        }

        private static void Save(string bookmarkName)
        {
            Game.Bookmarks.Save(bookmarkName);
        }
    }
}

using System.Collections.Generic;
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

            Dictionary<string, string> allBookmarks = Game.Bookmarks.List();

            if (allBookmarks.Count > 0)
            {
                foreach (string bookmark in allBookmarks.Keys)
                {
                    bookmarks.Children.Add(Buttons.Bookmark((s, args) => Load(allBookmarks[bookmark]), bookmark));
                }
            }
            else
            {
                bookmarks.Children.Add(Interface.Text("Пока ещё нет ни одной закладки", defaultParams: true));
            }
        }

        private static void Save(string bookmarkName)
        {
            Game.Bookmarks.Save(bookmarkName);
        }

        private static void Load(string bookmarkName)
        {
            Game.Bookmarks.Load(bookmarkName);
        }
    }
}

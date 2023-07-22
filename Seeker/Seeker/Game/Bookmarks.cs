using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Game
{
    class Bookmarks
    {
        private static List<string> BookmarkList(string bookmarksName)
        {
            if (!App.Current.Properties.TryGetValue(bookmarksName, out object bookmarksList))
                return new List<string>();
            else
                return (bookmarksList as string).Split(',').ToList();
        }

        public static Dictionary<string, string> List(out string bookmarksName)
        {
            bookmarksName = $"{Data.CurrentGamebook}-BOOKMARKS";
            Dictionary<string, string> bookmarks = new Dictionary<string, string>();

            foreach (string bookmark in BookmarkList(bookmarksName))
                bookmarks[bookmark.Split(':')[1]] = bookmark.Split(':')[0];

            return bookmarks;
        }

        public static void Save(string bookmark)
        {
            Dictionary<string, string> bookmarks = List(out string bookmarksName);
            Services.BookmarkName(bookmarks, bookmark, out string bookmarkOut, out string saveName);

            if (bookmarks.Count == 0)
                App.Current.Properties[bookmarksName] = $"{saveName}:{bookmarkOut}";
            else
                App.Current.Properties[bookmarksName] += $",{saveName}:{bookmarkOut}";

            Continue.Save($"{Data.CurrentGamebook}-{saveName}");
        }

        public static void Remove(string bookmark)
        {
            Dictionary<string, string> bookmarks = List(out string bookmarksName);
            string bookmarkIndex = bookmark.Split('-')[1];
            string newBookmarkList = String.Empty; 

            foreach (string index in bookmarks.Keys)
            {
                if (bookmarks[index] == bookmarkIndex)
                {
                    App.Current.Properties.Remove(bookmark);
                }
                else
                {
                    if (!String.IsNullOrEmpty(newBookmarkList))
                        newBookmarkList += ",";

                    newBookmarkList += String.Format("{0}:{1}", bookmarks[index], index);
                }
            }

            if (String.IsNullOrEmpty(newBookmarkList))
                App.Current.Properties.Remove(bookmarksName);
            else
                App.Current.Properties[bookmarksName] = newBookmarkList;
        }

        public static void Clean()
        {
            foreach (string gamebook in Gamebook.List.GetBooks())
            {
                string bookmarksName = String.Format("{0}-BOOKMARKS", gamebook);

                foreach (string bookmark in BookmarkList(bookmarksName))
                    App.Current.Properties.Remove(String.Format("{0}-{1}", gamebook, bookmark.Split(':')[0]));

                if (App.Current.Properties.ContainsKey(bookmarksName))
                    App.Current.Properties.Remove(bookmarksName);
            }
        }
    }
}

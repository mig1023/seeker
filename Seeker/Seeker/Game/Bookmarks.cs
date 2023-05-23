using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Game
{
    class Bookmarks
    {
        public static Dictionary<string, string> List(out string currentGame, out string bookmarksName)
        {
            currentGame = Continue.GetCurrentGame();
            bookmarksName = String.Format("{0}-BOOKMARKS", currentGame);
            Dictionary<string, string> bookmarks = new Dictionary<string, string>();

            if (App.Current.Properties.TryGetValue(bookmarksName, out object bookmarksList))
            {
                List<string> allBookmarks = (bookmarksList as string).Split(',').ToList();

                foreach (string bookmark in allBookmarks)
                    bookmarks[bookmark.Split(':')[1]] = bookmark.Split(':')[0];
            }

            return bookmarks;
        }

        public static void Save(string bookmark)
        {
            Dictionary<string, string> bookmarks = List(out string currentGame, out string bookmarksName);
            Services.BookmarkName(bookmarks, bookmark, out string bookmarkOut, out string saveName);

            if (bookmarks.Count == 0)
                App.Current.Properties[bookmarksName] = String.Format("{0}:{1}", saveName, bookmarkOut);
            else
                App.Current.Properties[bookmarksName] += String.Format(",{0}:{1}", saveName, bookmarkOut);

            Continue.Save(String.Format("{0}-{1}", currentGame, saveName));
        }

        public static void Remove(string bookmark)
        {
            Dictionary<string, string> bookmarks = List(out string _, out string bookmarksName);
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

                if (App.Current.Properties.TryGetValue(bookmarksName, out object bookmarksList))
                {
                    List<string> allBookmarks = (bookmarksList as string).Split(',').ToList();

                    foreach (string bookmark in allBookmarks)
                        App.Current.Properties.Remove(String.Format("{0}-{1}", gamebook, bookmark.Split(':')[0]));
                }

                if (App.Current.Properties.ContainsKey(bookmarksName))
                    App.Current.Properties.Remove(bookmarksName);
            }
        }
    }
}

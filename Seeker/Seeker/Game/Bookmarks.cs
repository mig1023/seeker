using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Game
{
    class Bookmarks
    {
        public static Dictionary<string, string> List()
        {
            string currentGame = Continue.GetCurrentGame();
            string bookmarksName = String.Format("{0}-BOOKMARKS", currentGame);
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
            Dictionary<string, string> bookmarks = List();

            string currentGame = Continue.GetCurrentGame();
            string bookmarksName = String.Format("{0}-BOOKMARKS", currentGame);

            int nextSaveGameIndex = 0;
            string saveName = String.Empty;

            do
            {
                nextSaveGameIndex += 1;
                saveName = String.Format("SAVE{0}", nextSaveGameIndex);
            }
            while (bookmarks.Values.Contains(saveName));

            if (bookmarks.Count == 0)
                App.Current.Properties[bookmarksName] = String.Format("{0}:{1}", saveName, bookmark);
            else
                App.Current.Properties[bookmarksName] += String.Format(",{0}:{1}", saveName, bookmark);

            Continue.Save(String.Format("{0}-{1}", currentGame, saveName));
        }
    }
}

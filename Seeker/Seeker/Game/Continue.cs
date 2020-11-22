using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Game
{
    class Continue
    {
        static string CurrentGameName { get; set; } 

        public static void CurrentGame(string name)
        {
            App.Current.Properties["LastGame"] = name;
            CurrentGameName = name;
        }

        public static bool IsGameSaved() => App.Current.Properties.TryGetValue(CurrentGameName, out _);

        public static void Save()
        {
            string triggers = String.Join(",", Game.Data.Triggers);
            int paragraph = Game.Data.CurrentParagraphID;
            string character = Game.Data.Save();

            App.Current.Properties[CurrentGameName] = String.Format("{0}@{1}@{2}", paragraph, triggers, character);
        }

        public static int Load()
        {
            string saveLine = (string)App.Current.Properties[CurrentGameName];

            string[] save = saveLine.Split('@');

            Game.Data.CurrentParagraphID = int.Parse(save[0]);
            Game.Data.Triggers = save[1].Split(',').ToList();
            Game.Data.Load(save[2]);

            return Game.Data.CurrentParagraphID;
        }
    }
}

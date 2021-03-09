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

        public static int? IntNullableParse(string line)
        {
            if (int.TryParse(line, out int value))
                return (value == -1 ? (int?)null : value);
            else
                return null;
        }

        public static bool IsGameSaved() => App.Current.Properties.TryGetValue(CurrentGameName, out _);

        public static void Save()
        {
            string triggers = String.Join(",", Game.Data.Triggers);
            string healing = Healing.Save();
            int paragraph = Game.Data.CurrentParagraphID;
            string character = Game.Data.Save();

            App.Current.Properties[CurrentGameName] = String.Format("{0}@{1}@{2}@{3}", paragraph, triggers, healing, character);
        }

        public static int Load()
        {
            string saveLine = (string)App.Current.Properties[CurrentGameName];

            string[] save = saveLine.Split('@');

            Game.Data.CurrentParagraphID = int.Parse(save[0]);
            Game.Data.Triggers = save[1].Split(',').ToList();

            Healing.Load(save[1]);
            Game.Data.Load(save[2]);

            return Game.Data.CurrentParagraphID;
        }
    }
}

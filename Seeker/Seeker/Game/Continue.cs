using System;
using System.Linq;

namespace Seeker.Game
{
    class Continue
    {
        public static void CurrentGame(string name)
        {
            App.Current.Properties["LastGame"] = name;
            Data.CurrentGamebook = name;
        }

        public static int? IntNullableParse(string line)
        {
            if (int.TryParse(line, out int value))
            {
                return (value == -1 ? (int?)null : value);
            }
            else
            {
                return null;
            }
        }

        public static bool IsGameSaved() =>
            App.Current.Properties.TryGetValue(Data.CurrentGamebook, out _);

        public static void SaveCurrentGame() =>
            Save(Data.CurrentGamebook);

        public static void Save(string gameName)
        {
            string triggers = String.Join(",", Data.Triggers);
            string healing = Healing.Save();
            int paragraph = Data.CurrentParagraphID;
            string path = String.Join(",", Data.Path);
            string character = Data.Character.Save();

            App.Current.Properties[gameName] =
                $"{paragraph}@{triggers}@{healing}@{character}@{path}";
        }

        public static int Load(string gameName)
        {
            if (String.IsNullOrEmpty(gameName))
                gameName = Data.CurrentGamebook;

            string saveLine = (string)App.Current.Properties[gameName];

            string[] save = saveLine.Split('@');

            Data.CurrentParagraphID = int.Parse(save[0]);
            Data.Triggers = save[1].Split(',').ToList();

            Healing.Load(save[2]);
            Data.Character.Load(save[3]);
            Data.Path = save[4].Split(',').ToList();

            return Data.CurrentParagraphID;
        }

        public static void Remove() =>
            App.Current.Properties.Remove(Data.CurrentGamebook);

        public static void Clean()
        {
            foreach (string gamebook in Gamebook.List.GetBooks())
                App.Current.Properties.Remove(gamebook);

            foreach (string variable in Game.Data.OuterGameVariable)
            {
                if (App.Current.Properties.ContainsKey(variable))
                    App.Current.Properties.Remove(variable);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Seeker.Game
{
    class Settings
    {
        private static Dictionary<string, int> Values { get; set; }

        public static bool IsEnabled(string name) =>
            GetValue(name) > 0;

        public static int GetValue(string name)
        {
            if (Values == null)
                Load();

            return Values.ContainsKey(name) ? Values[name] : 0;
        } 

        public static void SetValue(string name, int value)
        {
            Values[name] = value;
            Save();
        }

        private static void Load()
        {
            Values = new Dictionary<string, int>();

            if (!IsSettingsSaved())
                return;

            foreach (string setting in (App.Current.Properties["Settings"] as string).Split(','))
            {
                string[] value = setting.Split('=');
                Values.Add(value[0], int.Parse(value[1]));
            }
        }

        public static void Clean()
        {
            App.Current.Properties.Remove("Settings");
            Load();
        }

        private static void Save()
        {
            string setting = string
                .Join(",", Values.Select(x => x.Key + "=" + x.Value)
                .ToArray());

            App.Current.Properties["Settings"] = setting;
        }

        private static bool IsSettingsSaved() =>
            App.Current.Properties.TryGetValue("Settings", out _);
    }
}

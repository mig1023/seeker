using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Game
{
    class Settings
    {
        private static Dictionary<string, int> Values { get; set; }

        public static int GetValue(string name)
        {
            if (Values == null)
                Load();

            if (Values.ContainsKey(name))
                return Values[name];
            else
                return 0;
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

        private static void Save() =>
            App.Current.Properties["Settings"] = string.Join(",", Values.Select(x => x.Key + "=" + x.Value).ToArray());

        private static bool IsSettingsSaved() => App.Current.Properties.TryGetValue("Settings", out _);
    }
}

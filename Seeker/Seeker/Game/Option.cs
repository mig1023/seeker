using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Option
    {
        public int Destination { get; set; }
        public string Text { get; set; }
        public string Availability { get; set; }
        public bool Dynamic { get; set; }
        public string Singleton { get; set; }
        public List<Output.Text> Aftertexts { get; set; }
        public string Input { get; set; }
        public string Style { get; set; }

        public List<Abstract.IModification> Do { get; set; }

        private static Dictionary<Option, Button> Options { get; set; }

        public static void Trigger(string triggers, bool remove = false)
        {
            if (String.IsNullOrEmpty(triggers))
                return;

            string[] triggerList = triggers.Split(',');

            foreach (string trigger in triggerList)
                if (remove)
                    Data.Triggers.RemoveAll(x => x == trigger.Trim());
                else
                    Data.Triggers.Add(trigger.Trim());
        }

        public static bool IsTriggered(string trigger) =>
            Data.Triggers.Contains(trigger);

        public static void ListClean() =>
            Options = new Dictionary<Option, Button>();

        public static void ListAdd(Option option, Button button) =>
            Options.Add(option, button);

        public static void OpenButtonByDestination(int destination)
        {
            Button button = Options
                .Where(x => x.Key.Destination == destination)
                .FirstOrDefault()
                .Value;

            if (button != null)
            {
                button.IsVisible = true;
                button.IsEnabled = true;
            }
        }
    }
}

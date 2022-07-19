using System;
using System.Collections.Generic;

namespace Seeker.Game
{
    class Option
    {
        public int Link { get; set; }
        public string Text { get; set; }
        public string Availability { get; set; }
        public string Singleton { get; set; }
        public string Aftertext { get; set; }
        public List<Output.Text> Aftertexts { get; set; }
        public string Input { get; set; }

        public Abstract.IModification Do { get; set; }

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

        public static bool IsTriggered(string trigger) => Data.Triggers.Contains(trigger);
    }
}

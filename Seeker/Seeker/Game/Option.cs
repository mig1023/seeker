using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Option
    {
        public int Destination { get; set; }
        public string Text { get; set; }
        public string OnlyIf { get; set; }

        public Interfaces.IModification Do { get; set; }

        public static void Trigger(string triggers, bool remove = false)
        {
            if (String.IsNullOrEmpty(triggers))
                return;

            string[] triggerList = triggers.Split(',');

            foreach (string trigger in triggerList)
                if (remove)
                    Game.Data.Triggers.Remove(trigger.Trim());
                else
                    Game.Data.Triggers.Add(trigger.Trim());
        }
    }
}

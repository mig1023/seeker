using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SilverAgeSilhouette
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Verse")
            {
                Character.Protagonist.Verse.Add(ValueString);
            }
            else if (Name == "Replace")
            {
                TriggerReplace(TriggerSplit(ValueString));
            }
            else if (Name == "Add")
            {
                TriggerAdd(TriggerSplit(ValueString));
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }

        private void TriggerReplace(List<string> triggers)
        {
            if (Game.Option.IsTriggered(triggers[0]))
            {
                Game.Option.Trigger(triggers[0], remove: true);
                Game.Option.Trigger(triggers[1]);
            }
            else
            {
                Game.Option.Trigger(triggers[0]);
            }
        }

        private void TriggerAdd(List<string> triggers) =>
            Game.Option.Trigger(triggers[Game.Option.IsTriggered(triggers[0]) ? 1 : 0]);

        private List<string> TriggerSplit(string triggersLine)
        {
            List<string> triggers = triggersLine
                .Split(new string[] { "-->" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            return triggers;
        }
    }
}

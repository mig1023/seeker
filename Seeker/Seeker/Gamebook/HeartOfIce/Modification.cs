using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HeartOfIce
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public void Do()
        {
            if (Name == "Skill")
                Character.Protagonist.Skills.Add(ValueString);

            else if (Name == "RemoveTrigger")
                Game.Option.Trigger(ValueString, remove: true);

            else if (Name == "ReplaceTrigger")
            {
                string[] triggers = ValueString.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                if (!Game.Data.Triggers.Contains(triggers[0].Trim()))
                    return;

                Game.Option.Trigger(triggers[0].Trim(), remove: true);
                Game.Option.Trigger(triggers[1].Trim());
            }
            else if (Name == "LifeByTrigger")
                LifeByTrigger();

            else if (Name == "LifeByNotTrigger")
                LifeByTrigger(notLogic: true);

            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                if (Name.StartsWith("Max"))
                {
                    string normalParam = Name.Remove(0, 3);

                    int normalValue = (int)Character.Protagonist.GetType().GetProperty(normalParam).GetValue(Character.Protagonist, null);

                    if ((normalValue + Value) > currentValue)
                        Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue + Value);

                    Character.Protagonist.GetType().GetProperty(normalParam).SetValue(Character.Protagonist, currentValue + Value);
                }
                else
                {
                    currentValue += Value;

                    Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
                }
            }
        }

        private void LifeByTrigger(bool notLogic = false)
        {
            string[] values = ValueString.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
            string[] triggers = values[0].Split('|');

            bool isTrigger = false;

            foreach (string trigger in triggers)
                if (Game.Data.Triggers.Contains(trigger.Trim()) || Character.Protagonist.Skills.Contains(trigger.Trim()))
                    isTrigger = true;

            if (isTrigger != notLogic)
                Character.Protagonist.Life += int.Parse(values[1].Trim());
        }
    }
}

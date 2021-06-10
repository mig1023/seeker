using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Prototypes
{
    class Modification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public bool Empty { get; set; }
        public bool Restore { get; set; }

        public delegate void ParamMod();

        public virtual void Do() { }

        public void InnerDo(Abstract.ICharacter Character)
        {
            if (Name == "Trigger")
                Game.Option.Trigger(ValueString);

            else if (Name == "Healing")
                Game.Healing.Add(ValueString);

            else
            {
                int currentValue = 0;

                if (Restore)
                    currentValue = (int)Character.GetType().GetProperty("Max" + Name).GetValue(Character, null);

                else if (!Empty)
                    currentValue = (int)Character.GetType().GetProperty(Name).GetValue(Character, null);

                if (Name.StartsWith("Max"))
                {
                    string normalParam = Name.Remove(0, 3);

                    int normalValue = (int)Character.GetType().GetProperty(normalParam).GetValue(Character, null);

                    if ((normalValue + Value) > currentValue)
                        Character.GetType().GetProperty(Name).SetValue(Character, currentValue + Value);

                    Character.GetType().GetProperty(normalParam).SetValue(Character, currentValue + Value);
                }
                else
                {
                    currentValue += Value;

                    Character.GetType().GetProperty(Name).SetValue(Character, currentValue);
                }
            }
        }

        public void ModificationByTrigger(string trigger, ParamMod doModification, bool not = false)
        {
            if ((!not && Game.Data.Triggers.Contains(trigger)) || (not && !Game.Data.Triggers.Contains(trigger)))
                doModification();
        }

        public bool ModificationByName(string actualName, ParamMod doModification)
        {
            if (Name == actualName)
            {
                doModification();
                return true;
            }
            else
                return false;
        }

        public bool ModificationByValueString(string actualName, ParamMod doModification) =>
            (String.IsNullOrEmpty(ValueString) ? false : ModificationByName(actualName, doModification));
    }
}

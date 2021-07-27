using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HeartOfIce
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            bool skill = ModificationByName("Skill", () => Character.Protagonist.Skills.Add(ValueString));
            bool rmSkill = ModificationByName("RemoveSkill", () => Character.Protagonist.Skills.RemoveAll(item => item == ValueString));
            bool rmTrigger = ModificationByName("RemoveTrigger", () => Game.Option.Trigger(ValueString, remove: true));
            bool byTrigger = ModificationByName("ByTrigger", () => LifeByTrigger());
            bool byNotTrigger = ModificationByName("ByNotTrigger", () => LifeByTrigger(notLogic: true));
            bool byFood = ModificationByName("ByFood", () => LifeByFood());

            if (skill || rmSkill || rmTrigger || byTrigger || byNotTrigger || byFood)
                return;

            else if (Name == "ReplaceTrigger")
            {
                string[] triggers = ValueString.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                if (!Game.Data.Triggers.Contains(triggers[0].Trim()))
                    return;

                Game.Option.Trigger(triggers[0].Trim(), remove: true);
                Game.Option.Trigger(triggers[1].Trim());
            }
            else
                InnerDo(Character.Protagonist);
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

        private void LifeByFood()
        {
            if (Character.Protagonist.Skills.Contains(ValueString))
                return;

            else if (Character.Protagonist.Food > 0)
                Character.Protagonist.Food -= 1;

            else
                Character.Protagonist.Life -= Value;
        }
    }
}

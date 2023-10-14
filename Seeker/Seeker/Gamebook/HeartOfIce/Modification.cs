using System;

namespace Seeker.Gamebook.HeartOfIce
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            bool skill = DoByName("Skill", () => Character.Protagonist.Skills.Add(ValueString));
            bool rmSkill = DoByName("RemoveSkill", () => Character.Protagonist.Skills.RemoveAll(item => item == ValueString));
            bool rmTrigger = DoByName("Untrigger", () => Game.Option.Trigger(ValueString, remove: true));
            bool byTrigger = DoByName("ByTrigger", () => LifeByTrigger());
            bool byNotTrigger = DoByName("ByNotTrigger", () => LifeByTrigger(notLogic: true));
            bool byFood = DoByName("ByFood", () => LifeByFood());

            if (skill || rmSkill || rmTrigger || byTrigger || byNotTrigger || byFood)
            {
                return;
            }
            else if (Name == "ReplaceTrigger")
            {
                string[] triggers = ValueString
                    .Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                if (!Game.Option.IsTriggered(triggers[0].Trim()))
                    return;

                Game.Option.Trigger(triggers[0].Trim(), remove: true);
                Game.Option.Trigger(triggers[1].Trim());
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }

        private bool IsTriggered(string triggersLine)
        {
            string[] triggers = triggersLine.Split('|');

            foreach (string trigger in triggers)
            {
                bool isSkill = Character.Protagonist.Skills.Contains(trigger.Trim());

                if (Game.Option.IsTriggered(trigger.Trim()) || isSkill)
                    return true;
            }

            return false;
        }

        private void LifeByTrigger(bool notLogic = false)
        {
            string[] values = ValueString
                .Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

            if (IsTriggered(values[0]) != notLogic)
                Character.Protagonist.Life += int.Parse(values[1].Trim());
        }

        private void LifeByFood()
        {
            Character protagonist = Character.Protagonist;

            if ((ValueString != null) && (ValueString.Contains("->")))
            {
                string[] values = ValueString
                    .Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                if (!IsTriggered(values[0]))
                    protagonist.Life -= Game.Xml.IntParse(values[1]);
            }
            else if (protagonist.Food > 0)
            {
                protagonist.Food -= 1;
            }
            else
            {
                protagonist.Life -= Value;
            }
        }
    }
}

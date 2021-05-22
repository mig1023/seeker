﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HeartOfIce
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Skill")
                Character.Protagonist.Skills.Add(ValueString);

            else if (Name == "RemoveSkill")
                Character.Protagonist.Skills.RemoveAll(item => item == ValueString);

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
            else if (Name == "ByTrigger")
                LifeByTrigger();

            else if (Name == "ByNotTrigger")
                LifeByTrigger(notLogic: true);

            else if (Name == "ByFood")
                LifeByFood();

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

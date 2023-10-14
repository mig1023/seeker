using System;
using System.Linq;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (!ModBySpecificName(Name))
                base.Do(Character.Protagonist);
        }

        private bool ModBySpecificName(string name)
        {
            if (name == "Advantage")
            {
                protagonist.Advantages.Add(ValueString);
            }
            else if (name == "Disadvantage")
            {
                protagonist.Disadvantages.Add(ValueString);
            }
            else if (name == "Dynamic")
            {
                DynamicBonuses(ValueString);
            }
            else if (name == "Static")
            {
                foreach (string param in ValueString.Split(','))
                {
                    if (param.Contains("="))
                    {
                        SetPropertyByLine(param);
                    }
                    else
                    {
                        Game.Option.Trigger(param.Trim());
                    }
                }
            }
            else if (name == "MusicalPerformance")
            {
                int lutnaBonus = ((Game.Option.IsTriggered("Лютня") &&
                    protagonist.Advantages.Contains("Музицирование")) ? 2 : 1);

                string[] values = ValueString.Split(',');

                protagonist.Heroism += int.Parse(values[0]) * lutnaBonus;
                protagonist.Inspiration += int.Parse(values[1]) * lutnaBonus;
                protagonist.Buffoonery += int.Parse(values[2]) * lutnaBonus;
            }
            else if (name == "Awakening")
            {
                protagonist.Villainy = 0;
                protagonist.Buffoonery = 5;
                protagonist.AbubakarOffer = 0;

                Game.Data.Triggers = Game.Data.Triggers.Intersect(Constants.SleepCleaningSurvive).ToList();
            }
            else
            {
                return false;
            }

            return true;
        }

        private void SetPropertyByLine(string line)
        {
            string[] keyValue = line.Split('=');
            int currentValue = GetProperty(protagonist, keyValue[0].Trim());
            int newValue = currentValue + int.Parse(keyValue[1].Trim());
            SetProperty(protagonist, keyValue[0].Trim(), newValue);
        }

        private void DynamicBonuses(string bonuses)
        {
            string[] bonus = bonuses.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

            if ((bonus[0] == "Advantage") && protagonist.Advantages.Contains(bonus[1]))
            {
                if (bonus[1].Contains("+"))
                {
                    foreach (string advantage in bonus[1].Split('+'))
                    {
                        if (!protagonist.Advantages.Contains(advantage.Trim()))
                            return;
                    }

                    SetPropertyByLine(bonus[2]);
                }
                else
                {
                    foreach (string advantage in bonus[1].Split(','))
                    {
                        if (protagonist.Advantages.Contains(advantage.Trim()))
                            SetPropertyByLine(bonus[2]);
                    }
                }
            }
            else if (bonus[0] == "Disadvantage")
            {
                foreach (string disadvantage in bonus[1].Split(','))
                {
                    if (protagonist.Disadvantages.Contains(disadvantage.Trim()))
                        SetPropertyByLine(bonus[2]);
                }
            }
            else if (bonus[0] == "Param")
            {
                int currentValue = GetProperty(protagonist, bonus[1]);
                int division = int.Parse(bonus[2]);

                for (int i = 0; i < (currentValue/division); i++)
                    SetPropertyByLine(bonus[3]);
            }
            else if ((bonus[0] == "Trigger") && Game.Option.IsTriggered(bonus[1]))
            {
                foreach(string trigger in bonus[1].Split(','))
                {
                    if (Game.Option.IsTriggered(trigger.Trim()))
                        SetPropertyByLine(bonus[2]);
                }
            }
        }
    }
}

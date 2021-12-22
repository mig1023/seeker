using System;

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
            int lutnaBonus = ((Game.Option.IsTriggered("Лютня") && Advantage("Музицирование")) ? 2 : 1);

            if (name == "Advantage")
            {
                protagonist.Advantages.Add(ValueString);
            }
            else if (name == "Disadvantage")
            {
                protagonist.Disadvantages.Add(ValueString);
            }
            else if ((name == "CrazyDance") && Advantage("Пение, Танец"))
            {
                protagonist.Heroism += 6;
                protagonist.Inspiration += 6;
            }
            else if (name == "RevoltInspiration")
            {
                protagonist.Heroism += AdvantagesBonus("Красноречие, Артистизм, Верховая езда, Предусмотрительность, Сражение", 1);

                if (Advantage("Популярность"))
                    protagonist.Heroism += 10;

                for(int i = 0; i < protagonist.Buffoonery; i++)
                    protagonist.Heroism += 10;

                for (int i = 0; i < protagonist.Inspiration; i++)
                    protagonist.Heroism += 5;

                for (int i = 0; i < (protagonist.Villainy / 6); i++)
                    protagonist.Heroism -= 10;

                if (Advantage("Жестокосердие"))
                    protagonist.Heroism -= 10;
            }
            else if (name == "SuccessInspiration")
            {
                protagonist.Heroism += AdvantagesBonus("Популярность, Пение, Танец, Красноречие," +
                    "Везение, Смекалка, Чревовещание, Наблюдательность, Интуиция, Эрудиция", 5);
            }
            else if (name == "MusicalPerformance")
            {
                string[] values = ValueString.Split(',');

                protagonist.Heroism += int.Parse(values[0]) * lutnaBonus;
                protagonist.Inspiration += int.Parse(values[1]) * lutnaBonus;
                protagonist.Buffoonery += int.Parse(values[2]) * lutnaBonus;
            }
            else if (name == "DynamicBonuses")
            {
                DynamicBonuses(ValueString);
            }
            else if (name == "StaticBonuses")
            {
                foreach (string param in ValueString.Split(','))
                {
                    if (param.Contains("="))
                        SetPropertyByLine(param);
                    else
                        Game.Option.Trigger(param.Trim());
                }
            }
            else
                return false;

            return true;
        }

        private void SetPropertyByLine(string line)
        {
            string[] keyValue = line.Split('=');
            int currentValue = GetProperty(protagonist, keyValue[0].Trim());
            SetProperty(protagonist, keyValue[0].Trim(), currentValue + int.Parse(keyValue[1].Trim()));
        }

        private void DynamicBonuses(string bonuses)
        {
            string[] bonus = bonuses.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

            if ((bonus[0] == "Advantage") && protagonist.Advantages.Contains(bonus[1]))
            {
                SetPropertyByLine(bonus[2]);
            }
            else if ((bonus[0] == "Disadvantage") && protagonist.Disadvantages.Contains(bonus[1]))
            {
                SetPropertyByLine(bonus[2]);
            }
            else if (bonus[0] == "Param")
            {
                int currentValue = GetProperty(protagonist, bonus[1]);
                int division = int.Parse(bonus[2]);

                for (int i = 0; i < (currentValue/division); i++)
                    SetPropertyByLine(bonus[3]);
            }
        }

        private bool Advantage(string advantages)
        {
            if (advantages.Contains(","))
            {
                foreach (string advantage in advantages.Split(','))
                    if (!protagonist.Advantages.Contains(advantage.Trim()))
                        return false;

                return true;
            }
            else if (advantages.Contains("|"))
            {
                foreach (string advantage in advantages.Split('|'))
                    if (protagonist.Advantages.Contains(advantage.Trim()))
                        return true;

                return false;
            }
            else
                return protagonist.Advantages.Contains(advantages);
        }

        private int AdvantagesBonus(string advantages, int bonus)
        {
            int total = 0;

            foreach (string advantage in advantages.Split(','))
                if (Advantage(advantage))
                    total += bonus;

            return total;
        }
    }
}

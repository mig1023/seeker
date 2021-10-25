using System;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (!ModBySpecificName(Name))
                base.Do(Character.Protagonist);
        }

        private bool ModBySpecificName(string name)
        {
            Character protagonist = Character.Protagonist;
            int lutnaBonus = (Game.Data.Triggers.Contains("Лютня") ? 2 : 1);

            if ((name == "CrazyDance") && Advantage("Пение, Танец"))
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
                protagonist.Heroism += 10 * lutnaBonus;
                protagonist.Inspiration += 2 * lutnaBonus;
                protagonist.Buffoonery += 5 * lutnaBonus;
            }
            else if (name == "MusicalPerformance2")
            {
                protagonist.Buffoonery += 1 * lutnaBonus;
            }
            else if (name == "MusicalPerformance3")
            {
                protagonist.Heroism += 6 * lutnaBonus;
                protagonist.Inspiration += 2 * lutnaBonus;
                protagonist.Buffoonery += 6 * lutnaBonus;
            }
            else if (name == "MusicalPerformance4")
            {
                protagonist.Heroism += 5 * lutnaBonus;
                protagonist.Inspiration += 2 * lutnaBonus;
                protagonist.Buffoonery += 5 * lutnaBonus;
            }
            else if (name == "MusicalPerformance5")
            {
                protagonist.Heroism += 2 * lutnaBonus;
            }
            else if (name == "MusicalPerformance6")
            {
                protagonist.Heroism += 3 * lutnaBonus;
            }
            else
                return false;

            return true;
        }

        private bool Advantage(string advantages)
        {
            Character protagonist = Character.Protagonist;

            if (advantages.Contains(","))
            {
                string[] advantagesList = advantages.Split(',');

                foreach (string advantage in advantagesList)
                    if (!protagonist.Advantages.Contains(advantage.Trim()))
                        return false;

                return true;
            }
            else if (advantages.Contains("|"))
            {
                string[] advantagesList = advantages.Split('|');

                foreach (string advantage in advantagesList)
                    if (protagonist.Advantages.Contains(advantage.Trim()))
                        return true;

                return false;
            }
            else
                return protagonist.Advantages.Contains(advantages);
        }

        private int AdvantagesBonus(string advantages, int bonus)
        {
            string[] advantagesList = advantages.Split(',');

            int total = 0;

            foreach (string advantage in advantagesList)
                if (Advantage(advantage))
                    total += bonus;

            return total;
        }
    }
}

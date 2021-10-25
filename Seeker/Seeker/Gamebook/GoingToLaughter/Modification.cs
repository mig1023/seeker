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

            if ((name == "CrazyDance") && Advantage("Пение, Танец"))
            {
                protagonist.Heroism += 6;
                protagonist.Inspiration += 6;
            }
            else if (name == "RevoltInspiration")
            {
                string[] advantagesList = { "Красноречие", "Артистизм", "Верховая езда", "Предусмотрительность", "Сражение" };

                foreach(string advantage in advantagesList)
                    if (Advantage(advantage))
                        protagonist.Heroism += 1;

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
    }
}

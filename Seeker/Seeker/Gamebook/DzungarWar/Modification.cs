using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.DzungarWar
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Empty { get; set; }
        public bool Init { get; set; }

        public void Do()
        {
            if ((Name == "SaleOfPredatorSkins") && Game.Data.Triggers.Contains("Хищник"))
                Character.Protagonist.Tanga += 150;
            else if (Name == "Favour")
            {
                if (Empty)
                    Character.Protagonist.Favour = null;
                else if (Init)
                    Character.Protagonist.Favour = 0;
                else
                    Character.Protagonist.Favour += Value;
            }
            else if (Name == "Danger")
            {
                if (Empty)
                    Character.Protagonist.Danger = null;
                else if (Character.Protagonist.Danger != null)
                    Character.Protagonist.Danger += Value;
            }
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                if (Empty || (currentValue < 0))
                    currentValue = 0;

                if ((currentValue > 12) && (Name != "Tanga"))
                    currentValue = 12;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}

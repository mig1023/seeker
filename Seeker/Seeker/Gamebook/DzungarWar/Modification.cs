using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.DzungarWar
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        public override void Do()
        {
            Character hero = Character.Protagonist;

            if ((Name == "SaleOfPredatorSkins") && Game.Data.Triggers.Contains("Хищник"))
                hero.Tanga += 150;

            else if (Name == "Favour")
            {
                if (Empty)
                    hero.Favour = null;

                else if (Init)
                    hero.Favour = 0;

                else
                    hero.Favour += Value;
            }
            else if (Name == "Danger")
            {
                if (Empty)
                    hero.Danger = null;

                else if (Character.Protagonist.Danger != null)
                    hero.Danger += Value;
            }
            else
                InnerDo(Character.Protagonist);
        }
    }
}

using System;

namespace Seeker.Gamebook.DzungarWar
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        public override void Do()
        {
            Character protagonist = Character.Protagonist;

            if (Name == "Bargain")
            {
                Game.Option.Trigger("Bargain");
            }
            else if ((Name == "SaleOfPredatorSkins") && Game.Option.IsTriggered("Хищник"))
            {
                protagonist.Tanga += 150;
            }
            else if (Name == "Favour")
            {
                if (Empty)
                {
                    protagonist.Favour = null;
                }
                else if (Init)
                {
                    protagonist.Favour = 0;
                }
                else
                {
                    protagonist.Favour += Value;
                }
            }
            else if (Name == "Danger")
            {
                if (Empty)
                    protagonist.Danger = null;

                else if (Character.Protagonist.Danger != null)
                    protagonist.Danger += Value;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

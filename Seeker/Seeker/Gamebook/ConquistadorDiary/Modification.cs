using System;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "BetIsMade")
            {
                protagonist.LastBet = protagonist.CurrentBet;
                protagonist.CurrentBet = 0;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

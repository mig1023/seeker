using System;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "BetIsMade")
            {
                Character.Protagonist.LastBet = Character.Protagonist.CurrentBet;
                Character.Protagonist.CurrentBet = 0;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

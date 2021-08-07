using System;

namespace Seeker.Gamebook.RockOfTerror
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        private static Character protogonist = Character.Protagonist;
        
        public override void Do()
        {
            if (Name == "MonksHeart")
            {
                if (Init && (protogonist.MonksHeart == null))
                    protogonist.MonksHeart = 0;

                else if (!Init && (protogonist.MonksHeart != null))
                    protogonist.MonksHeart += Value;
            }
            else
            {
                int currentValue = GetProperty(protogonist, Name);

                int injuryModificator = ((Name == "Time") && (protogonist.Injury > 0) ? 2 : 1);

                currentValue += Value * injuryModificator;

                SetProperty(protogonist, Name, currentValue);
            }
        }
    }
}

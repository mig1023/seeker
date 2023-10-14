using System;

namespace Seeker.Gamebook.RockOfTerror
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        private static Character protagonist = Character.Protagonist;
        
        public override void Do()
        {
            if (Name == "MonksHeart")
            {
                if (Init && (protagonist.MonksHeart == null))
                {
                    protagonist.MonksHeart = 0;
                }
                else if (!Init && (protagonist.MonksHeart != null))
                {
                    protagonist.MonksHeart += Value;
                }
            }
            else
            {
                int currentValue = GetProperty(protagonist, Name);

                int injuryModificator = ((Name == "Time") && (protagonist.Injury > 0) ? 2 : 1);

                currentValue += Value * injuryModificator;

                SetProperty(protagonist, Name, currentValue);
            }
        }
    }
}

using System;

namespace Seeker.Gamebook.RockOfTerror
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        public override void Do()
        {
            if (Name == "MonksHeart")
            {
                if (Init && (Character.Protagonist.MonksHeart == null))
                {
                    Character.Protagonist.MonksHeart = 0;
                }
                else if (!Init && (Character.Protagonist.MonksHeart != null))
                {
                    Character.Protagonist.MonksHeart += Value;
                }
            }
            else
            {
                int currentValue = GetProperty(Character.Protagonist, Name);

                int injuryModificator = ((Name == "Time") && (Character.Protagonist.Injury > 0) ? 2 : 1);

                currentValue += Value * injuryModificator;

                SetProperty(Character.Protagonist, Name, currentValue);
            }
        }
    }
}

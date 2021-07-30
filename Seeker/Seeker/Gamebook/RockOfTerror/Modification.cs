using System;

namespace Seeker.Gamebook.RockOfTerror
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protogonist = Character.Protagonist;
        public bool Init { get; set; }

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
                int currentValue = (int)protogonist.GetType().GetProperty(Name).GetValue(protogonist, null);

                int injuryModificator = ((Name == "Time") && (protogonist.Injury > 0) ? 2 : 1);

                currentValue += Value * injuryModificator;

                protogonist.GetType().GetProperty(Name).SetValue(protogonist, currentValue);
            }
        }
    }
}

using System.Collections.Generic;

namespace Seeker.Gamebook.StrikeBack
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<Character.SpecialTechniques, string> TechniquesNames() =>
            new Dictionary<Character.SpecialTechniques, string>
        {
            [Character.SpecialTechniques.RumbleKnife] = "Волшебный кинжал",
        };
    }
}

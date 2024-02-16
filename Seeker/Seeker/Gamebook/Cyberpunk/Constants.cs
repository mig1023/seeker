using System.Collections.Generic;

namespace Seeker.Gamebook.Cyberpunk
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<string, string> CharactersParams { get; set; }

        public static List<string> NormalizationParams { get; set; }
    }
}

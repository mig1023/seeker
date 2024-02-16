using System.Collections.Generic;

namespace Seeker.Gamebook.Genesis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<string, int> GetStartValues { get; set; }
    }
}

using System.Collections.Generic;

namespace Seeker.Gamebook.Genesis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, int> GetStartValues { get; set; }
    }
}

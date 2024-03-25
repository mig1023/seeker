using System.Collections.Generic;

namespace Seeker.Gamebook.Insight
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, int> GetStartValues { get; set; }
    }
}

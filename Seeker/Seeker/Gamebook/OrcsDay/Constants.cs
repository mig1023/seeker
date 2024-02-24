using System.Collections.Generic;

namespace Seeker.Gamebook.OrcsDay
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, string> StatNames { get; set; }

        public static Dictionary<string, string> ResultCalculation { get; set; }

        public static Dictionary<string, string> Orcishness { get; set; }
    }
}

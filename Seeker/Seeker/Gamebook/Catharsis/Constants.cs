using System.Collections.Generic;

namespace Seeker.Gamebook.Catharsis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, int> GetStartValues { get; set; }
    }
}

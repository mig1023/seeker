using System.Collections.Generic;

namespace Seeker.Gamebook.WalkInThePark
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static int FirstPartSize = 100;
        public static int GameoverRating = 200;

        public static List<string> What { get; set; }
        public static List<string> Where { get; set; }

        public static Dictionary<string, string> Rating { get; set; }
    }
}

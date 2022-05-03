using System.Collections.Generic;

namespace Seeker.Gamebook.PrairieLaw
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<int, int> Skills() => new Dictionary<int, int>
        {
            [2] = 8,
            [3] = 10,
            [4] = 12,
            [5] = 9,
            [6] = 11,
            [7] = 9,
            [8] = 10,
            [9] = 8,
            [10] = 9,
            [11] = 10,
            [12] = 11
        };

        public static Dictionary<int, int> Strengths() => new Dictionary<int, int>
        {
            [2] = 22,
            [3] = 20,
            [4] = 16,
            [5] = 18,
            [6] = 20,
            [7] = 20,
            [8] = 16,
            [9] = 24,
            [10] = 22,
            [11] = 18,
            [12] = 20
        };

        public static Dictionary<int, int> Charms() => new Dictionary<int, int>
        {
            [2] = 8,
            [3] = 6,
            [4] = 5,
            [5] = 8,
            [6] = 6,
            [7] = 7,
            [8] = 7,
            [9] = 7,
            [10] = 6,
            [11] = 7,
            [12] = 5
        };

        public static Dictionary<int, string> LuckList() => new Dictionary<int, string>
        {
            [1] = "①",
            [2] = "②",
            [3] = "③",
            [4] = "④",
            [5] = "⑤",
            [6] = "⑥",

            [11] = "❶",
            [12] = "❷",
            [13] = "❸",
            [14] = "❹",
            [15] = "❺",
            [16] = "❻",
        };

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            Availability = Actions.StaticInstance.Availability,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}

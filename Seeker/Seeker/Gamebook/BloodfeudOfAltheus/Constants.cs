using System.Collections.Generic;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<int, string> HealthLine() => new Dictionary<int, string>
        {
            [0] = "мертв",
            [1] = "тяжело ранен",
            [2] = "ранен",
            [3] = "здоров",
        };

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}

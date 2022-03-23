using System.Collections.Generic;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 30, 60, 90 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 205 };

        public static Dictionary<string, string> StatNames = new Dictionary<string, string>
        {
            ["Strength"] = "силы",
            ["Skill"] = "ловкости",
            ["Wisdom"] = "мудрости",
            ["Cunning"] = "хитрости",
            ["Oratory"] = "красноречия",
            ["Popularity"] = "популярности",
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

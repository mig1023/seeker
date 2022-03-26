using System.Collections.Generic;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 556, 557, 558, 559 };

        public static Dictionary<string, Character.SpecializationType> GetSpecializationType()
        {
            return new Dictionary<string, Character.SpecializationType>
            {
                ["ВОИН"] = Character.SpecializationType.Warrior,
                ["МАГ"] = Character.SpecializationType.Wizard,
                ["МЕТАТЕЛЬ"] = Character.SpecializationType.Thrower,
            };
        }

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

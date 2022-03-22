using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#e0e7f2",
            [ColorTypes.ActionBox] = "#7988a4",
            [ColorTypes.StatusBar] = "#364d77",
            [ColorTypes.AdditionalStatus] = "#5e7092",
            [ColorTypes.AdditionalFont] = "#ced4de",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 556, 557, 558, 559 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 556, 557, 558, 559 };

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

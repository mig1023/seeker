using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#151515",
            [ButtonTypes.Action] = "#3f3f3f",
            [ButtonTypes.Option] = "#696969",
            [ButtonTypes.System] = "#f9f9f9",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#2a2a2a",
            [ColorTypes.BookColor] = "#151515",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 619 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 619 };

        public override int? GetParagraphsStatusesLimit() => 620;

        public static List<string> StaticSpells() => new List<string>
        {
            "ЗАКЛЯТИЕ КОПИИ", "ЗАКЛЯТИЕ СИЛЫ", "ЗАКЛЯТИЕ СЛАБОСТИ"
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

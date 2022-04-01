using System.Collections.Generic;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 619 };

        public override int? GetParagraphsStatusesLimit() => 620;

        public static List<string> StaticSpells() => new List<string>
        {
            "ЗАКЛЯТИЕ КОПИИ", "ЗАКЛЯТИЕ СИЛЫ", "ЗАКЛЯТИЕ СЛАБОСТИ"
        };

        public override Dictionary<string, string> ButtonText() => new Dictionary<string, string>
        {
            ["Fight"] = "Сражаться",
            ["Luck"] = "Проверить удачу",
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

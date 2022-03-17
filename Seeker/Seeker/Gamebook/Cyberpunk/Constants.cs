using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Cyberpunk
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#118984",
            [ButtonTypes.Action] = "#0c5c58",
            [ButtonTypes.Option] = "#79bcb9",
            [ButtonTypes.Continue] = "#79bcb9",
            [ButtonTypes.System] = "#66b3af",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.BookColor] = "#118984",
            [ColorTypes.StatusBar] = "#094542",
        };

        public static Dictionary<string, string> CharactersParams() => new Dictionary<string, string>
        {
            ["Planning"] = "Планирование",
            ["Preparation"] = "Подготовка",
            ["Luck"] = "Везение",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 587, 588, 589 };

        public override string ButtonText() => "Пройти проверку";

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

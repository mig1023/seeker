using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.OrcsDay
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#a4be84",
            [ButtonTypes.Continue] = "#cad9b7",
            [ButtonTypes.Action] = "#72855c",
            [ButtonTypes.System] = "#cad9b7",
            [ButtonTypes.Font] = "#000000",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Font] = "#000000",
            [ColorTypes.SystemFont] = "#000000",
            [ColorTypes.ActionBox] = "#c8d8b5",
            [ColorTypes.BookColor] = "#a4be84",
            [ColorTypes.StatusBar] = "#72855c",
            [ColorTypes.AdditionalStatus] = "#c8d8b5",
            [ColorTypes.BookFontColor] = "#000000",
        };

        public static Dictionary<string, string> StatNames() => new Dictionary<string, string>
        {
            ["Muscle"] = "мышцы",
            ["Wits"] = "мозги",
            ["Courage"] = "смелость",
            ["Luck"] = "удачу",
            ["Orcishness"] = "оркишность",
        };

        public static Dictionary<string, string> ResultCalculation() => new Dictionary<string, string>
        {
            ["!Гибель"] = "+1 за то, что ты ещё жив",
            ["Уже есть"] = "+1 за то, что обрёл имя",
            ["Выбрался из подземелья"] = " +1 за то, то выбрался из подземелья",
            ["Приключенец"] = "+1 за то, что стал искателем приключений",
            ["Бетани"] = "+1 за то, что узнал, как зовут девушку-рабыню",
            ["Поцелуй"] = "+1 за то, что тебя поцеловала девушка-рабыня",
            ["Галрос Бессмертный"] = "+1 за то, что одолел Галроса",
            ["Мортимер Нечихающий"] = "+1 за то, что одолел Мортимера",
            ["Тёмный Властелин"] = "+1 за то, что стал новым Темным Властелином",
            ["Ограблен"] = "-1 за то, что был ограблен",
            ["Раб своей природы"] = "-1 за то, что стал рабом своей оркской натуры",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 102, 103 };

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

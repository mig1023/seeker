using System.Collections.Generic;

namespace Seeker.Gamebook.Moonrunner
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<int, string> SpellsList() => new Dictionary<int, string>
        {
            [1] = "первое",
            [2] = "второе",
            [3] = "третье",
            [4] = "четвёртое",
            [5] = "пятое",
            [6] = "шестое",
        };

        public static List<string> Skills() => new List<string>
        {
            "Акробатика",
            "Скалолазание",
            "Сражение",
            "Интеллект",
            "Маскировка",
            "Взлом",
            "Ловкость рук",
            "Скрытность",
            "Поиск следов",
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

using System.Collections.Generic;

namespace Seeker.Gamebook.MissionToUrpan
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<string, int> ResultCalculation() => new Dictionary<string, int>
        {
            ["Рана"] = -10,
            ["Телохранитель"] = 15,
            ["Проблемы с законом"] = 10,
            ["Поломка"] = -20,
            ["Два"] = -15,
            ["Пилот боевого корабля"] = 20,
            ["Миротворец"] = 10,
            ["Задание выполнено"] = 20,
            ["Лучший финал"] = 30,
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

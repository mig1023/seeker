using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.MissionToUrpan
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#0d1a36",
        };

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

        public override bool ShowDisabledOption() => true;

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

using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Genesis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4c687c",
            [ButtonTypes.Option] = "#6f8696",
            [ButtonTypes.Action] = "#445d6f",
            [ButtonTypes.Continue] = "#849fb3",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#293342",
            [ColorTypes.Font] = "#b6cbd8",
            [ColorTypes.StatusBar] = "#3c5363",
            [ColorTypes.ActionBox] = "#69707a",
            [ColorTypes.BookColor] = "#202b41",
        };

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Skill"] = 30,
            ["Weapon"] = 15,
            ["Stealth"] = 3,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 201, 202, 203, 204, 205, 206, 207 };

        public override bool ShowDisabledOption() => true;

        public override string GetDescription() => "Планета Ледхом, станция буровиков." +
            " В тот день заканчивалось ледхомское лето, и стояла обычная для этого вре" +
            "мени года погода – минус тридцать градусов, яркое слепящее солнце и посте" +
            "пенно усиливающаяся метель. Станция жила обычной рутиной, извлекая из нед" +
            "р планеты ценную руду. Внезапно, молчавший месяцами приемник ожил и на св" +
            "язь со станцией вышел космический корабль. Сигнал, пришедший от него, ока" +
            "зался просьбой разрешить вынужденную посадку. По его структуре стало ясно" +
            ", что он послан бортовым компьютером, скорее всего в автоматическом режим" +
            "е. Дежурный по станции отправил запрос о цели прибытия и причинах посадки" +
            ", но ответа не последовало.\n\nВскоре стало ясно, что происходит нечто ст" +
            "ранное – появившийся в атмосфере корабль заходил на посадку по крутой бал" +
            "листической орбите. Компьютер идентифицировал его как транспортный грузов" +
            "оз микарда – расы нейтральных к землянам инопланетян. Космопорт на Ледхом" +
            "е был всего один, рассчитанный максимум на два корабля среднего класса, п" +
            "оэтому транспортнику едва хватило бы на нем места. Грузовоз промахнулся м" +
            "имо космопорта, всего на несколько градусов, но и этого хватило для катас" +
            "трофы.";

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
        };
    }
}

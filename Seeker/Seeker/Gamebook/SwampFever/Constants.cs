using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.SwampFever
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#5c6649",
            [ButtonTypes.Action] = "#5a6546",
            [ButtonTypes.Option] = "#5c6649",
            [ButtonTypes.Continue] = "#8c937f",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#485432",
            [ColorTypes.Font] = "#eaece8",
            [ColorTypes.ActionBox] = "#707a60",
            [ColorTypes.StatusBar] = "#34411c",
            [ColorTypes.BookColor] = "#ff557c48",
        };

        public static Dictionary<int, string> GetRangeTypes()
        {
            return new Dictionary<int, string>
            {
                [6] = "ДАЛЬНЯЯ ДИСТАНЦИЯ",
                [5] = "СРЕДНЯЯ ДИСТАНЦИЯ",
                [4] = "БЛИЖНЯЯ ДИСТАНЦИЯ",
            };
        }

        public static Dictionary<int, Dictionary<string, string>> GetUpgrates()
        {
            return new Dictionary<int, Dictionary<string, string>>
            {
                [1] = new Dictionary<string, string>
                {
                    ["name"] = "SecondEngine",
                    ["output"] = "Второй двигатель",
                },
                [2] = new Dictionary<string, string>
                {
                    ["name"] = "Stealth",
                    ["output"] = "Стелс-покрытие",
                },
                [3] = new Dictionary<string, string>
                {
                    ["name"] = "Radar",
                    ["output"] = "Радар",
                },
                [4] = new Dictionary<string, string>
                {
                    ["name"] = "CircularSaw",
                    ["output"] = "Циркулярная пила",
                },
                [5] = new Dictionary<string, string>
                {
                    ["name"] = "Flamethrower",
                    ["output"] = "Реактивный огнемёт",
                },
                [6] = new Dictionary<string, string>
                {
                    ["name"] = "PlasmaCannon",
                    ["output"] = "Спаренная плазмопушка",
                },
            };
        }

        public static List<string> GetActionParams() => new List<string>
        {
            "EnemyName", "EnemyCombination", "Level", "Birds"
        };

        public static List<string> GetModsParams() => new List<string>
        {
            "Name", "Value", "Multiplication", "Division"
        };

        public override string GetDescription() => "Стигия — третья планета в системе Сигма Вермес. Когда-то Солнечная Конфедерация пыталась её колонизировать, но была вынуждена отступить перед враждебной средой. Свинцовая пелена, едкие испарения, засасывающие воронки, омега-излучение, опасные твари сделали планету невозможной для освоения. Лишь космопорт на плато является безопасным местом для нахождения. Космопорт (он же Порт Грёз) продолжает свою работу, потому что на планете встречается ценное растение, названное Стигоном. Оно слабо изучено и не подаётся искусственному выращиванию. Стигон меняет свою форму в зависимости от возраста. В его побегах содержится эпиморфин, применяемый в фармацевтике новейшего поколения.\n\nВы — один из отчаянных искателей Стигона, авантюрист и космический старатель.Подписав контракт с компанией «Манекенгроу», вы получили право на бесплатное посещение и месячное проживание в Порту Грёз. Вам также достался харвестер (специальная машина для сбора и хранения полезных ископаемых). В ответ вы обязуетесь реализовывать добытый Стигон только через фирму.";

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

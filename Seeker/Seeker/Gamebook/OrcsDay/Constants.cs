using System.Collections.Generic;

namespace Seeker.Gamebook.OrcsDay
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

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
            ["Обрёл имя"] = "+1 за то, что обрёл имя",
            ["Выбрался из подземелья"] = " +1 за то, что выбрался из подземелья",
            ["Кузнец своей судьбы"] = " +1 за то, что стал кузнецом своей судьбы",
            ["Приключенец"] = "+1 за то, что стал искателем приключений",
            ["Вместе с ней, !Девушка погибла"] = "+1 за то, что девушка-рабыня осталась с тобой и в живых",
            ["Бетани"] = "+1 за то, что узнал, как зовут девушку-рабыню",
            ["Поцелуй"] = "+1 за то, что тебя поцеловала девушка-рабыня",
            ["Галрос Бессмертный"] = "+1 за то, что одолел Галроса",
            ["Мортимер Нечихающий"] = "+1 за то, что одолел Мортимера",
            ["Тёмный Властелин"] = "-1 за то, что стал новым Темным Властелином",
            ["Ограблен"] = "-1 за то, что был ограблен",
            ["Раб своей природы"] = "-1 за то, что стал рабом своей оркской натуры",
        };

        public override Dictionary<string, string> ButtonText() => new Dictionary<string, string>
        {
            ["Fight"] = "Сражаться",
            ["Test"] = "Проверить",
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

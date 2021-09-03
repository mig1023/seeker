using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.DzungarWar
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#93684f",
            [ButtonTypes.Action] = "#8c7254",
            [ButtonTypes.Option] = "#a88672",
            [ButtonTypes.Continue] = "#b99e8e",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#f1e6d1",
            [ColorTypes.ActionBox] = "#af8f69",
            [ColorTypes.StatusBar] = "#dcc18c",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.AdditionalStatus] = "#ebdcbe",
            [ColorTypes.BookColor] = "#533818",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 658, 659, 660 };

        public static List<string> GetActionParams() => new List<string>
        {
            "RemoveTrigger", "Stat", "TriggerTestPenalty", "Level", "StatStep", "StatToMax"
        };

        public static List<string> GetModsParams() => new List<string>
        {
            "Name", "Value", "Empty", "Init"
        };

        public static Dictionary<string, string> StatNames() => new Dictionary<string, string>
        {
            ["Strength"] = "силы",
            ["Skill"] = "ловкости",
            ["Wisdom"] = "мудрости",
            ["Cunning"] = "хитрости",
            ["Oratory"] = "красноречия",
            ["Danger"] = "опасности",
        };

        public override bool ShowDisabledOption() => true;

        public override string GetDescription() => "В начале XVIII выходцы из монгольских степей джунгары стали стремительно захватывать всё новые и новые земли Азии. Ещё жива память об успехах Атиллы-гунна, Чингисхана да Хромого Тимура (Тамерлана). Неужели и джунгарским правителям удастся повторить опыт своих предшественников или даже превзойти их? А может быть кочевники Средней Азии сумеют раз и навсегда остановить набеги монгольских варваров? Всё это зависит только от ВАС.\n\nАлдар Косе, весельчак и балагур, герой казахских сказок, известный нам по книге-игре 'Приключения Безбородого Обманщика' вновь встаёт на защиту своей Родины.До сих пор ему удавалось бороться со злом и несправедливостью.Но теперь нашему герою противостоят не жадные богачи, а хитроумные джунгарские правители и военачальники. Удастся ли Алдару Косе пройти через земли, наводнённые врагами и выполнить задание казахского хана? Любовь и ненависть, дружба и предательство, герои и негодяи, кровавые стычки и дипломатия – всё это ожидает вас на страницах книги.";

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

using System.Collections.Generic;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<string, string> IncompatiblesDisadvantages => new Dictionary<string, string>
        {
            ["Пьянство"] = "Набожность",
            ["Алчность"] = "Набожность",
            ["Суеверие"] = "Набожность",
            ["Похоть"] = "Набожность",
            ["Жестокосердие"] = "Набожность",
            ["Азартность"] = "Набожность",
            ["Невезение"] = "Везение",
            ["Рассеянность"] = "Наблюдательность",
            ["Обжорство"] = "Эквилибристика, Акробатика",
            ["Малодушие"] = "Вспыльчивость",
        };

        public static Dictionary<string, string> ParamNames() => new Dictionary<string, string>
        {
            ["Heroism"] = "Героизму",
            ["Villainy"] = "Злодейству",
            ["Buffoonery"] = "Шутовству",
            ["Inspiration"] = "Вдохновению",
        };

        public static List<string> SleepCleaningSurvive() => new List<string>
        {
            "Дежа вю", "Се ля ви", "Шерше ля фам",
            "Триумфатор", "Лютня", "Записная книжка", "Бревиарий",
        };

        public override bool ShowDisabledOption() => true;

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 1393, 1394 };

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

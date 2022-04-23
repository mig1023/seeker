using System.Collections.Generic;

namespace Seeker.Gamebook.Cyberpunk
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<string, string> CharactersParams() => new Dictionary<string, string>
        {
            ["Planning"] = "Планирование",
            ["Preparation"] = "Подготовка",
            ["Luck"] = "Везение",
            ["Cybernetics"] = "Кибернетика",
            ["Morality"] = "Мораль",
            ["Careerism"] = "Карьеризм",
            ["BlackMarket"] = "Чёрный рынок",
            ["Clan"] = "Клан",
            ["Selfcontrol25"] = "Самообладание 25",
            ["Selfcontrol50"] = "Самообладание 50",
            ["Selfcontrol75"] = "Самообладание 75",
            ["Selfcontrol100"] = "Самообладание 100",
        };

        public static List<string> NormalizationParams() => new List<string> { "Planning", "Preparation", "Luck" };

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

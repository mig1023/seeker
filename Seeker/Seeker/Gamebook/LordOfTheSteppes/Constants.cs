using System.Collections.Generic;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Output.Interface.TextFontSize GetFontSize() =>
            Output.Interface.TextFontSize.Little;

        public static Dictionary<string, int> GetStartValues { get; set; }

        public static Dictionary<Character.SpecialTechniques, string> TechniquesNames() =>
            new Dictionary<Character.SpecialTechniques, string>
        {
            [Character.SpecialTechniques.TwoBlades] = "Бой двумя клинками",
            [Character.SpecialTechniques.TotalProtection] = "Веерная защита",
            [Character.SpecialTechniques.FirstStrike] = "Первый удар",
            [Character.SpecialTechniques.PowerfulStrike] = "Мощный выпад",
            [Character.SpecialTechniques.Reaction] = "Реакция",
            [Character.SpecialTechniques.IgnoreReaction] = "Игнорирует прием Реакции",
            [Character.SpecialTechniques.IgnoreFirstStrike] = "Игнорирует прием Первый удар",
            [Character.SpecialTechniques.IgnorePowerfulStrike] = "Игнорирует прием Мощный выпад",
            [Character.SpecialTechniques.ExtendedDamage] = "Каждый удар отнимает 3 Жизни",
            [Character.SpecialTechniques.PoisonBlade] = "Отравленный клинок",
        };

        public static Dictionary<Character.FightStyles, string> FightStyles() =>
            new Dictionary<Character.FightStyles, string>
        {
            [Character.FightStyles.Aggressive] = "агрессивный",
            [Character.FightStyles.Counterattacking] = "контратакующий",
            [Character.FightStyles.Defensive] = "оборонительный",
            [Character.FightStyles.Fullback] = "глухую защиту",
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

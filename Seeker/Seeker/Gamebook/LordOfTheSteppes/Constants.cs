using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#b80f0a",
            [ButtonTypes.Action] = "#a92605",
            [ButtonTypes.Option] = "#878787",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#b42806",
            [ColorTypes.StatusFont] = "#ffffff",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => String.Empty;

        public Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.little;

        public double? GetLineHeight() => 1.20;

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Attack"] = 8,
            ["Defence"] = 15,
            ["Endurance"] = 14,
            ["Initiative"] = 10,
        };

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 904, 905, 906 };

        public static Dictionary<Character.SpecialTechniques, string> TechniquesNames() => new Dictionary<Character.SpecialTechniques, string>
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

        public static Dictionary<Character.FightStyles, string> FightStyles() => new Dictionary<Character.FightStyles, string>
        {
            [Character.FightStyles.Aggressive] = "агрессивный",
            [Character.FightStyles.Counterattacking] = "контратакующий",
            [Character.FightStyles.Defensive] = "оборонительный",
            [Character.FightStyles.Fullback] = "глухую защиту",
        };
    }
}

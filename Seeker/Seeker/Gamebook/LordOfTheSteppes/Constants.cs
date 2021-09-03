using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#b80f0a",
            [ButtonTypes.Action] = "#a92605",
            [ButtonTypes.Option] = "#878787",
            [ButtonTypes.Continue] = "#db8784",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#b42806",
            [ColorTypes.StatusFont] = "#ffffff",
            [ColorTypes.BookColor] = "#b80f0a",
        };

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.little;

        public override double? GetLineHeight() => 1.20;

        public override bool JustifyText() => true;

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Attack"] = 8,
            ["Defence"] = 15,
            ["Endurance"] = 14,
            ["Initiative"] = 10,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 904, 905, 906 };

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

        public static List<string> GetActionParams() => new List<string>
        {
            "Stat", "StatStep", "RoundsToWin", "WoundsToWin", "Coherence",
            "Dices", "NotToDeath", "Odd", "Initiative", "StoneGuard"
        };

        public static List<string> GetCharacterParams() => new List<string>
        {
            "Name", "MaxAttack", "MaxEndurance", "MaxDefence", "MaxInitiative"
        };

        public static List<string> GetModsParams() => new List<string>
        {
            "Name", "Value", "ValueString", "Restore", "Empty"
        };

        public override string GetDescription() => "Беда надвигается на Полесье - земли русичей. Все чаще с юга приходят вести о налётах на мирные деревни банд степняков, оставляющих после себя сожжённые дома и разлученные семьи. И это не обычные лихие разбойники или жаждущие наживы бандиты, а регулярные отряды степняков. Что случилось в южном государстве, торговые отношения с которым ещё недавно приносили только прибыль? Что делать разрозненным городам русичей в случае войны, которая кажется неминуемой?\n\nПеред вами книга-игра - книга, в которой читатель сам принимает участие в происходящих событиях и решает, как будет развиваться сюжет.И сюжет этот может привести не только к счастливому концу - все зависит от самого читателя, который станет героем сказочного мира, где мужественному, но разрозненному народу русичей угрожает нашествие воинственных степняков. А чтобы в конце все жили долго и счастливо, придется воспользоваться смекалкой и запастись терпением. А ещё вам понадобятся два игральных кубика, листок бумаги, карандаш и ластик.";

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

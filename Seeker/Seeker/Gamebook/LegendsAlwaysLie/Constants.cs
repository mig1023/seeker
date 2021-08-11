using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#911",
            [ButtonTypes.Action] = "#ba2020",
            [ButtonTypes.Option] = "#cc8888",
            [ButtonTypes.Continue] = "#cc8888",
            [ButtonTypes.System] = "#e6c5c5",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#f5e7e5",
            [ColorTypes.StatusBar] = "#870808",
            [ColorTypes.AdditionalStatus] = "#b70b0b",
            [ColorTypes.AdditionalFont] = "#ffffff",
        };

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.little;

        public override double? GetLineHeight() => 1.20;

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 687, 714, 715, 701, 702 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 714, 715, 716 };

        public static Dictionary<string, string> GetActionParams() => new Dictionary<string, string>
        {
            ["ConneryAttacks"] = "string",
            ["ReactionWounds"] = "string",
            ["ReactionRound"] = "string",
            ["ReactionHit"] = "string",
            ["Dices"] = "int",
            ["DiceBonus"] = "int",
            ["OnlyRounds"] = "int",
            ["RoundsToWin"] = "int",
            ["AttackWounds"] = "int",
            ["Disabled"] = "bool",
            ["IncrementWounds"] = "bool",
            ["GolemFight"] = "bool",
            ["ZombieFight"] = "bool",
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
        };
    }
}

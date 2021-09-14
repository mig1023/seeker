using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#3662ae",
            [ButtonTypes.Action] = "#203a68",
            [ButtonTypes.Option] = "#9ab0d6",
            [ButtonTypes.Continue] = "#9ab0d6",
            [ButtonTypes.System] = "#ccd7ea",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#e0e7f2",
            [ColorTypes.ActionBox] = "#7988a4",
            [ColorTypes.StatusBar] = "#364d77",
            [ColorTypes.AdditionalStatus] = "#5e7092",
            [ColorTypes.AdditionalFont] = "#ced4de",
            [ColorTypes.BookColor] = "#3662ae",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 556, 557, 558, 559 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 556, 557, 558, 559 };

        public static List<string> GetActionParams() => new List<string>
        {
            "OnlyRounds", "RoundsToWin", "RoundsWinToWin", "ThisIsSpell", "Regeneration", "ReactionFight",
            "Poison", "OnlyOne", "TailAttack", "IncrementWounds", "ThreeWoundLimit", "Invincible", "Wound",
            "Dices", "EvenWound", "WoundsLimit", "DeathLimit", "ReactionWounds"
        };

        public static List<string> GetModsParams() => new List<string>
        {
            "Name", "Value", "ValueString", "Empty", "Restore"
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

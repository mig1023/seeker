using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#145334",
            [ButtonTypes.Action] = "#104229",
            [ButtonTypes.Option] = "#42755c",
            [ButtonTypes.Continue] = "#7d9188",
            [ButtonTypes.System] = "#9db6aa",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#c9d7d0",
            [ColorTypes.StatusBar] = "#0e3b24",
        };

        public static List<string> GetActionParams() => new List<string> {
            "WoundsToWin", "RoundsToWin", "RoundsToFight", "Ophidiotaur", "ManicBeast", "GiantHornet"
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

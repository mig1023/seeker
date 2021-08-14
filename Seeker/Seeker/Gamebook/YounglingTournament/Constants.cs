using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.YounglingTournament
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#c0ac6c",
            [ButtonTypes.Continue] = "#d6c078",
            [ButtonTypes.System] = "#f9f9f9",
            [ButtonTypes.Action] = "#c0a23b",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#e8d17e",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.ActionBox] = "#e6d9ae",
        };

        public override bool ShowDisabledOption() => true;

        public static List<string> GetActionParams() => new List<string>
        {
            "Level"
        };

        public static List<string> GetEnemyParams() => new List<string>
        {
            "Name", "MaxHitpoints", "Accuracy", "Shield"
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

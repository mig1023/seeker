using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.RockOfTerror
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#191919",
            [ButtonTypes.Action] = "#2a2a2a",
            [ButtonTypes.Option] = "#494949",
            [ButtonTypes.Continue] = "#2f2f2f",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#000000",
            [ColorTypes.Font] = "#FFFFFF",
            [ColorTypes.StatusBar] = "#151515",
        };
    }
}

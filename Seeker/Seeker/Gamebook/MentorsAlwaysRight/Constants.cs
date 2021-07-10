using System;
using System.Collections.Generic;
using System.Text;

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
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#e0e7f2",
            [ColorTypes.ActionBox] = "#7988a4",
            [ColorTypes.StatusBar] = "#364d77",
            [ColorTypes.AdditionalStatus] = "#5e7092",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 556, 557 };
    }
}

﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;

namespace Seeker.Gamebook.LandOfUnwaryBears
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#9e003a",
            [ButtonTypes.System] = "#f9f9f9",
        };

        public override string GetFont() => "RobotoFont";

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.small;

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

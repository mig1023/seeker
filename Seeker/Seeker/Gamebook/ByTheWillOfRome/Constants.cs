﻿using System.Collections.Generic;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override bool ShowDisabledOption() => true;

        public override Dictionary<string, string> ButtonText() => new Dictionary<string, string>
        {
            ["Get"] = "Купить",
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
            Debug = Character.Protagonist.Debug,
        };
    }
}

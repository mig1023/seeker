﻿using System.Collections.Generic;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<string, string> TextByYears { get; set; }

        public static Links GetLinks() => new Links
        {
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
        };
    }
}

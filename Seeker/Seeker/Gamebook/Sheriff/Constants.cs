﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Sheriff
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#7d2c1c",
            [ButtonTypes.Option] = "#b45b27",
            [ButtonTypes.Continue] = "#b45b27",
            [ButtonTypes.System] = "#f0d1b9",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.BookColor] = "#7d2c1c",
        };

        public static List<string> CleaningNotebookList() => new List<string>
        {
            "Билли убит в амбаре",
            "Все любили Билли",
            "Убит ночью",
            "Не успел защититься",
            "У убийцы есть рука",
            "Убийца женщина",
            "Надо проверить салон мадам Жу-жу",
            "Женская перчатка",
            "Дробовик",
            "Патроны",
            "Портсигар",
        };

        public static Dictionary<string, int> Levels() => new Dictionary<string, int>
        {
            ["Easy"] = 100,
            ["Medium"] = 1,
            ["Hard"] = 0,
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

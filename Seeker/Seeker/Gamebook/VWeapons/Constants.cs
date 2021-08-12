﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.VWeapons
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#FFFFFF",
            [ButtonTypes.Action] = "#FFFFFF",
            [ButtonTypes.Option] = "#FFFFFF",
            [ButtonTypes.Font] = "#000000",
            [ButtonTypes.Border] = "#000000",
            [ButtonTypes.Continue] = "#FFFFFF",
            [ButtonTypes.System] = "#f4f4f4",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.ActionBox] = "#efefef",
            [ColorTypes.StatusBar] = "#FFFFFF",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.StatusBorder] = "#000000",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 266 };

        public static Dictionary<string, string> healingParts = new Dictionary<string, string>
        {
            ["головы"] = "Head",
            ["плеч"] = "ShoulderGirdle",
            ["корпуса"] = "Body",
            ["рук"] = "Hands",
            ["ног"] = "Legs",
        };

        public static List<string> GetActionParams() => new List<string>
        {
            "Dogfight", "DamagedWeapon", "Value", "Time"
        };

        public static List<string> GetEnemyParams() => new List<string>
        {
            "Name", "Hitpoints", "Accuracy", "First", "WithoutCartridges", "Animal"
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

﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.YounglingTournament
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#e8d17e",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.ActionBox] = "#e6d9ae",
            [ColorTypes.BookColor] = "#c0ac6c",
        };

        public override bool ShowDisabledOption() => true;

        public static int GetMaxTechniqueValue() => 4;

        public static Dictionary<Character.SwordTypes, string> SwordSkillsNames() => new Dictionary<Character.SwordTypes, string>
        {
            [Character.SwordTypes.Decisiveness] = "Решительности",
            [Character.SwordTypes.Elasticity] = "Эластичности",
            [Character.SwordTypes.Rivalry] = "Соперничества",
            [Character.SwordTypes.Perseverance] = "Настойчивости",
            [Character.SwordTypes.Aggressiveness] = "Агрессивности",
            [Character.SwordTypes.Confidence] = "Уверенности",
            [Character.SwordTypes.Vaapad] = "Ваапад",
            [Character.SwordTypes.JarKai] = "Джар-Кай",
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

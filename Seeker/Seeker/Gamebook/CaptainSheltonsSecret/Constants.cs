using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#b5cde1",
            [ColorTypes.ActionBox] = "#8fb3f2",
            [ColorTypes.StatusBar] = "#0a5c96",
            [ColorTypes.BookColor] = "#4682B4",
        };

        public static Dictionary<int, int> Masterys() => new Dictionary<int, int>
        {
            [2] = 8,
            [3] = 10,
            [4] = 12,
            [5] = 9,
            [6] = 11,
            [7] = 9,
            [8] = 10,
            [9] = 8,
            [10] = 9,
            [11] = 10,
            [12] = 11
        };

        public static Dictionary<int, int> Endurances() => new Dictionary<int, int>
        {
            [2] = 22,
            [3] = 20,
            [4] = 16,
            [5] = 18,
            [6] = 20,
            [7] = 20,
            [8] = 16,
            [9] = 24,
            [10] = 22,
            [11] = 18,
            [12] = 20
        };

        public static Dictionary<int, string> LuckList() => new Dictionary<int, string>
        {
            [1] = "①",
            [2] = "②",
            [3] = "③",
            [4] = "④",
            [5] = "⑤",
            [6] = "⑥",

            [11] = "❶",
            [12] = "❷",
            [13] = "❸",
            [14] = "❹",
            [15] = "❺",
            [16] = "❻",
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

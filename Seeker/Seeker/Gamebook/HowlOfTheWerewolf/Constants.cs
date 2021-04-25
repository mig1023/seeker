using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#383e3b",
            [ButtonTypes.Action] = "#516f72",
            [ButtonTypes.Option] = "#696969",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#253130",
            [ColorTypes.AdditionalStatus] = "#a8b7b8",
            [ColorTypes.ActionBox] = "#a8b7b8",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => String.Empty;

        public Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public double? GetLineHeight() => null;

        public static Dictionary<int, string> GetCountName() => new Dictionary<int, string>
        {
            [1] = "Первый",
            [2] = "Второй",
            [3] = "Третий",
            [4] = "Четвёртый",
            [5] = "Пятый",
            [6] = "Шестой",
        };

        public static Dictionary<int, string> GetPassageName() => new Dictionary<int, string>
        {
            [1] = "дверь",
            [2] = "первое окно",
            [3] = "второе окно",
        };

        public static int GetUlrichMastery() => 8;

        public static int GetVanRichtenMastery() => 10;

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };
    }
}

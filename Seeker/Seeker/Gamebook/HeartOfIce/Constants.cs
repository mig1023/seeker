using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.HeartOfIce
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#23a2a1",
            [ButtonTypes.Option] = "#91d0d0",
            [ButtonTypes.Action] = "#629f9e",
            [ButtonTypes.Continue] = "#bde2e2",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#d9eeee",
            [ColorTypes.ActionBox] = "#7bc7c6",
            [ColorTypes.StatusBar] = "#1f9190",
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

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 454, 455, 456 };
    }
}

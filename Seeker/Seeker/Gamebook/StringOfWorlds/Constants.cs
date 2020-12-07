using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Constants : Abstract.IConstants
    {
        private Random random = new Random();

        private static string LastButtonColor = String.Empty; 

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#990066",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            if (type == ButtonTypes.Border)
                return String.Empty;
            else if (type == ButtonTypes.Font)
                return BlackOrWhite();
            else
                return RandomColor();
        }

        private string RandomColor()
        {
            LastButtonColor = String.Format("#{0:X6}", random.Next(0x1000000));
            return LastButtonColor;
        }

        private string BlackOrWhite()
        {
            List<int> rgb = new List<int>();

            for (int i = 1; i < 6; i += 2)
                rgb.Add(Convert.ToInt32(new string(new char[] { LastButtonColor[i], LastButtonColor[i+1] }), 16));

            return (((rgb[0] * 0.299) + (rgb[1] * 0.587) + (rgb[2] * 0.114)) > 186 ? "#000000" : "#FFFFFF" );
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => String.Empty;

        public bool GetLtlFont() => false;

        public double? GetLineHeight() => null;
    }
}

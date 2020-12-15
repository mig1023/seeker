using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Constants : Abstract.IConstants
    {
        private static int ColorShiftAmount = 55;

        private static Random random = new Random();

        private static string LastButtonColor = String.Empty;
        private static List<int> LastColor = new List<int>();
        private static int ShiftFactor = 1;

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#990066",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            if ((type == ButtonTypes.Border) || (type == ButtonTypes.Continue))
                return String.Empty;
            else if (type == ButtonTypes.Font)
                return (((LastColor[0] * 0.299) + (LastColor[1] * 0.587) + (LastColor[2] * 0.114)) > 186 ? "#000000" : "#FFFFFF");
            else
                return NextColor();
        }

        public static void RandomColor()
        {
            ShiftFactor = random.Next(3);

            LastColor.Clear();

            for (int i = 0; i < 3; i++)
                LastColor.Add(random.Next(256));
        }

        private string NextColor()
        {
            LastColor[ShiftFactor] += (ColorShiftAmount * (LastColor[ShiftFactor] <= 200 ? 1 : -1));

            Color myColor = Color.FromArgb(LastColor[0], LastColor[1], LastColor[2]);

            return myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
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

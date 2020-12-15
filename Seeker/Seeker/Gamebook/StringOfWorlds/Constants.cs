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
        private static Random random = new Random();

        private static string LastButtonColor = String.Empty;
        private static List<int> LastColor = new List<int>();
        private static int ChangeFactor = 1;

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#990066",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            if ((type == ButtonTypes.Border) || (type == ButtonTypes.Continue))
                return String.Empty;
            else if (type == ButtonTypes.Font)
                return BlackOrWhite();
            else
                return NextColor();
        }

        public static string RandomColor()
        {
            LastButtonColor = String.Format("#{0:X6}", random.Next(0x1000000));

            ChangeFactor = random.Next(3);

            LastColor.Clear();

            for (int i = 1; i < 6; i += 2)
                LastColor.Add(Convert.ToInt32(new string(new char[] { LastButtonColor[i], LastButtonColor[i + 1] }), 16));

            return LastButtonColor;
        }

        private string NextColor()
        {
            List<int> nextColor = new List<int>(LastColor);

            nextColor[ChangeFactor] += random.Next(70) - 35;
            nextColor[ChangeFactor] = Normalization(nextColor[ChangeFactor]);

            Color myColor = Color.FromArgb(nextColor[0], nextColor[1], nextColor[2]);

            return myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        }

        private static int Normalization(int param)
        {
            if (param < 0)
                param = 0;

            if (param > 255)
                param = 255;

            return param;
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

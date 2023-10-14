using System.Collections.Generic;
using System.Drawing;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Services
    {
        public static int SubColor(int color, int value)
        {
            if (color >= value)
            {
                return color - value;
            }
            else
            {
                return 0;
            }
        }

        public static List<int> ModColors(List<int> color)
        {
            color[0] = SubColor(color[0], 7);
            color[1] = SubColor(color[1], 6);
            color[2] = SubColor(color[2], 6);

            return color;
        }

        public static string HexColor(int r, int g, int b)
        {
            Color myColor = Color.FromArgb(r, g, b);
            return myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        }

        public static string СontrastBorder(List<int> color, List<int> button) =>
            color[0] > 24 ? HexColor(button[0], button[1], button[2]) : "#234249";

        public static string СontrastText(List<int> color) =>
            color[0] > 66 ? "#082126" : "#cfd9db";
    }
}

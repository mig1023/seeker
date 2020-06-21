using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Buttons
    {
        static Random rand = new Random();

        static List<string> ButtonsColors = new List<string>
        {
            "#3CB371",
            "#2E8B57",
            "#228B22",
            "#008000",
            "#006400",
            "#6B8E23",
            "#556B2F",
            "#20B2AA",
            "#008B8B",
            "#008080",
            "#4682B4",
            "#1E90FF",
            "#6495ED",
            "#7B68EE",
            "#4169E1",
            "#191970",
            "#696969",
            "#008080",
            "#800080",
            "#BA55D3",
            "#8A2BE2",
            "#9400D3",
            "#9932CC",
            "#800080",
            "#4B0082",
            "#6A5ACD",
            "#483D8B",
            "#C71585",
        };

        public static string NextColor()
        {
            return ButtonsColors[rand.Next(ButtonsColors.Count)];
        }
    }
}

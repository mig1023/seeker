using System;
using System.Collections.Generic;
using System.Drawing;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        private static int ColorShiftAmount = 45;
        private static List<int> LastColor = new List<int>();
        private static List<int> StatusColor = null;
        private static int ShiftFactor = 0;
        private static bool ShiftDirectionUp = true;

        private static Random random = new Random();

        public override string GetColor(ButtonTypes type)
        {
            if (Game.Settings.IsEnabled("WithoutStyles"))
            {
                return base.GetColor(type);
            }
            else if ((type == ButtonTypes.Border) || (type == ButtonTypes.Continue) || (type == ButtonTypes.System))
            {
                return String.Empty;
            }
            else if (type == ButtonTypes.ButtonFont)
            {
                return СontrastColor(LastColor);
            }
            else
            {
                return NextColor();
            }
        }

        public static void RandomColor()
        {
            LastColor.Clear();

            for (int i = 0; i < 3; i++)
                LastColor.Add(random.Next(256));

            ShiftFactor = random.Next(3);

            StatusColor = new List<int>(LastColor);
        }

        private static string NextColor()
        {
            ShiftDirectionUp = ShiftDirectionCheck();

            LastColor[ShiftFactor] += (ColorShiftAmount * (ShiftDirectionUp ? 1 : -1));

            return HexColor(LastColor[0], LastColor[1], LastColor[2]);
        }

        private static bool ShiftDirectionCheck()
        {
            if (ShiftDirectionUp)
            {
                return LastColor[ShiftFactor] < (255 - ColorShiftAmount);
            }
            else
            {
                return LastColor[ShiftFactor] < ColorShiftAmount;
            }
        }

        private static string HexColor(int r, int g, int b)
        {
            Color myColor = Color.FromArgb(r, g, b);

            return myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        }

        public override string GetColor(Game.Data.ColorTypes type) 
        {
            if (Game.Settings.IsEnabled("WithoutStyles"))
            {
                return base.GetColor(type);
            }
            else if (type == ColorTypes.StatusBar)
            {
                return HexColor(StatusColor[0], StatusColor[1], StatusColor[2]);
            }
            else if (type == ColorTypes.StatusFont)
            {
                return СontrastColor(StatusColor);
            }
            else
            {
                return String.Empty;
            }
        }

        private string СontrastColor(List<int> color) =>
            ((color[0] * 0.299) + (color[1] * 0.587) + (color[2] * 0.114)) > 186 ? "#000000" : "#FFFFFF";

        public static Dictionary<int, int> Skills { get; set; }

        public static Dictionary<int, int> Strengths { get; set; }

        public static Dictionary<int, int> Charms { get; set; }

        public static Dictionary<int, string> LuckList { get; set; }
    }
}

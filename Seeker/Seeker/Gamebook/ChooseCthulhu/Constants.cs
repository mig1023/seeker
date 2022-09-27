using System.Collections.Generic;
using System.Drawing;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        private static List<int> BackgroudColor = null;
        private static List<int> MainButtonColor = null;

        public static bool InitColor()
        {
            if (BackgroudColor == null)
                BackgroudColor = new List<int> { 129, 147, 147 };
            else
                ModColors(ref BackgroudColor);

            if (MainButtonColor == null)
                MainButtonColor = new List<int> { 0, 47, 62 };
            else
                ModColors(ref MainButtonColor);

            return true;
        }

        private static bool CheckColorInit() =>
            (BackgroudColor == null) || (MainButtonColor == null) ? InitColor() : Game.Services.DoNothing();

        private static int SubColor(int color, int value)
        {
            if (color >= value)
                return color - value;

            else
                return 0;
        }

        private static void ModColors(ref List<int> color)
        {
            color[0] = SubColor(color[0], 7);
            color[1] = SubColor(color[1], 6);
            color[2] = SubColor(color[2], 6);
        }

        public override string GetColor(ButtonTypes type)
        {
            CheckColorInit();

            if (Game.Settings.IsEnabled("WithoutStyles"))
                return base.GetColor(type);

            else if ((type == ButtonTypes.Main) || (type == ButtonTypes.Option) || (type == ButtonTypes.System))
                return HexColor(MainButtonColor[0], MainButtonColor[1], MainButtonColor[2]);

            else if (type == ButtonTypes.Border)
                return СontrastBorder(BackgroudColor);

            else
                return base.GetColor(type);
        }

        public override string GetColor(ColorTypes type)
        {
            CheckColorInit();

            if (Game.Settings.IsEnabled("WithoutStyles"))
                return base.GetColor(type);

            else if (type == ColorTypes.Background)
                return HexColor(BackgroudColor[0], BackgroudColor[1], BackgroudColor[2]);

            else if (type == ColorTypes.Font)
                return СontrastText(BackgroudColor);

            else
                return base.GetColor(type);
        }

        private static string HexColor(int r, int g, int b)
        {
            Color myColor = Color.FromArgb(r, g, b);
            return myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        }

        private string СontrastBorder(List<int> color) =>
            color[0] > 24 ? HexColor(MainButtonColor[0], MainButtonColor[1], MainButtonColor[2]) : "#234249";

        private string СontrastText(List<int> color) =>
            color[0] > 66 ? "#082126" : "#cfd9db";

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            Availability = Actions.StaticInstance.Availability,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}

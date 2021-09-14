using System.Collections.Generic;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Constants
    {
        public static string BACK_LINK = "НАЗАД";
        public static string DISCLAIMER_LINK = "➝ подробнее";

        public static Color BACKGROUND = Color.FromHex("#f7f7f7");
        public static Color LINK_COLOR_DEFAULT = Color.Black;
        public static Color SPLITTER_COLOR_DEFAULT = Color.FromHex("#bdbdbd");

        public static double BIG_FONT = 25;
        public static double STATUSBAR_FONT = 12;
        public static double LINE_HEIGHT = 1.20;
        public static double ACTIONPLACE_SPACING = 5;
        public static double ACTIONPLACE_PADDING = 20;
        public static double TEXT_LABEL_MARGIN = 5;
        public static double BORDER_WIDTH = 1;
        public static double REPRESENT_PADDING = -10;
        public static double SPLITTER_HIGHT = 25;
        public static double DISCLAIMER_BORDER = 8;

        public static List<string> FONT_SIZE_SETTING = new List<string>
        {
            "В зависимости от игры",
            "Очень мелкий",
            "Мелкий",
            "Нормальный",
            "Крупный",
        };

        public static Dictionary<int, double> FONT_SIZE_VALUES = new Dictionary<int, double>
        {
            [1] = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            [2] = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            [3] = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            [4] = Constants.BIG_FONT,
        };

        public static List<string> JUSTYFY_SETTING = new List<string>
        {
            "Выкл",
            "Вкл",
        };

        public static List<string> OPTION_SETTING = new List<string>
        {
            "В зависимости от игры",
            "Всегда отображать",
            "Всегда скрывать",
        };
    }
}

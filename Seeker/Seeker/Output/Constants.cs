using System.Collections.Generic;
using Xamarin.Forms;
using static Seeker.Output.Interface;

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
        public static double SYS_MENU_SPACING = 4;
        public static double SYS_MENU_HIGHT = 25;

        public static Dictionary<TextFontSize, double> FontSize = new Dictionary<TextFontSize, double>
        {
            [TextFontSize.micro] = Font(NamedSize.Micro),
            [TextFontSize.small] = Font(NamedSize.Small),
            [TextFontSize.little] = Font(NamedSize.Medium),
            [TextFontSize.normal] = Font(NamedSize.Large),
            [TextFontSize.nope] = Font(NamedSize.Large),
            [TextFontSize.big] = BIG_FONT,
        };

        public static Dictionary<TextFontSize, double> FontSizeItalic = new Dictionary<TextFontSize, double>
        {
            [TextFontSize.micro] = Font(NamedSize.Micro),
            [TextFontSize.small] = Font(NamedSize.Micro),
            [TextFontSize.little] = Font(NamedSize.Small),
            [TextFontSize.normal] = Font(NamedSize.Small),
            [TextFontSize.nope] = Font(NamedSize.Small),
            [TextFontSize.big] = Font(NamedSize.Large),
        };

        public static List<string> FONT_TYPE_SETTING = new List<string>
        {
            "Yanone",
            "Roboto",
        };

        public static Dictionary<int, string> FONT_TYPE_VALUES = new Dictionary<int, string>
        {
            [0] = "YanoneFont",
            [1] = "RobotoFont",
        };

        public static List<string> FONT_SIZE_SETTING = new List<string>
        {
            "Настройки игры",
            "Очень мелкий",
            "Мелкий",
            "Нормальный",
            "Крупный",
        };

        public static Dictionary<int, double> FONT_SIZE_VALUES = new Dictionary<int, double>
        {
            [1] = Interface.Font(NamedSize.Small),
            [2] = Interface.Font(NamedSize.Medium),
            [3] = Interface.Font(NamedSize.Large),
            [4] = Constants.BIG_FONT,
        };

        public static List<string> ON_OFF_SETTING = new List<string>
        {
            "Выкл",
            "Вкл",
        };

        public static List<string> OPTION_SETTING = new List<string>
        {
            "Настройки игры",
            "Отображать",
            "Скрывать",
        };

        public static List<string> SORT_SETTING = new List<string>
        {
            "По умолчанию",
            "По размеру",
        };
    }
}

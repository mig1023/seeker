using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;
using System.Linq;
using System;

namespace Seeker.Gamebook.SeaTales
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static List<string> Throws { get; set; }

        public static Dictionary<int, string> Colors { get; set; }

        public static Dictionary<string, string> Properties { get; set; }

        public static List<string> SuccessButtons { get; set; }

        public static List<string> FailButtons { get; set; }

        public static Dictionary<int, string> GetRangeTypes { get; set; }

        public static Dictionary<int, string> GetBattleTypes { get; set; }

        public static Dictionary<int, string> NumTexts { get; set; }

        public static Dictionary<int, string> AllNumTexts { get; set; }

        public static int StoryPart()
        {
            int part = 0;
            int current = Game.Data.CurrentParagraphID;

            foreach (int startParagraph in Colors.Keys.OrderBy(x => x))
            {
                if (current >= startParagraph)
                    part += 1;
            }

            return part > 0 ? part : 1;
        }

        private bool ColorsList(out List<string> colors)
        {
            colors = null;

            if (Game.Settings.IsEnabled("WithoutStyles"))
                return false;

            int currentStart = GetCurrentStartParagraph(Colors);

            if (currentStart <= 0)
                return false;

            string colorsLine = Constants.Colors[currentStart];

            colors = colorsLine
                .Split(',')
                .Select(x => x.Trim())
                .ToList();

            return true;
        }

        public override string GetColor(ButtonTypes type)
        {
            List<string> colorsList = null;

            if (!ColorsList(out colorsList))
                return base.GetColor(type);

            Dictionary<ButtonTypes, string> colors = new Dictionary<ButtonTypes, string>
            {
                [ButtonTypes.Main] = colorsList[0],
                [ButtonTypes.Option] = colorsList[1],
                [ButtonTypes.Continue] = colorsList[1],
                [ButtonTypes.System] = colorsList[2],
                [ButtonTypes.Action] = colorsList[3],
            };

            if (colors.ContainsKey(type))
                return colors[type];
            else
                return base.GetColor(type);
        }

        public override string GetColor(ColorTypes type)
        {
            List<string> colorsList = null;

            if (!ColorsList(out colorsList))
                return base.GetColor(type);

            Dictionary<ColorTypes, string> colors = new Dictionary<ColorTypes, string>
            {
                [ColorTypes.StatusBar] = colorsList[4],
                [ColorTypes.ActionBox] = colorsList[5],
                [ColorTypes.Background] = colorsList[6],
                [ColorTypes.AdditionalStatus] = colorsList[7],
            };

            if (colors.ContainsKey(type))
                return colors[type];
            else
                return base.GetColor(type);
        }
    }
}

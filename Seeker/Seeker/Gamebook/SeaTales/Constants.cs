﻿using System.Collections.Generic;
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

        private static int FirstPartSize = 400;

        public static bool ThisIsFirstPart() =>
            Game.Data.CurrentParagraphID <= FirstPartSize;

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
            };

            if (colors.ContainsKey(type))
                return colors[type];
            else
                return base.GetColor(type);
        }
    }
}

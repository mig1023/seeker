using System;
using System.Collections.Generic;
using static Seeker.Output.Buttons;
using System.Linq;

namespace Seeker.Gamebook.ChooseYourOwnAdventure
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, string> Buttons { get; set; }

        public static int MillionerStartParagraph = 120;

        public static int GetCurrentStartParagraph()
        {
            int start = 0;
            int current = Game.Data.CurrentParagraphID;

            foreach (int startParagraph in Constants.Buttons.Keys.OrderBy(x => x))
            {
                if (current >= startParagraph)
                    start = startParagraph;
            }

            return start;
        }

        public override string GetColor(ButtonTypes type)
        {
            int currentStart = GetCurrentStartParagraph();

            if (((type == ButtonTypes.Main) || (type == ButtonTypes.Action)) && currentStart > 0)
                return Constants.Buttons[currentStart];
            else
                return base.GetColor(type);
        }
    }
}

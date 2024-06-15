using System;
using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;
using System.Linq;

namespace Seeker.Gamebook.Nightmare
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, string> Buttons { get; set; }

        public override string GetColor(ButtonTypes type)
        {
            if (type == ButtonTypes.Main)
            {
                string currentColor = String.Empty;

                foreach (int startParagraph in Constants.Buttons.Keys.OrderBy(x => x))
                {
                    if (Game.Data.CurrentParagraphID >= startParagraph)
                        currentColor = Constants.Buttons[startParagraph];
                }

                return currentColor;
            }
            else
            {
                return base.GetColor(type);
            }
        }
    }
}

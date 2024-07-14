using System.Collections.Generic;
using static Seeker.Output.Buttons;

namespace Seeker.Gamebook.ChooseYourOwnAdventure
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, string> Buttons { get; set; }

        public static int MillionerStartParagraph = 120;

        public override string GetColor(ButtonTypes type)
        {
            if (Game.Settings.IsEnabled("WithoutStyles"))
                return base.GetColor(type);

            int currentStart = GetCurrentStartParagraph(Buttons);

            if (((type == ButtonTypes.Main) || (type == ButtonTypes.Action)) && currentStart > 0)
                return Constants.Buttons[currentStart];
            else
                return base.GetColor(type);
        }
    }
}

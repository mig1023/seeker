using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Checks
    {
        public static bool ExistsInParagraph(string actionName = "", string actionText = "", string optionText = "")
        {
            if (Game.Data.CurrentParagraph.Actions != null)
                foreach (Abstract.IActions action in Game.Data.CurrentParagraph.Actions)
                {
                    if (!String.IsNullOrEmpty(actionName) && action.ActionName.ToUpper().Contains(actionName.ToUpper()))
                        return true;

                    if (!String.IsNullOrEmpty(actionText) && action.ButtonName.ToUpper().Contains(actionText.ToUpper()))
                        return true;
                }

            if (Game.Data.CurrentParagraph.Options != null)
                foreach (Option option in Game.Data.CurrentParagraph.Options)
                    if (!String.IsNullOrEmpty(optionText) && option.Text.ToUpper().Contains(optionText.ToUpper()))
                        return true;

            return false;
        }

    }
}

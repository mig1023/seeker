using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Checks
    {
        public static bool ExistsInParagraph(string actionName = "", string actionText = "", string optionText = "")
        {
            if ((!String.IsNullOrEmpty(actionName) || !String.IsNullOrEmpty(actionText)) && (Game.Data.CurrentParagraph.Actions != null))
                foreach (Abstract.IActions action in Game.Data.CurrentParagraph.Actions)
                {
                    if (action.ActionName.ToUpper().Contains(actionName.ToUpper()))
                        return true;

                    if (action.ButtonName.ToUpper().Contains(actionText.ToUpper()))
                        return true;
                }

            if (!String.IsNullOrEmpty(optionText) && (Game.Data.CurrentParagraph.Options != null))
                foreach (Option option in Game.Data.CurrentParagraph.Options)
                    if (option.Text.ToUpper().Contains(optionText.ToUpper()))
                        return true;

            return false;
        }

    }
}

using System;

namespace Seeker.Game
{
    class Buttons
    {
        public static bool ExistsInParagraph(string actionName = "", string actionText = "", string optionText = "")
        {
            if (Data.CurrentParagraph.Actions != null)
                foreach (Abstract.IActions action in Data.CurrentParagraph.Actions)
                {
                    if (!String.IsNullOrEmpty(actionName) && action.Type.ToUpper().Contains(actionName.ToUpper()))
                        return true;

                    if (!String.IsNullOrEmpty(actionText) && action.Button.ToUpper().Contains(actionText.ToUpper()))
                        return true;
                }

            if (Data.CurrentParagraph.Options != null)
            {
                foreach (Option option in Data.CurrentParagraph.Options)
                {
                    if (!String.IsNullOrEmpty(optionText) && option.Text.ToUpper().Contains(optionText.ToUpper()))
                        return true;
                }
            }

            return false;
        }

        public static void Disable(string name) =>
            Data.DisableMethod(name);

        public static void Disable(bool okResult, string good, string bad) =>
            Data.DisableMethod(okResult ? bad : good);
    }
}

using System;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("!") && (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim())))
                        return false;

                    else if (!oneOption.Contains("!") && (!Game.Option.IsTriggered(oneOption.Trim())))
                        return false;
                }

                return true;
            }
        }
    }
}

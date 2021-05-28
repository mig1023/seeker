using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.PensionerSimulator
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override bool CheckOnlyIf(string option)
        {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("!") && (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim())))
                            return false;

                    else if (!oneOption.Contains("!") && (!Game.Data.Triggers.Contains(oneOption.Trim())))
                        return false;
                }

                return true;
        }
    }
}

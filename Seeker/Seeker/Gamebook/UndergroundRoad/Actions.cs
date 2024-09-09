using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.UndergroundRoad
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() =>
            new List<string> { $"Ранений: {Character.Protagonist.Wounds}" };

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("РАНЕН ДВАЖДЫ или НЕТ СЕРДЦА"))
            {
                bool heart = Game.Option.IsTriggered("Голубое каменное сердце");
                bool wounded = Character.Protagonist.Wounds > 1;
                return !heart || wounded;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    string opt = oneOption.Trim();

                    if (opt.Contains("РАНЕН"))
                    {
                        if ((opt == "!РАНЕН ДВАЖДЫ") && (Character.Protagonist.Wounds > 1))
                        {
                            return false;
                        }
                        else if ((opt == "!РАНЕН") && (Character.Protagonist.Wounds > 0))
                        {
                            return false;
                        }
                        else if ((opt == "РАНЕН ДВАЖДЫ") && Character.Protagonist.Wounds < 2)
                        {
                            return false;
                        }
                        else if ((opt == "РАНЕН") && Character.Protagonist.Wounds < 1)
                        {
                            return false;
                        }
                    }
                    else if (!AvailabilityTrigger(opt))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}

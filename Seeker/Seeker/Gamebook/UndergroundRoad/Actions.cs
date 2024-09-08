using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.UndergroundRoad
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() =>
            new List<string> { $"Ранений: {Character.Protagonist.Wounds}" };

        private bool SilverHeart() =>
            Game.Option.IsTriggered("Голубое каменное сердце");

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("!РАНЕН ДВАЖДЫ и ЕСТЬ СЕРДЦЕ"))
            {
                return SilverHeart() && Character.Protagonist.Wounds < 2;
            }
            else if (option.Contains("РАНЕН ДВАЖДЫ или НЕТ СЕРДЦА"))
            {
                return !SilverHeart() || Character.Protagonist.Wounds > 1;
            }
            else if (option.Contains("!РАНЕН ДВАЖДЫ"))
            {
                return Character.Protagonist.Wounds < 2;
            }
            else if (option.Contains("!РАНЕН"))
            {
                return Character.Protagonist.Wounds < 1;
            }
            else if (option.Contains("РАНЕН ДВАЖДЫ"))
            {
                return Character.Protagonist.Wounds == 2;
            }
            else if (option.Contains("РАНЕН"))
            {
                return Character.Protagonist.Wounds > 0;
            }
            else
            {
                return base.Availability(option);
            }
        }
    }
}

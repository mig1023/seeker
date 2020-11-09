﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.RockOfTerror
{
    class Modification : Interfaces.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Init { get; set; }

        public void Do()
        {
            if (Name == "MonksHeart")
            {
                if (Init && (Character.Protagonist.MonksHeart == null))
                    Character.Protagonist.MonksHeart = 0;

                else if (!Init && (Character.Protagonist.MonksHeart != null))
                    Character.Protagonist.MonksHeart += Value;
            }
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                int injuryModificator = ((Name == "Time") && (Character.Protagonist.Injury > 0) ? 2 : 1);

                currentValue += Value * injuryModificator;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Modification : Interfaces.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }
        public bool Empty { get; set; }
        public bool Init { get; set; }

        public void Do()
        {
            if (Name == "PopularityByTime")
            {
                if ((Character.Protagonist.UnitOfTime > 2) && (Character.Protagonist.Popularity > 0))
                    Character.Protagonist.Popularity -= 1;
                else if (Character.Protagonist.UnitOfTime == 1)
                    Character.Protagonist.Popularity += 1;
            }
            else if ((Name == "AkynGlory") || (Name == "UnitOfTime"))
            {
                int? currentValue = (int?)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                if (Empty)
                    currentValue = null;
                else if (Init && (Name == "AkynGlory"))
                    currentValue = (Game.Data.OpenedOption.Contains("PartyClothes") ? 1 : 0);
                else if (Init && (Name == "UnitOfTime"))
                    currentValue = 4;
                else
                    currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                if (Empty || (currentValue < 0))
                    currentValue = 0;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }

        }
    }
}

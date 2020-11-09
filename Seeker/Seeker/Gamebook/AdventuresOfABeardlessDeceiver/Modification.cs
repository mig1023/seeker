using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Modification : Interfaces.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
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
            else if (Name == "AkynGlory")
            {
                if (Empty)
                    Character.Protagonist.AkynGlory = null;
                else if (Init)
                    Character.Protagonist.AkynGlory = (Game.Data.Triggers.Contains("PartyClothes") ? 1 : 0);
                else
                    Character.Protagonist.AkynGlory += Value;
            }
            else if (Name == "UnitOfTime")
            {
                if (Empty)
                    Character.Protagonist.UnitOfTime = null;
                else if (Init)
                    Character.Protagonist.UnitOfTime = 4;
                else
                    Character.Protagonist.UnitOfTime += Value;
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

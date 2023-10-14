using System;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        public override void Do()
        {
            Character protagonist = Character.Protagonist;

            if (Name == "PopularityByTime")
            {
                if ((protagonist.UnitOfTime > 2) && (protagonist.Popularity > 0))
                {
                    protagonist.Popularity -= 1;
                }
                else if (protagonist.UnitOfTime == 1)
                {
                    protagonist.Popularity += 1;
                }
            }
            else if (Name == "AkynGlory")
            {
                if (Empty)
                {
                    protagonist.AkynGlory = null;
                }
                else if (Init)
                {
                    protagonist.AkynGlory = (Game.Option.IsTriggered("PartyClothes") ? 1 : 0);
                }
                else
                {
                    protagonist.AkynGlory += Value;
                }
            }
            else if (Name == "UnitOfTime")
            {
                if (Empty)
                {
                    protagonist.UnitOfTime = null;
                }
                else if (Init)
                {
                    protagonist.UnitOfTime = 4;
                }
                else
                {
                    protagonist.UnitOfTime += Value;
                }
                    
            }
            else
            {
                int currentValue = GetProperty(protagonist, Name);

                currentValue += Value;

                if (Empty || (currentValue < 0))
                    currentValue = 0;

                SetProperty(protagonist, Name, currentValue);
            }
        }
    }
}

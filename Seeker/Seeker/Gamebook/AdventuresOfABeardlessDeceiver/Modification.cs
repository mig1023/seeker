using System;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        public override void Do()
        {
            Character hero = Character.Protagonist;

            if (Name == "PopularityByTime")
            {
                if ((hero.UnitOfTime > 2) && (hero.Popularity > 0))
                    hero.Popularity -= 1;

                else if (hero.UnitOfTime == 1)
                    hero.Popularity += 1;
            }
            else if (Name == "AkynGlory")
            {
                if (Empty)
                    hero.AkynGlory = null;
                else if (Init)
                    hero.AkynGlory = (Game.Data.Triggers.Contains("PartyClothes") ? 1 : 0);
                else
                    hero.AkynGlory += Value;
            }
            else if (Name == "UnitOfTime")
            {
                if (Empty)
                    hero.UnitOfTime = null;
                else if (Init)
                    hero.UnitOfTime = 4;
                else
                    hero.UnitOfTime += Value;
            }
            else
            {
                int currentValue = (int)hero.GetType().GetProperty(Name).GetValue(hero, null);

                currentValue += Value;

                if (Empty || (currentValue < 0))
                    currentValue = 0;

                hero.GetType().GetProperty(Name).SetValue(hero, currentValue);
            }
        }
    }
}

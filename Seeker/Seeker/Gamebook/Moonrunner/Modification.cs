using System;

namespace Seeker.Gamebook.Moonrunner
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Oblivion")
            {
                foreach (string skill in Constants.Skills)
                    Game.Option.Trigger(skill, remove: true);
            }
            else if (Name == "GoldOffer")
            {
                Character.Protagonist.Gold -= Character.Protagonist.Offer;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

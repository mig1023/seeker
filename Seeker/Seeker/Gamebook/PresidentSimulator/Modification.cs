using System;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Year")
            {
                Character.Protagonist.Year += 1;
                Character.Protagonist.InnerYear += 1;
            }
            else if (Name == "ModsByArmyForces")
            {
                Character.Protagonist.Rating += Game.Dice.Roll(Character.Protagonist.Army) + 1;
            }
            else if (Name == "PeopleAreCallingBack")
            {
                Character.Protagonist.Rating = 89 + Game.Dice.Roll(size: 11);
                Character.Protagonist.ArmyLoyalty = 89 + Game.Dice.Roll(size: 11);
                Character.Protagonist.BusinessLoyalty = 89 + Game.Dice.Roll(size: 11);
                Character.Protagonist.Money = 5;
                Character.Protagonist.InnerYear = 1979;
                Game.Option.Trigger("БегствоКапиталов", remove: true);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

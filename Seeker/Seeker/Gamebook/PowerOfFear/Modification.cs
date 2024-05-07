using System;

namespace Seeker.Gamebook.PowerOfFear
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Constants.PropertiesNames.ContainsKey(Name) && (Value == 0))
            {
                int current = GetProperty(Character.Protagonist, Name);

                if (current == 0)
                {
                    SetProperty(Character.Protagonist, Name, 6);
                }
                else if (current < 10)
                {
                    SetProperty(Character.Protagonist, Name, current + 1);
                }
            }
            else if (Name == "HitpointsRestore")
            {
                Character.Protagonist.Hitpoints = 10;
            }
            else if (Name == "HitpointsLoss")
            {
                if (Character.Protagonist.Strength == 0)
                    Character.Protagonist.Hitpoints -= 2;
            }
            else if (Name == "HitpointsBySkills")
            {
                Character.Protagonist.Hitpoints += (Character.Protagonist.Strength > 0 ? 2 : 0);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

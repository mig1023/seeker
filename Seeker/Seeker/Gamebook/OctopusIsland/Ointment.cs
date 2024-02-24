using System;

namespace Seeker.Gamebook.OctopusIsland
{
    class Ointment
    {
         public static int Cure(int protagonistHitpoint)
        {
            while ((Character.Protagonist.LifeGivingOintment > 0) && (protagonistHitpoint < 20))
            {
                Character.Protagonist.LifeGivingOintment -= 1;
                protagonistHitpoint += 1;
            }

            return protagonistHitpoint;
        }
    }
}

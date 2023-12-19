using System;

namespace Seeker.Gamebook.OctopusIsland
{
    class Ointment
    {
        private static Character protagonist = Character.Protagonist;

        public static int Cure(int protagonistHitpoint)
        {
            while ((protagonist.LifeGivingOintment > 0) && (protagonistHitpoint < 20))
            {
                protagonist.LifeGivingOintment -= 1;
                protagonistHitpoint += 1;
            }

            return protagonistHitpoint;
        }
    }
}

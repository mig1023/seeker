using System;

namespace Seeker.Gamebook.VWeapons
{
    class Checks
    {
        public static bool Special()
        {
            bool mt = Game.Option.IsTriggered("Mt");
            bool p = Game.Option.IsTriggered("P");
            bool b = Game.Option.IsTriggered("B");

            return (mt || p) && !b && Character.Protagonist.Suspicions <= 3;
        }

        public static bool SpecialF() =>
            ((Character.Protagonist.Suspicions >= 4) && !Game.Option.IsTriggered("F"));
    }
}

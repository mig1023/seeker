using System;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Resurrection
    {
        public static bool IsPosible()
        {
            bool normal = (Character.Protagonist.Resurrection > 0);
            bool glory = (Character.Protagonist.Glory - Character.Protagonist.Shame) >= 10;
            bool brooch = (Character.Protagonist.BroochResurrection > 0) && glory;

            return (normal || brooch);
        }
    }
}

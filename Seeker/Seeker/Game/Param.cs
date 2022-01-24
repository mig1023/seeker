using System;

namespace Seeker.Game
{
    class Param
    {
        public static int Setter(int value, int current)
        {
            if ((value < current) && (Game.Settings.GetValue("Godmode") > 0))
                return current;

            else if (value < 0)
                return 0;

            else
                return value;
        }        

        public static int Setter(int value, int max, int current) =>
            (value > max ? max : Setter(value, current));

        public static int? Setter(int? value, int max, int current) =>
            (value.HasValue ? Setter((int)value, max, current) : (int?)null);
    }
}

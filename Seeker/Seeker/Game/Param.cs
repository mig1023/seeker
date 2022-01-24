using System;

namespace Seeker.Game
{
    class Param
    {
        public static int Setter(int value) => (value < 0 ? 0 : value);

        public static int Setter(int value, int current, object character)
        {
            bool thisIsHero = String.IsNullOrEmpty((character as Abstract.ICharacter).GetName());
            bool cheat = Game.Settings.GetValue("Godmode") > 0;

            if ((value < current) && cheat && thisIsHero)
                return current;

            else if (value < 0)
                return 0;

            else
                return value;
        }

        public static int Setter(int value, int max) =>
            (value > max ? max : Setter(value));

        public static int Setter(int value, int max, int current, object character) =>
            (value > max ? max : Setter(value, current, character));

        public static int? Setter(int? value, int max, int? current, object character) =>
            (value.HasValue ? Setter((int)value, max, current, character) : (int?)null);
    }
}

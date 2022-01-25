using System;

namespace Seeker.Game
{
    class Param
    {
        public static int Setter(int value) => (value < 0 ? 0 : value);

        public static int Setter(int value, int current, Abstract.ICharacter character)
        {
            bool cheat = Game.Settings.GetValue("Godmode") > 0;

            if (cheat && character.ThisIsProtagonist() && (value < current))
                return current;

            else if (value < 0)
                return 0;

            else
                return value;
        }

        public static int Setter(int value, int max) =>
            (value > max ? max : Setter(value));

        public static int Setter(int value, int max, int current, Abstract.ICharacter character) =>
            (value > max ? max : Setter(value, current, character));

        public static int? Setter(int? value, int max, int? current, Abstract.ICharacter character) =>
            (value.HasValue ? Setter((int)value, max, current, character) : (int?)null);
    }
}

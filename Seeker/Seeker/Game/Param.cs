using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Param
    {
        public static int Setter(int value) => (value < 0 ? 0 : value);

        public static int Setter(int value, int max) => (value > max ? max : Setter(value));

        public static int? Setter(int? value, int max) => (value.HasValue ? Setter((int)value, max) : (int?)null);
    }
}

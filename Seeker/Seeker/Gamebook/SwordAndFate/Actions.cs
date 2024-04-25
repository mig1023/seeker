using System.Collections.Generic;

namespace Seeker.Gamebook.SwordAndFate
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<string> RollCoin()
        {
            bool coin = Game.Dice.Roll() % 2 == 0;
            string line = coin ? "GOOD|На монетке выпал ОРЁЛ" : "BAD|На монетке выпала РЕШКА";

            return new List<string> { $"BIG|{line}" };
        }
    }
}

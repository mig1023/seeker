using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.HelpJane
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<string> RollCoin()
        {
            bool coin = Game.Dice.Roll() % 2 == 0;
            string line = coin ? "На монетке выпал ОРЁЛ" : "На монетке выпала РЕШКА";

            Game.Buttons.Disable(coin, "Выпал «орёл»", "Выпала «решка»");

            return new List<string> { $"BIG|BOLD|{line}" };
        }
    }
}

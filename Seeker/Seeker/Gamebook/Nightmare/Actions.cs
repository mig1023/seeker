using System.Collections.Generic;
using Seeker.Game;

namespace Seeker.Gamebook.Nightmare
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<string> Coin()
        {
            List<string> lines = new List<string>();

            lines.Add("Бросаем монетку:");

            bool coin = Dice.Roll() % 2 == 0;

            if (coin)
            {
                lines.Add("BIG|BOLD|GOOD|Выпал ОРЁЛ!");
                Buttons.Disable("Выпала решка");
            }
            else
            {
                lines.Add("BIG|BOLD|BAD|Выпала РЕШКА!");
                Buttons.Disable("Выпал орёл");
            }

            return lines;
        }
    }
}

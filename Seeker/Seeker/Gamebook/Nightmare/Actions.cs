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

                List<string> buttons = new List<string>
                {
                    "Выпала решка",
                    "Если выпала решка - торопись в горы",
                    "Выпала «решка» - нажми на кнопку «План Б»",
                    "Если выпала решка - иди направо",
                };
                
                Buttons.Disable(string.Join(",", buttons));
            }
            else
            {
                lines.Add("BIG|BOLD|BAD|Выпала РЕШКА!");

                List<string> buttons = new List<string>
                {
                    "Выпал орёл",
                    "Если выпал орел - беги в Арктику",
                    "Выпал «орёл» - нажимай на кнопку «Стоп»",
                    "Если выпал орел - иди налево",
                };

                Buttons.Disable("Выпал орёл");
            }

            return lines;
        }
    }
}

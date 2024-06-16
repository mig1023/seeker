using System.Collections.Generic;
using Seeker.Game;
using Xamarin.Forms.Shapes;

namespace Seeker.Gamebook.Nightmare
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        private void CoinResult(ref List<string> lines, string text, List<string> buttons)
        {
            lines.Add($"BIG|BOLD|{text}!");
            Buttons.Disable(string.Join(",", buttons));
        }

        public List<string> Coin()
        {
            List<string> lines = new List<string> { "Бросаем монетку:" };

            bool coin = Dice.Roll() % 2 == 0;

            if (coin)
            {
                List<string> buttons = new List<string>
                {
                    "Выпала решка",
                    "Если выпала решка - торопись в горы",
                    "Выпала «решка» - нажми на кнопку «План Б»",
                    "Если выпала решка - иди направо",
                };

                CoinResult(ref lines, "GOOD|Выпал ОРЁЛ", buttons);
            }
            else
            {
                List<string> buttons = new List<string>
                {
                    "Выпал орёл",
                    "Если выпал орел - беги в Арктику",
                    "Выпал «орёл» - нажимай на кнопку «Стоп»",
                    "Если выпал орел - иди налево",
                };

                CoinResult(ref lines, "BAD|Выпала РЕШКА", buttons);
            }

            return lines;
        }
    }
}

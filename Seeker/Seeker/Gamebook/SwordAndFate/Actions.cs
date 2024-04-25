using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.SwordAndFate
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public bool ThreeOptions { get; set; }
        public bool SevenOptions { get; set; }

        private string SevenOptionsLine(int dice)
        {
            switch (dice)
            {
                case 1:
                    return "На монетке выпал ОРЁЛ (первый вариант)";

                case 2:
                    return "На монетке выпала РЕШКА (второй вариант)";

                case 3:
                    return "Монетка встала НА РЕБРО (третий вариант)";

                case 4:
                    return "Монетка улетела обратно в кошелёк (четвёртый вариант)";

                case 5:
                    return "Монетка унесена ветром (пятый вариант)";

                case 6:
                    return "Монетка унесена птицей (шестой вариант)";

                default:
                    return "Монетка зависла в воздухе (седьмой вариант)";
            }
        }

        public List<string> RollCoin()
        {
            string line = String.Empty;
            
            if (ThreeOptions)
            {
                int dice = Game.Dice.Roll(size: 3);

                if (dice == 1)
                    line = "На монетке выпал ОРЁЛ";
                else if (dice == 2)
                    line = "На монетке выпала РЕШКА";
                else
                    line = "Монетка встала НА РЕБРО";
            }
            else if (SevenOptions)
            {
                int dice = Game.Dice.Roll(size: 7);
                line = SevenOptionsLine(dice);
            }
            else
            {
                bool coin = Game.Dice.Roll() % 2 == 0;
                line = coin ? "На монетке выпал ОРЁЛ" : "На монетке выпала РЕШКА";
            }

            return new List<string> { $"BIG|BOLD|{line}" };
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.NorthernShores
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Dices { get; set; }

        public int Throws { get; set; }

        public int Heat { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Теплота: {Character.Protagonist.Heat}",
        };

        private List<int> GetTragetDices(out string dicesLine)
        {
            List<int> targets = Dices
                .Split(',')
                .Select(x => int.Parse(x))
                .ToList();

            dicesLine = String.Empty;

            if (targets.Count < 2)
            {
                dicesLine = Game.Dice.Symbol(targets[0]);
            }
            else
            {
                for (int i = 0; i < targets.Count; i += 1)
                {
                    if (i > 0)
                        dicesLine += i == targets.Count - 1 ? " или " : ", ";

                    dicesLine += Game.Dice.Symbol(targets[i]);
                }
            }

            return targets;
        }

        public override List<string> Representer()
        {
            List<int> targetDices = GetTragetDices(out string dicesLine);
            return new List<string> { $"Выбросить {dicesLine} за {Constants.Throws[Throws]}" };
        }

        public List<string> Test()
        {
            List<string> test = new List<string>();

            List<int> targetDices = GetTragetDices(out string dicesLine);

            test.Add($"GRAY|Необходимо выбросить {dicesLine} за {Constants.Throws[Throws]}");

            for (int i = 1; i <= Throws; i += 1)
            {
                string num = String.Empty;

                if (Throws > 1)
                    num = $" попытка {i}";

                int dice = Game.Dice.Roll();
                test.Add($"Бросок{num}: {Game.Dice.Symbol(dice)}");

                if (targetDices.Contains(dice))
                {
                    Character.Protagonist.Heat += Heat;

                    test.Add("GOOD|BOLD|Успех! :)");
                    test.Add($"Вы получаете {Heat} Тепла!");
                    return test;
                }
                else
                {
                    test.Add("Нет, выпал неправильный кубик...");
                }
            }

            test.Add("BAD|BOLD|Неудачно :(");
            return test;
        }
    }
}

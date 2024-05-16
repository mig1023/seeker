using System;
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

            return new List<string> { $"Нужно выбросить {dicesLine}\n" +
                $"за {Constants.Throws[Throws]}\nесли получится, то +{Heat} Тепла" };
        }

        public List<string> Test()
        {
            List<string> test = new List<string>();

            List<int> targetDices = GetTragetDices(out string dicesLine);

            test.Add($"BOLD|Пробуем выбросить {dicesLine} за {Constants.Throws[Throws]}\n");

            for (int i = 1; i <= Throws; i += 1)
            {
                string num = String.Empty;

                if (Throws > 1)
                    num = $", попытка {i}";

                int dice = Game.Dice.Roll();
                test.Add($"Бросаем{num}: {Game.Dice.Symbol(dice)}");

                if (targetDices.Contains(dice))
                {
                    Character.Protagonist.Heat += Heat;

                    test.Add("BIG|GOOD|BOLD|Успех! :)");
                    test.Add($"Вы получаете {Heat} Тепла!");
                    return test;
                }
                else
                {
                    string fail = i > 1 ? "Опять" : "Нет,";
                    test.Add($"{fail} выпал неправильный кубик...");
                }
            }

            test.Add("BIG|BAD|BOLD|Неудачно :(");
            return test;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.DzungarWar
{
    class Services
    {
        public static int TestLevelWithPenalty(int level, out List<string> penaltyLine,
            ref bool NextTestWithTincture, ref bool NextTestWithGinseng, ref bool NextTestWithAirag,
            string TriggerTestPenalty)
        {
            penaltyLine = new List<string> { };

            if (NextTestWithTincture)
            {
                level -= 4;
                penaltyLine.Add("Бонус в -4 к уровню проверки за настойку");
            }

            if (NextTestWithGinseng)
            {
                level -= 8;
                penaltyLine.Add("Бонус в -8 к уровню проверки за отвар женьшеня");
            }

            if (NextTestWithAirag)
            {
                level -= 2;
                penaltyLine.Add("Бонус в -2 к уровню проверки за айраг");
            }

            if (String.IsNullOrEmpty(TriggerTestPenalty))
                return level;

            string[] penalties = TriggerTestPenalty.Split(';');

            foreach (string eachPenalty in penalties)
            {
                string[] penalty = eachPenalty.Split(',');

                if (penalty[0].Trim() == "Вино")
                {
                    int bottles = Game.Data.Triggers.Where(x => x == "Кувшин вина").Count();

                    if (bottles > 0)
                    {
                        level += (bottles * -1);
                        penaltyLine.Add(String.Format("Пенальти -{0} за покупку {0} кувшинов", bottles));
                    }
                }
                else if (Game.Option.IsTriggered(penalty[0].Trim()))
                {
                    level += int.Parse(penalty[1].Trim());
                    penaltyLine.Add(String.Format("Пенальти {0} к уровню проверки за ключевое слово {1}", penalty[1].Trim(), penalty[0].Trim()));
                }
            }

            if (level < 0)
                level = 0;

            return level;
        }

        public static void TestParam(string stat, int level, out bool result, out List<string> resultLine,
            ref bool NextTestWithTincture, ref bool NextTestWithGinseng, ref bool NextTestWithAirag, int currentStat,
            string TriggerTestPenalty)
        {
            resultLine = new List<string>();

            level = TestLevelWithPenalty(level, out List<string> penalties,
                ref NextTestWithTincture, ref NextTestWithGinseng, ref NextTestWithAirag, TriggerTestPenalty);

            NextTestWithTincture = false;
            NextTestWithGinseng = false;
            NextTestWithAirag = false;

            resultLine.AddRange(penalties);

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            result = (firstDice + secondDice) + currentStat >= level;

            resultLine.Add(String.Format(
                "Проверка {0}: {1} + {2} + {3} {4} {5}",
                Constants.StatNames[stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice),
                currentStat, (result ? ">=" : "<"), level));
        }
    }
}

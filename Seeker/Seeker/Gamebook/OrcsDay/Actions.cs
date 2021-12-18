using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.OrcsDay
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public string Stat { get; set; }
        public int Level { get; set; }
        public bool OrcishnessTest { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Оркишность: {0}", Game.Other.NegativeMeaning(protagonist.Orcishness)),
            String.Format("Здоровье: {0}", protagonist.Hitpoints),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Деньги: {0}", protagonist.Money),
            String.Format("Удача: {0}", Game.Other.NegativeMeaning(protagonist.Luck)),
            String.Format("Смелость: {0}", Game.Other.NegativeMeaning(protagonist.Courage)),
            String.Format("Мозги: {0}", Game.Other.NegativeMeaning(protagonist.Wits)),
            String.Format("Мышцы: {0}", Game.Other.NegativeMeaning(protagonist.Muscle)),
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (OrcishnessTest && (Level > 0))
            {
                return new List<string> { String.Format("Проверка {0}\nуровень Оркишность ({1}) + {2}",
                    Constants.StatNames()[Stat], protagonist.Orcishness, Level) };
            }
            else if (OrcishnessTest)
            {
                return new List<string> { String.Format("Проверка {0}\nпо уровню Оркишности",
                    Constants.StatNames()[Stat]) };
            }
            else if (Level > 0)
            {
                return new List<string> { String.Format("Проверка {0}, уровень {1}",
                    Constants.StatNames()[Stat], Level) };
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                return new List<string> { String.Format("{0}\n(текущее значение: {1})",
                    Text, Game.Other.NegativeMeaning(GetProperty(protagonist, Stat))) };
            }
            else if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nатака {1}  защита {2}  здоровье {3}",
                    enemy.Name, enemy.Attack, enemy.Defense, enemy.Hitpoints));

            return enemies;
        }

        public override bool IsButtonEnabled(bool secondButton = false) =>
            String.IsNullOrEmpty(Stat) || (protagonist.StatBonuses > 0) || secondButton;

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
                ChangeProtagonistParam(Stat, protagonist, "StatBonuses");

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() => ChangeProtagonistParam(Stat, protagonist, "StatBonuses", decrease: true);

        public List<string> OrcishnessInit()
        {
            Character orc = protagonist;

            orc.Orcishness = 6;

            List<string> orcishness = new List<string> { "BOLD|Изначальное значение: 6" };

            if ((orc.Muscle < 0) || (orc.Wits < 0) || (orc.Courage < 0) || (orc.Luck < 0))
            {
                orcishness.Add("-1 Оркишность за отрицательный параметр");
                orc.Orcishness -= 1;
            }

            if (orc.Wits > orc.Muscle)
            {
                orcishness.Add("-1 Оркишность за то, что Мозги круче Мускулов");
                orc.Orcishness -= 1;
            }

            if (orc.Luck > 0)
            {
                orcishness.Add("-1 Оркишность за Удачу");
                orc.Orcishness -= 1;
            }
                
            if ((orc.Muscle > orc.Wits) && (orc.Muscle > orc.Courage) || (orc.Muscle > orc.Luck))
            {
                orcishness.Add("+1 Оркишность за то, что Мышцы самое крутое");
                orc.Orcishness += 1;
            }

            if (orc.Courage > orc.Wits)
            {
                orcishness.Add("+1 Оркишность за то, что Смелости больше, чем Мозгов");
                orc.Orcishness += 1;
            }

            if (orc.Courage > 2)
            {
                orcishness.Add("-1 Оркишность за то, что Смелости слишком много");
                orc.Orcishness -= 1;
            }

            orcishness.Add(String.Format("BIG|BOLD|Итоговая Оркишность: {0}", orc.Orcishness));

            return orcishness;

        }

        public List<string> Test()
        {
            List<string> testLines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int currentStat = GetProperty(protagonist, Stat);

            bool okResult = false;

            if (OrcishnessTest)
            {
                okResult = (firstDice + secondDice) + currentStat >= protagonist.Orcishness + Level;

                testLines.Add(String.Format("Проверка на {0}: {1} + {2} + {3} {4} {5}{6}",
                    Constants.StatNames()[Stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice),
                    currentStat, (okResult ? ">=" : "<"), protagonist.Orcishness, 
                    (Level > 0 ? String.Format(" + {0}", Level) : String.Empty)));
            }
            else
            {
                okResult = (firstDice + secondDice) + currentStat >= Level;

                testLines.Add(String.Format("Проверка на {0}: {1} + {2} + {3} {4} {5}",
                    Constants.StatNames()[Stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice),
                    currentStat, (okResult ? ">=" : "<"), Level));
            }

            testLines.Add(Result(okResult, "УСПЕШНО|НЕУДАЧНО"));

            return testLines;
        }

        private int Protection(ref List<string> fight)
        {
            if (protagonist.Wits > protagonist.Luck)
            {
                fight.Add("Используем мозги, т.к. их больше, чем удачи");
                return protagonist.Wits;
            }
            else
            {
                fight.Add("Полагаемся на удачу, т.к. на неё больше надежды, чем на мозги");
                return protagonist.Luck;
            }
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();
            Character enemy = Enemies[0];

            int round = 1;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                fight.Add(String.Format("BOLD|{0} нападает:", enemy.Name));

                Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                int protection = Protection(ref fight);

                bool enemyAttackFail = (enemyRollFirst + enemyRollSecond) + protection >= enemy.Attack;

                fight.Add(String.Format("Удар {0}: {1} + {2} + {3} {4}",
                    Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond),
                    protection, (enemyAttackFail ? ">=" : "<"), enemy.Attack));

                if (!enemyAttackFail)
                {
                    protagonist.Hitpoints -= 1;
                    fight.Add(String.Format("BAD|BOLD|{0} ранил тебя", enemy.Name));
                }
                else
                    fight.Add("Ты отбил удар!");

                if (protagonist.Hitpoints <= 0)
                {
                    fight.Add(String.Empty);
                    fight.Add(String.Format("BIG|BAD|Ты ПРОИГРАЛ :("));
                    return fight;
                }

                fight.Add(String.Empty);
                fight.Add("BOLD|Ты нападаешь:");

                Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                bool protagonistAttackWin = (protagonistRollFirst + protagonistRollSecond) + protagonist.Muscle >= enemy.Defense;

                fight.Add(String.Format("Удар {0}: {1} + {2} + {3} {4}",
                    Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                    protagonist.Muscle, (enemyAttackFail ? ">=" : "<"), enemy.Defense));

                if (protagonistAttackWin)
                {
                    enemy.Hitpoints -= 1;
                    fight.Add("GOOD|BOLD|Ты ранил противника!");
                }
                else
                    fight.Add(String.Format("Противник отбил твой удар"));

                if (enemy.Hitpoints <= 0)
                {
                    fight.Add(String.Empty);
                    fight.Add(String.Format("BIG|GOOD|Ты ПОБЕДИЛ :)"));
                    return fight;
                }

                 fight.Add(String.Empty);

                round += 1;
            }
        }
    }
}

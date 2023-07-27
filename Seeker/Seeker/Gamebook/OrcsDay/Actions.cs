using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.OrcsDay
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public string Stat { get; set; }
        public int Level { get; set; }
        public bool LevelNull { get; set; }
        public bool OrcishnessTest { get; set; }
        public bool ZombiePotionTest { get; set; }
        public bool MortimerFight { get; set; }
        public bool LateHelp { get; set; }
        public bool GirlHelp { get; set; }
        public bool OrcsHelp { get; set; }
        public bool SecondGame { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Оркишность: {Game.Services.NegativeMeaning(protagonist.Orcishness)}",
            $"Здоровье: {protagonist.Hitpoints}/5",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Мышцы: {Game.Services.NegativeMeaning(protagonist.Muscle)}",
            $"Мозги: {Game.Services.NegativeMeaning(protagonist.Wits)}",
            $"Смелость: {Game.Services.NegativeMeaning(protagonist.Courage)}",
            $"Удача: {Game.Services.NegativeMeaning(protagonist.Luck)}",
            $"Деньги: {protagonist.Money}",
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (OrcishnessTest && (Level > 0))
            {
                return new List<string> { String.Format("Проверка {0}\nуровень Оркишность ({1}) + {2}",
                    Constants.StatNames[Stat], protagonist.Orcishness, Level) };
            }
            else if (OrcishnessTest)
            {
                return new List<string> { String.Format("Проверка {0}\nпо уровню Оркишности",
                    Constants.StatNames[Stat]) };
            }
            else if (Level > 0)
            {
                return new List<string> { String.Format("Проверка {0}, уровень {1}",
                    Constants.StatNames[Stat], Level) };
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                return new List<string> { String.Format("{0}\n(текущее значение: {1})",
                    Head, Game.Services.NegativeMeaning(GetProperty(protagonist, Stat))) };
            }
            else if (Price > 0)
            {
                return new List<string> { Head };
            }
            else if (Enemies == null)
            {
                return enemies;
            }

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nатака {1}  защита {2}  здоровье {3}",
                    enemy.Name, enemy.Attack, enemy.Defense, enemy.Hitpoints));

            return enemies;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (ZombiePotionTest && !Game.Option.IsTriggered("Зелье управление зомби"))
            {
                return false;
            }
            else if (Used)
            {
                return false;
            }
            else if (Stat == "Bet")
            {
                if (secondButton)
                    return protagonist.Bet > 1;

                else
                    return protagonist.Bet < 5;
            }
            else
            {
                return String.IsNullOrEmpty(Stat) || (protagonist.StatBonuses > 0) || (Level > 0) || LevelNull || secondButton;
            }
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
                ChangeProtagonistParam(Stat, protagonist, "StatBonuses");

            if (Benefit != null)
                Benefit.Do();

            if (Price > 0)
                Used = true;

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, protagonist, "StatBonuses", decrease: true);

        private string OrcishnessChange(string line)
        {
            if (line.StartsWith("+"))
            {
                protagonist.Orcishness += 1;
                return String.Format("BAD|{0}", line);
            }
            else
            {
                protagonist.Orcishness -= 1;
                return String.Format("GOOD|{0}", line);
            }
        }

        public List<string> OrcishnessInit()
        {
            Character orc = protagonist;

            orc.Orcishness = 6;

            List<string> orcishness = new List<string> { "BOLD|Изначальное значение: 6" };

            if ((orc.Muscle < 0) || (orc.Wits < 0) || (orc.Courage < 0) || (orc.Luck < 0))
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Negative"]));

            if (orc.Wits > orc.Muscle)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Wits"]));

            if (orc.Luck > 0)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Luck"]));
                
            if ((orc.Muscle > orc.Wits) && (orc.Muscle > orc.Courage) || (orc.Muscle > orc.Luck))
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Muscle"]));

            if (orc.Courage > orc.Wits)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Courage"]));

            if (orc.Courage > 2)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["TooMuch"]));

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
                    Constants.StatNames[Stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice),
                    currentStat, (okResult ? ">=" : "<"), protagonist.Orcishness, 
                    (Level > 0 ? String.Format(" + {0}", Level) : String.Empty)));
            }
            else
            {
                okResult = (firstDice + secondDice) + currentStat >= Level;

                testLines.Add(String.Format("Проверка на {0}: {1} + {2} + {3} {4} {5}",
                    Constants.StatNames[Stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice),
                    currentStat, (okResult ? ">=" : "<"), Level));
            }

            testLines.Add(Result(okResult, "УСПЕШНО|НЕУДАЧНО"));

            if (ZombiePotionTest && okResult)
            {
                testLines.Add("Теперь ты контролируешь Зомби-тролля!");
                Game.Option.Trigger("Зомби-тролль");
            }

            return testLines;
        }

        public List<string> CourageTest()
        {
            List<string> testLines = new List<string>();

            bool okResult = protagonist.Courage >= protagonist.Orcishness + protagonist.Wits;

            testLines.Add(String.Format("Проверка: {0} Смелость {1} {2} Оркишность + {3} Мозги",
                protagonist.Courage, (okResult ? ">=" : "<"), protagonist.Orcishness, protagonist.Wits));

            testLines.Add(Result(okResult, "УСПЕШНО|НЕУДАЧНО"));

            return testLines;
        }

        public List<string> CardGames()
        {
            List<string> gameLines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            int gameResult = (firstDice + secondDice) + protagonist.Luck;

            gameLines.Add(String.Format("Выпавшие карты: {0} + {1} + {2} = {3}",
                   Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), protagonist.Luck, gameResult));

            if (gameResult >= 15)
            {
                protagonist.Money += protagonist.Bet;
                gameLines.Add("GOOD|BIG|Получилось! Ты не только вернул ставку, но и выиграл столько же!");
            }
            else if (((gameResult < 12) && !SecondGame) || ((gameResult < 10) && SecondGame))
            {
                protagonist.Money -= protagonist.Bet;
                gameLines.Add("BAD|BIG|Провал! Ты потерял свою ставку!");
            }
            else
                gameLines.Add("BIG|Выиграть не получилось, но ставка осталась при тебе!");

            return gameLines;
        }

        public override bool Availability(string option) =>
            AvailabilityTrigger(option);

        public List<string> Fight()
        {
            List<string> fight = new List<string>();
            Character enemy = Enemies[0];

            bool otherOrcs = false;
            int otherOrcsHitpoints = 3, girlWounds = 3;
            bool magicPotion = GirlHelp && !Services.FightVsAdvanturer(enemy.Name);

            if (OrcsHelp || Game.Option.IsTriggered("Много орков помогают"))
            {
                otherOrcs = true;
                otherOrcsHitpoints = (OrcsHelp ? 5 : 3);
            }

            if (MortimerFight && (otherOrcs || Game.Option.IsTriggered("Несколько орков помогают")))
            {
                fight.Add("BOLD|-2 к Атаке и Защите противника из-за помощи других орков\n");
                Services.FightBonus(enemy, sub: true);
            }
            
            if (Game.Option.IsTriggered("Зомби-тролль"))
            {
                fight.Add("BOLD|-3 к Атаке и Защите противника из-за помощи Зомби-тролля\n");
                Services.FightBonus(enemy, sub: true, bonusLevel: 3);
            }

            int round = 1;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));
                fight.Add(String.Format("BOLD|{0} нападает:", enemy.Name));

                bool otherOrcsUnderAttack = false, girlUnderAttack = false, enemyAttackFail = false;

                if (MortimerFight && otherOrcs)
                {
                    int whoUnderAttack = Game.Dice.Roll();
                    otherOrcsUnderAttack = whoUnderAttack < 3;

                    fight.Add(String.Format("Кого атакует Мортимер: {0}", Game.Dice.Symbol(whoUnderAttack)));

                    if (!otherOrcsUnderAttack)
                        fight.Add("BOLD|Он атакует тебя");
                }
                else if (GirlHelp || OrcsHelp)
                {
                    int whoUnderAttack = Game.Dice.Roll();

                    if (GirlHelp)
                        girlUnderAttack = whoUnderAttack < 4;
                    else
                        otherOrcsUnderAttack = whoUnderAttack < 4;

                    fight.Add(String.Format("Кого атакует противник: {0}", Game.Dice.Symbol(whoUnderAttack)));

                    if (!girlUnderAttack && !otherOrcsUnderAttack)
                        fight.Add("BOLD|Он атакует тебя");
                }

                if (!otherOrcsUnderAttack && !girlUnderAttack)
                {
                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int protection = Services.Protection(ref fight);

                    enemyAttackFail = (enemyRollFirst + enemyRollSecond) + protection >= enemy.Attack;

                    fight.Add(String.Format("Удар врага: {0} + {1} + {2} {3} {4}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond),
                        protection, (enemyAttackFail ? ">=" : "<"), enemy.Attack));
                }

                if (girlUnderAttack)
                {
                    girlWounds -= 1;

                    fight.Add(String.Format("BOLD|Противник атакует девушку!\n" +
                        "Она теряет 1 Здоровье, осталось {0}", girlWounds));

                    if (girlWounds <= 0)
                    {
                        fight.Add("BAD|\nПротивник убил девушку!");

                        if (magicPotion)
                        {
                            fight.Add("GOOD|BOLD|Ты использовал целительное снадобье и её здоровье восстановлено!");
                            girlWounds = 3;
                            magicPotion = false;
                        }
                        else
                        {
                            fight.Add("BOLD|Дальше драться придётся тебе одному!");
                            GirlHelp = false;
                            Game.Option.Trigger("Девушка погибла");

                            if (Services.FightVsAdvanturer(enemy.Name))
                            {
                                fight.Add("Приключенцы получают бонус к Атаке и Защите");
                                Services.FightBonus(enemy);
                            }
                        }
                    }
                }
                else if (otherOrcsUnderAttack)
                {
                    otherOrcsHitpoints -= 1;

                    fight.Add(String.Format("BOLD|Он атакует других орков!\n" +
                        "Они теряют 1 Здоровье, осталось {0}", otherOrcsHitpoints));

                    if (otherOrcsHitpoints <= 0)
                    {
                        fight.Add("BAD|\nПротивник победил остальных орков и ты теряешь их помощь");
                        fight.Add("BOLD|Дальше драться придётся тебе одному\n");

                        OrcsHelp = false;
                        otherOrcs = false;
                        Services.FightBonus(enemy);
                    }
                }
                else if (enemyAttackFail)
                {
                    fight.Add("Ты отбил удар!");
                }
                else
                {
                    protagonist.Hitpoints -= 1;
                    fight.Add(String.Format("BAD|BOLD|{0} ранил тебя", enemy.Name));
                    fight.Add(String.Format("Твоё здоровье стало равно {0}", protagonist.Hitpoints));

                    if ((protagonist.Hitpoints == 2) && LateHelp)
                    {
                        fight.Add("BOLD|Другие орки присоединяются к бою!\n-2 к Атаке и Защите противника!");
                        Services.FightBonus(enemy, sub: true);
                    }
                }

                if (protagonist.Hitpoints <= 0)
                {
                    if (magicPotion)
                    {
                        fight.Add("GOOD|Ты использовал целительное снадобье и получаешь +3 к здоровью!\n");
                        protagonist.Hitpoints += 3;
                        magicPotion = false;
                    }
                    else
                    {
                        fight.Add(String.Empty);
                        fight.Add(String.Format("BIG|BAD|Ты ПРОИГРАЛ :("));
                        return fight;
                    }
                }

                fight.Add(String.Empty);
                fight.Add("BOLD|Ты нападаешь:");

                Game.Dice.DoubleRoll(out int protRollFirst, out int protRollSecond);
                int protagonistAttack = (protRollFirst + protRollSecond) + protagonist.Muscle + protagonist.Weapon;
                bool protagonistAttackWin = protagonistAttack >= enemy.Defense;

                fight.Add(String.Format("Твой удар: {0} + {1} + {2}{3} {4} {5}",
                    Game.Dice.Symbol(protRollFirst), Game.Dice.Symbol(protRollSecond),
                    protagonist.Muscle, (protagonist.Weapon > 0 ? String.Format(" + {0} меч", protagonist.Weapon) : String.Empty),
                    (protagonistAttackWin ? ">=" : "<"), enemy.Defense));

                if (protagonistAttackWin)
                {
                    enemy.Hitpoints -= 1;
                    fight.Add("GOOD|BOLD|Ты ранил противника!");
                    fight.Add(String.Format("Его здоровье стало равно {0}", enemy.Hitpoints));
                }
                else
                    fight.Add(String.Format("Противник отбил твой удар"));

                if (enemy.Hitpoints <= 0)
                {
                    Services.FightWinTriggers(enemy.Name, GirlHelp);

                    fight.Add(String.Empty);
                    fight.Add(String.Format("BIG|GOOD|Ты ПОБЕДИЛ :)"));
                    return fight;
                }
                 fight.Add(String.Empty);

                round += 1;
            }
        }

        public List<string> Calculation()
        {
            List<string> results = new List<string> { "BOLD|CЧИТАЕМ:" };

            int result = 0, lines = 0;

            if (Character.Protagonist.Orcishness <= 0)
            {
                results.Add("GOOD|+1 за то, что твоя Оркишность упала до нуля или ниже");
                result += 1;
                lines += 1;

                if (Game.Option.IsTriggered("Кандидат в Властелины") && !Game.Option.IsTriggered("Тёмный Властелин"))
                {
                    results.Add("GOOD|+1 за то, что стал новым Темным Властелином");
                    result += 1;
                }
            }

            foreach (KeyValuePair<string, string> trigger in Constants.ResultCalculation)
            {
                bool add = trigger.Value.Contains("+");
                string color = (add ? "GOOD" : "BAD");

                if (!Services.CalculationCondition(trigger.Key))
                    continue;

                results.Add(String.Format("{0}|{1}", color, trigger.Value));
                result += (add ? 1 : -1);
                lines += 1;
            }

            if (lines == 0)
                results.Add("Да уж, вообще нечего вспомнить...");

            results.Add(String.Format("BIG|BOLD|ИТОГО: {0}",  Game.Services.NegativeMeaning(result)));

            return results;
        }

        public List<string> OvercomeOrcishness()
        {
            List<string> overcome = new List<string> { "BOLD|CЧИТАЕМ:" };

            while (true)
            {
                int sense = protagonist.Courage + protagonist.Wits;
                int orcishness = protagonist.Muscle + protagonist.Orcishness + 5;

                overcome.Add("BOLD|Борьба:");

                overcome.Add(String.Format("С одной стороны: {0} (смелость) + {1} (мозги) = {2}",
                    protagonist.Courage, protagonist.Wits, sense));

                overcome.Add(String.Format("С другой: {0} (мышцы) + {1} (оркишность) + 5 = {2}",
                    protagonist.Muscle, protagonist.Orcishness, orcishness));

                overcome.Add(String.Format("В результате: {0} {1} {2}",
                    sense, Game.Services.Сomparison(sense, orcishness), orcishness));

                if (sense >= orcishness)
                {
                    overcome.Add("BOLD|GOOD|Ты выиграл!");
                    overcome.Add("Оркишность снизилась на единицу!");

                    protagonist.Orcishness -= 1;

                    if (protagonist.Orcishness <= 0)
                    {
                        overcome.Add("BIG|GOOD|Ты освободился от своей Оркской природы! :)");
                        overcome.Add("Ты получаешь за это дополнительно 3 единицы Смелости!");

                        protagonist.Courage += 3;

                        if (Game.Option.IsTriggered("Кандидат в Властелины"))
                        {
                            overcome.Add("\nBOLD|Ты стал Тёмным Властелином!");
                            Game.Option.Trigger("Тёмный Властелин");
                        }

                        return overcome;
                    }
                }
                else
                {
                    overcome.Add("BOLD|BAD|Ты проиграл!");
                    overcome.Add("Смелость снизилась на единицу!");

                    protagonist.Courage -= 1;

                    if (protagonist.Courage <= 0)
                    {
                        overcome.Add("BIG|BAD|Ты остался рабом своей Оркской природы :(");
                        return overcome;
                    }
                }
            }
        }

        public override bool IsHealingEnabled() =>
            protagonist.Hitpoints < 5;

        public override void UseHealing(int healingLevel) =>
            protagonist.Hitpoints += healingLevel;
    }
}

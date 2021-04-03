using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Actions : Abstract.IActions
    {
        public enum Specifics { Nope, ElectricDamage, WitchFight, Ulrich, BlackWidow, Invulnerable,
            RandomRoundsToFight, NeedForSpeed, NeedForSpeedAndDead, ToadVenom, IncompleteCorpse };

        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
        public bool Used { get; set; }
        public bool Multiple { get; set; }
        public List<Modification> Benefit { get; set; }

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int RoundsWinToWin { get; set; }
        public int RoundsToFight { get; set; }
        public int WoundsToWin { get; set; }
        public int WoundsForTransformation { get; set; }
        public int WoundsLimit { get; set; }
        public int HitStrengthBonus { get; set; }
        public int ExtendedDamage { get; set; }
        public Specifics Specificity { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Text) || (ActionName == "Get"))
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                if (enemy.Endurance > 0)
                    enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));
                else
                    enemies.Add(String.Format("{0}\nмастерство {1} ", enemy.Name, enemy.Mastery));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Мастерство: {0}", Character.Protagonist.Mastery),
                String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
                String.Format("Удача: {0}", Character.Protagonist.Luck)
            };

            return statusLines;
        }

        public List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Золото: {0}", Character.Protagonist.Gold),
                String.Format("Изменение: {0}", Character.Protagonist.Change)
            };

            if (Character.Protagonist.Crossbow > 0)
                statusLines.Add(String.Format("Арбалет: {0}", Character.Protagonist.Crossbow));

            if (Character.Protagonist.Gun > 0)
                statusLines.Add(String.Format("Пистолет: {0}", Character.Protagonist.Gun));

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Endurance <= 0;
        }

        public List<string> Luck()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodLuck = (fisrtDice + secondDice) <= Character.Protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), Character.Protagonist.Luck
            ) };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Mastery()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool masteryOk = (fisrtDice + secondDice) <= Character.Protagonist.Mastery;

            List<string> masteryCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (masteryOk ? "<=" : ">"), Character.Protagonist.Mastery
            ) };

            masteryCheck.Add(masteryOk ? "BIG|GOOD|Мастерства ХВАТИЛО :)" : "BIG|BAD|Мастерства НЕ хватило :(");

            return masteryCheck;
        }

        public List<string> Transformation()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool changeOk = (fisrtDice + secondDice) > Character.Protagonist.Change;

            List<string> changeCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (changeOk ? ">" : "<="), Character.Protagonist.Change
            ) };

            changeCheck.Add(changeOk ? "BIG|GOOD|Победил ЧЕЛОВЕК :)" : "BIG|BAD|Победил ВОЛК :(");

            return changeCheck;
        }

        public List<string> Dice() => new List<string> { String.Format("BIG|На кубике выпало: {0}", Game.Dice.Symbol(Game.Dice.Roll())) };

        public List<string> DicesEndurance()
        {
            List<string> diceCheck = new List<string> { };

            int result = 0;

            for (int i = 1; i <= 3; i++)
            {
                int dice = Game.Dice.Roll();
                result += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            diceCheck.Add(String.Format("BIG|Сумма на кубиках: {0}", result));

            diceCheck.Add(result < Character.Protagonist.Endurance ? "BIG|GOOD|Меньше! :)" : "BIG|BAD|Больше :(");

            return diceCheck;
        }

        public List<string> DiceAnxiety()
        {
            List<string> diceCheck = new List<string> { };

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            int result = firstDice + secondDice;

            diceCheck.Add(String.Format("На кубиках выпало: {0} + {1} = {2}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(firstDice), result));
            diceCheck.Add(String.Format("Текущий уровень тревоги: {0}", Character.Protagonist.Anxiety));

            diceCheck.Add(result > Character.Protagonist.Anxiety ? "BIG|GOOD|Больше! :)" : "BIG|BAD|Меньше :(");

            return diceCheck;
        }
        
        public List<string> Competition()
        {
            List<string> competition = new List<string> { };

            int penalty = 0;
            string penaltyLine = String.Empty;
            bool inTarget = true;

            for (int i = 1; i <= 3; i++)
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();
                int result = firstDice + secondDice + penalty;

                if (penalty > 0)
                    penaltyLine = String.Format(" + {0} пенальти", penalty);

                competition.Add(
                    String.Format("{0} выстрел: {1} + {2}{3} = {4}",
                    i, Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), penaltyLine, result)
                );

                if (result > Character.Protagonist.Mastery)
                {
                    competition.Add("BAD|Это больше Мастерства: вы промахнулись...");
                    inTarget = false;
                }
                else
                    competition.Add("GOOD|Это меньше или равно Мастерству: вы попали в цель!");

                competition.Add(String.Empty);

                penalty += 1;
            }

            if (inTarget)
            {
                competition.Add("BIG|GOOD|Вы ВЫИГРАЛИ и получаете выигрышь в 5 золотых! :)");
                Character.Protagonist.Gold += 5;
            }
            else
                competition.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

            return competition;
        }

        public List<string> DicesRestore()
        {
            List<string> diceRestore = new List<string> { };

            int dice = Game.Dice.Roll();

            diceRestore.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));

            string line = String.Empty;

            if (dice < 3)
            {
                Character.Protagonist.Mastery = Character.Protagonist.MaxMastery;
                line = "о Мастерство";
            }
            else if (dice > 4)
            {
                Character.Protagonist.Luck = Character.Protagonist.MaxLuck;
                line = "а Удача";
            }
            else
            {
                Character.Protagonist.Endurance = Character.Protagonist.MaxEndurance;
                line = "а Выносливость";
            }

            diceRestore.Add(String.Format("BIG|GOOD|Восстановлен{0}", line));

            return diceRestore;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            string bonus = (Value > 0 ? String.Format(" + ещё {0}", Value) : String.Empty);

            diceCheck.Add(String.Format("На кубике выпало: {0}{1}", Game.Dice.Symbol(dice), bonus));

            Character.Protagonist.Endurance -= dice + Value;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dice + Value));

            return diceCheck;
        }

        public List<string> DiceGold()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add(String.Format("На кубике выпало: {0} + ещё {1}", Game.Dice.Symbol(dice), Value));

            dice += Value;

            Character.Protagonist.Gold -= dice;

            diceCheck.Add(String.Format("BIG|GOOD|Вы нашли золотых: {0}", dice));

            return diceCheck;
        }

        public bool IsButtonEnabled()
        {
            bool disabledByUsed = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Gold < Price);

            return !(disabledByUsed || disabledByPrice);
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Gold >= Price))
            {
                Character.Protagonist.Gold -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    foreach (Modification modification in Benefit)
                        modification.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public static bool CheckOnlyIf(string option) => true;

        private bool EnemyWound(List<Character> FightEnemies, ref int enemyWounds, ref List<string> fight)
        {
            enemyWounds += 1;

            bool enemyLost = true;

            foreach (Character e in FightEnemies)
                if (e.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0))
                    enemyLost = false;

            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
            {
                fight.Add(String.Empty);
                fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                return true;
            }
            else
                return false;
        }

        private int UlrichFight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int ulrichMastery = Constants.GetUlrichMastery();

            fight.Add(String.Empty);

            int ulrichRollFirst = Game.Dice.Roll();
            int ulrichRollSecond = Game.Dice.Roll();
            int ulrichHitStrength = ulrichRollFirst + ulrichRollSecond + ulrichMastery;

            fight.Add(String.Format("Сила удара Ульриха: {0} + {1} + {2} = {3}",
                Game.Dice.Symbol(ulrichRollFirst), Game.Dice.Symbol(ulrichRollSecond), ulrichMastery, ulrichHitStrength
            ));

            if (ulrichHitStrength > enemyHitStrength)
            {
                fight.Add(String.Format("GOOD|{0} ранен", enemyName));
                return 2;
            }
            else
            {
                fight.Add("BOLD|Ульрих не смог ранить врага");
                return 0;
            }
        }

        private void ElectricDamage(ref Character hero, ref List<string> fight)
        {
            int electric = Game.Dice.Roll();

            fight.Add(String.Format("Кубик электрического разряда: {0}", Game.Dice.Symbol(electric)));

            if (electric >= 5)
            {
                hero.Endurance -= 3;
                fight.Add("Вы потеряли ещё 3 Выносливость от разряда");
            }
            else
                fight.Add("Разряд прошёл мимо");
        }
        private void WitchFight(ref Character hero, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(witchAttack)));

            if (witchAttack < 3)
            {
                hero.Endurance -= 2;
                fight.Add("Вы потеряли 2 Выносливости");
            }
            else if ((witchAttack == 3) || (witchAttack == 4))
            {
                hero.Endurance -= 3;
                fight.Add("Вы потеряли 3 Выносливости");
            }
            else if (witchAttack == 5)
            {
                hero.Endurance -= 2;
                hero.Luck -= 1;
                fight.Add("Вы потеряли 2 Выносливости и 1 Удачу");
            }
            else
            {
                hero.Endurance -= 2;
                hero.Change += 1;
                fight.Add(String.Format("Вы потеряли 2 Выносливости и Трансформация продолжилась (Изменение достигло {0})", hero.Change));
            }
        }

        private void ToadVenomFight(ref Character hero, ref List<string> fight)
        {
            int venomAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик яда: {0}", Game.Dice.Symbol(venomAttack)));

            if (venomAttack >= 5)
            {
                hero.Endurance -= 2;
                fight.Add("Вы потеряли ещё 2 Выносливости от яда");
            }
            else
                fight.Add("Обошлось...");
        }

        private void RandomRoundsToFight(ref List<string> fight)
        {
            RoundsToFight = 15 - Character.Protagonist.Mastery;
            fight.Add(String.Format("Вам необходимо продержаться: 15 - {0} = {1} раундов", Character.Protagonist.Mastery, RoundsToFight));
            fight.Add(String.Empty);
        }
        
        private void IncompleteCorpse(ref List<Character> fightEnemies, ref List<string> fight)
        {
            Character corpse = fightEnemies[0];

            int incomplete = Game.Dice.Roll();

            fight.Add(String.Format("Кубик вскрытия: {0}", Game.Dice.Symbol(incomplete)));

            if (incomplete < 3)
            {
                corpse.Mastery -= 1;

                fight.Add(String.Format("Доктор вырезал мозг: Мастерство мертвеца снижается на единицу до {0}", corpse.Mastery));
            }
            else if (incomplete == 5)
            {
                corpse.Mastery -= 1;
                corpse.Endurance -= 1;

                fight.Add(String.Format("Доктор вырезал мозг и сердце: Мастерство мертвеца снижается на единицу до {0}," +
                    "Выносливость мертвеца снижается на единицу до {1},", corpse.Mastery, corpse.Endurance));
            }
            else if (incomplete == 6)
            {
                fight.Add("Доктор вырезал кишечник: Никакого эффекта, мертвецу он уже не нужен");
            }
            else
            {
                corpse.Endurance -= 1;

                fight.Add(String.Format("Доктор вырезал сердце: Выносливость мертвеца снижается на единицу до {0}", corpse.Endurance));
            }

            fight.Add(String.Empty);
        }

        private bool GunShot(ref List<Character> fightEnemies, ref List<string> fight, ref int enemyWounds)
        {
            int shots = Character.Protagonist.Mastery - fightEnemies[0].Mastery;

            if (shots <= 0)
                fight.Add("BAD|Противник так ловок, что вы не успеваете выстрелить из пистолета");

            else
            {
                if (Character.Protagonist.Gun < shots)
                    shots = Character.Protagonist.Gun;

                fight.Add(String.Format("Вы успеваете сделать выстрелов: {0} - {1} = {2}",
                    Character.Protagonist.Mastery, fightEnemies[0].Mastery, shots
                ));

                int enemyIndex = 0;

                for (int shot = 1; shot <= shots; shot++)
                {
                    int shotRoll = Game.Dice.Roll();

                    fight.Add(String.Format("{0} выстрел по {1}: {2}",
                        shot, fightEnemies[enemyIndex].Name, Game.Dice.Symbol(shotRoll)
                    ));

                    if (shotRoll == 6)
                    {
                        fight.Add("GOOD|Выстрел убивает врага наповал!");
                        fightEnemies[enemyIndex].Endurance = 0;
                    }
                    else
                    {
                        fight.Add("GOOD|Выстрел ранит врага на 2 Выносливости!");
                        fightEnemies[enemyIndex].Endurance -= 2;
                    }

                    if (EnemyWound(fightEnemies, ref enemyWounds, ref fight))
                        return true;

                    if (fightEnemies[enemyIndex].Endurance <= 0)
                        enemyIndex += 1;
                }
            }

            fight.Add(String.Empty);

            return false;
        }

        private void CrossbowShot(ref List<Character> fightEnemies, ref List<string> fight, ref int enemyWounds)
        {
            fightEnemies[0].Endurance -= 2;
            Character.Protagonist.Crossbow -= 1;

            enemyWounds += 1;

            fight.Add(String.Format("GOOD|Вы стреляете из арбалета: {0} теряет 2 Выносливости", fightEnemies[0].Name));
            fight.Add(String.Empty);
        }

        private int BlackWidow(ref Character hero, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(witchAttack)));

            if (witchAttack < 3)
            {
                hero.Endurance -= 2;
                fight.Add("Удар когтями: вы потеряли 2 Выносливости");
            }
            else if (witchAttack == 3)
            {
                hero.Endurance -= 3;
                fight.Add("Сильный удар: вы потеряли 3 Выносливости и в следующий раунд не сможете атаковать пытаясь подняться на ноги");

                return 3;
            }
            else if (witchAttack == 4)
            {
                fight.Add("Плевок паутиной: вы не ранены, но следующий Раунд Атаки не можете защититься");

                return 4;
            }
            else if (witchAttack == 4)
            {
                hero.Endurance -= 4;
                fight.Add("Ядовитый укус: вы потеряли 4 Выносливости");
            }
            else
            {
                int spiders = Game.Dice.Roll();
                hero.Endurance -= spiders;

                fight.Add(String.Format("Стая пауков: вы теряете {0}, но и она теряет 2 Выносливости", Game.Dice.Symbol(spiders)));

                return 6;
            }

            return 0;
        }

        private bool WerewolfDeadFight(ref Character hero, ref List<string> fight)
        {
            int wwerewolfAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(wwerewolfAttack)));

            if (wwerewolfAttack == 6)
            {
                fight.Add(String.Empty);
                fight.Add("BIG|BAD|Вы ПРОИГРАЛИ, выпала ШЕСТЁРКА :(");
                fight.Add("BAD|Перейдите на соответствующий пункт...");
                return true;
            }
            else
            {
                fight.Add("Обошлось...");
                return false;
            }
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, heroWounds = 0, enemyWounds = 0, roundWins = 0;
            
            int blackWidowLastAttack = 0;

            bool invulnerable = (Specificity == Specifics.Invulnerable);
            bool speed = ((Specificity == Specifics.NeedForSpeed) || (Specificity == Specifics.NeedForSpeedAndDead));

            Character hero = Character.Protagonist;

            if (Specificity == Specifics.RandomRoundsToFight)
                RandomRoundsToFight(ref fight);

            if (Specificity == Specifics.IncompleteCorpse)
                IncompleteCorpse(ref FightEnemies, ref fight);

            if ((Character.Protagonist.Crossbow > 0) && !invulnerable)
                CrossbowShot(ref FightEnemies, ref fight, ref enemyWounds);

            if ((Character.Protagonist.Gun > 0) && !invulnerable && GunShot(ref FightEnemies, ref fight, ref enemyWounds))
                return fight;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if ((enemy.Endurance <= 0) && !invulnerable)
                        continue;

                    fight.Add(String.Format("{0} (выносливость {1})", enemy.Name, enemy.Endurance));

                    if (blackWidowLastAttack == 4)
                    {
                        protagonistHitStrength = 0;
                        attackAlready = true;
                    }
                        
                    if (!attackAlready)
                    {
                        int protagonistRollFirst = Game.Dice.Roll();
                        int protagonistRollSecond = Game.Dice.Roll();
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + hero.Mastery + HitStrengthBonus;

                        string bonus = String.Empty;

                        if (HitStrengthBonus > 0)
                            bonus = String.Format(" + {0} бонус", HitStrengthBonus);

                        else if (HitStrengthBonus < 0)
                            bonus = String.Format(" - {0} пенальти", HitStrengthBonus);

                        else if (blackWidowLastAttack == 4)
                        {
                            bonus = " - 1 пенальти за паутину";
                            protagonistHitStrength -= 1;
                        }

                        else if (speed && !Game.Data.Triggers.Contains("Скорость"))
                        {
                            bonus = " - 1 за остутствие Скорости";
                            protagonistHitStrength -= 1;
                        }

                        fight.Add(String.Format("Сила вашего удара: {0} + {1} + {2}{3} = {4}",
                            Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                            hero.Mastery, bonus, protagonistHitStrength
                        ));
                    }

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    fight.Add(String.Format("Сила его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Mastery, enemyHitStrength
                    ));

                    bool webLastAttack = (blackWidowLastAttack == 3);

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready && !invulnerable && !webLastAttack)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Endurance -= 2;

                        roundWins += 1;

                        if (EnemyWound(FightEnemies, ref enemyWounds, ref fight))
                            return fight;
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));

                        roundWins += 1;
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        bool evenHit = (enemyHitStrength % 2 == 0);

                        if (Specificity == Specifics.WitchFight)
                            WitchFight(ref hero, ref fight);

                        else if (Specificity == Specifics.NeedForSpeedAndDead)
                        {
                            if (WerewolfDeadFight(ref hero, ref fight))
                                return fight;
                        }
                        else if (Specificity == Specifics.BlackWidow)
                        {
                            blackWidowLastAttack = BlackWidow(ref hero, ref fight);

                            if (blackWidowLastAttack == 6)
                            {
                                enemy.Endurance -= 2;

                                if (EnemyWound(FightEnemies, ref enemyWounds, ref fight))
                                    return fight;
                            }
                        }
                        else if (Game.Data.Triggers.Contains("Кольчуга") && evenHit)
                        {
                            fight.Add("Кольчуга смягчила удар: вы теряете лишь 1 Выносливость");
                            hero.Endurance -= 1;
                        }
                        else
                            hero.Endurance -= (ExtendedDamage > 0 ? ExtendedDamage : 2);

                        if (Specificity == Specifics.ElectricDamage)
                            ElectricDamage(ref hero, ref fight);

                        if (Specificity == Specifics.ToadVenom)
                            ToadVenomFight(ref hero, ref fight);

                        heroWounds += 1;

                        if (heroWounds == WoundsForTransformation)
                        {
                            hero.Change += 1;

                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Трансформация продолжается!");
                            fight.Add(String.Format("BAD|Изменение увеличилось на единицу и достигло {0}", hero.Change));
                        }

                        if (hero.Endurance <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    attackAlready = true;

                    if (Specificity == Specifics.Ulrich) 
                    {
                        enemy.Endurance -= UlrichFight(enemy.Name, ref fight, enemyHitStrength);

                        if (EnemyWound(FightEnemies, ref enemyWounds, ref fight))
                            return fight;
                    }

                    bool enoughRounds = (RoundsToFight > 0) && (RoundsToFight <= round);
                    bool notEnoughRounds = (RoundsToWin > 0) && (RoundsToWin <= round);
                    bool enoughRoundsWin = (RoundsWinToWin > 0) && (RoundsWinToWin <= roundWins);

                    if (notEnoughRounds || notEnoughRounds || enoughRoundsWin)
                    {
                        fight.Add(String.Empty);

                        if (notEnoughRounds)
                        {
                            fight.Add(String.Format("BAD|Отведённые на победу раунды истекли.", RoundsToWin));
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        }
                        else if (enoughRoundsWin)
                            fight.Add(String.Format("GOOD|Вы выиграли необходимое количество раундов.", RoundsToFight));

                        else
                            fight.Add(String.Format("GOOD|Вы продержались все отведённые на бой раунды.", RoundsToFight));

                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();
    }
}

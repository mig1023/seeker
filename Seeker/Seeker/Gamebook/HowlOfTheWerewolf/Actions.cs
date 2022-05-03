using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public enum Specifics
        {
            Nope, ElectricDamage, WitchFight, Ulrich, BlackWidow, Invulnerable, Bats, NeedForSpeed,
            NeedForSpeedAndDead, ToadVenom, IncompleteCorpse, Dehctaw, Moonstone, IcyTouch,
            GlassKnight, AcidDamage, WaterWitch, SnakeFight, Plague, StoneGriffin
        };

        public int Value { get; set; }

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int RoundsWinToWin { get; set; }
        public int RoundsFailToFail { get; set; }
        public int RoundsToFight { get; set; }
        public int WoundsToWin { get; set; }
        public int WoundsToFail { get; set; }
        public int WoundsForTransformation { get; set; }
        public int WoundsLimit { get; set; }
        public int HitStrengthBonus { get; set; }
        public int ExtendedDamage { get; set; }
        public Specifics Specificity { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { String.Format("{0}, {1} {2}", Text, Price, gold) };
            }

            if (!String.IsNullOrEmpty(Text) || (Type == "Get"))
                return new List<string> { Text };

            if (Type == "WolfFight")
                return new List<string> { "Битва с волками" };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                if (enemy.Endurance > 0)
                    enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));
                else
                    enemies.Add(String.Format("{0}\nмастерство {1} ", enemy.Name, enemy.Mastery));
            }

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Мастерство: {0}", protagonist.Mastery),
            String.Format("Выносливость: {0}/{1}", protagonist.Endurance, protagonist.MaxEndurance),
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Удача: {0}/{1}", protagonist.Luck, protagonist.MaxLuck),
                String.Format("Золото: {0}", protagonist.Gold),
                String.Format("Изменение: {0}", protagonist.Change)
            };

            if (protagonist.Crossbow > 0)
                statusLines.Add(String.Format("Арбалет: {0}", protagonist.Crossbow));

            if (protagonist.Gun > 0)
                statusLines.Add(String.Format("Пистолет: {0}", protagonist.Gun));

            if (protagonist.VanRichten > 0)
                statusLines.Add(String.Format("Выносливость Ван Рихтена: {0}", protagonist.VanRichten));

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodLuck = (firstDice + secondDice) <= protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка удачи: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), protagonist.Luck) };

            luckCheck.Add(Result(goodLuck, "УСПЕХ|НЕУДАЧА"));

            if (protagonist.Luck > 2)
            {
                protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Mastery()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int mastery = (Value > 0 ? Value : protagonist.Mastery);
            bool masteryOk = (firstDice + secondDice) <= mastery;

            List<string> masteryCheck = new List<string> { String.Format(
                "Проверка мастерства: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (masteryOk ? "<=" : ">"), mastery) };

            if (Value > 0)
                masteryCheck.Add(Result(!masteryOk, "Мастерства НЕ хватило|Мастерства ХВАТИЛО"));
            else
                masteryCheck.Add(Result(masteryOk, "Мастерства ХВАТИЛО|Мастерства НЕ хватило"));

            return masteryCheck;
        }

        public List<string> Transformation()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            string bonusLine = String.Empty;

            if ((Specificity == Specifics.Dehctaw) && Game.Option.IsTriggered("Dehctaw"))
            {
                result -= 2;
                bonusLine = " - 2 за Dehctaw";
            }
            else if ((Specificity == Specifics.Moonstone) && Game.Option.IsTriggered("Лунный камень"))
            {
                result += 3;
                bonusLine = " + 3 за Лунный камень";
            }

            bool changeOk = result > protagonist.Change;
            string cmpLine = Game.Services.Сomparison(result, protagonist.Change);

            List<string> changeCheck = new List<string> { String.Format(
                "Проверка: {0} + {1}{2} {3} {4} изменение",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), bonusLine, cmpLine, protagonist.Change) };

            changeCheck.Add(Result(changeOk, "Победил ЧЕЛОВЕК|Победил ВОЛК"));

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

            diceCheck.Add(Result(result < protagonist.Endurance, "Меньше!|Больше"));

            return diceCheck;
        }

        public List<string> DiceAnxiety()
        {
            List<string> diceCheck = new List<string> { };

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            diceCheck.Add(String.Format("На кубиках выпало: {0} + {1} = {2}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(firstDice), result));
            diceCheck.Add(String.Format("Текущий уровень тревоги: {0}", protagonist.Anxiety));

            diceCheck.Add(Result(result > protagonist.Anxiety, "Больше!|Меньше"));

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
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
                int result = firstDice + secondDice + penalty;

                if (penalty > 0)
                    penaltyLine = String.Format(" + {0} пенальти", penalty);

                competition.Add(String.Format("{0} выстрел: {1} + {2}{3} = {4}",
                    i, Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), penaltyLine, result));

                if (result > protagonist.Mastery)
                {
                    competition.Add("BAD|Это больше Мастерства: вы промахнулись...");
                    inTarget = false;
                }
                else
                    competition.Add("GOOD|Это не превышает Мастерства: вы попали в цель!");

                competition.Add(String.Empty);

                penalty += 1;
            }

            if (inTarget)
            {
                competition.Add("BIG|GOOD|Вы ВЫИГРАЛИ и получаете выигрышь в 5 золотых! :)");
                protagonist.Gold += 5;
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

            string line = "BIG|GOOD|Восстановлен";

            if (dice < 3)
            {
                protagonist.Mastery = protagonist.MaxMastery;
                line += "о Мастерство";
            }
            else if (dice > 4)
            {
                protagonist.Luck = protagonist.MaxLuck;
                line += "а Удача";
            }
            else
            {
                protagonist.Endurance = protagonist.MaxEndurance;
                line += "а Выносливость";
            }

            diceRestore.Add(line);

            return diceRestore;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            string bonus = (Value > 0 ? String.Format(" + ещё {0}", Value) : String.Empty);

            diceCheck.Add(String.Format("На кубике выпало: {0}{1}", Game.Dice.Symbol(dice), bonus));

            protagonist.Endurance -= dice + Value;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dice + Value));

            return diceCheck;
        }

        public List<string> DiceGold()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add(String.Format("На кубике выпало: {0} + ещё {1}", Game.Dice.Symbol(dice), Value));

            dice += Value;

            protagonist.Gold -= dice;

            diceCheck.Add(String.Format("BIG|GOOD|Вы нашли золотых: {0}", dice));

            return diceCheck;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledByUsed = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (protagonist.Gold < Price);

            return !(disabledByUsed || disabledByPrice);
        }

        public List<string> Get()
        {
            if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;

                if (!Multiple)
                    Used = true;

                if (BenefitList != null)
                {
                    foreach (Modification modification in BenefitList)
                        modification.Do();
                }
            }

            return new List<string> { "RELOAD" };
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (option.Contains("ЗОЛОТО >=") && (level > protagonist.Gold))
                            return false;

                        if (option.Contains("КИНЖАЛЫ >=") && (level > protagonist.SilverDaggers))
                            return false;

                        if (option.Contains("КИНЖАЛЫ <") && (level <= protagonist.SilverDaggers))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        private bool EnemyWound(List<Character> FightEnemies, ref int enemyWounds, ref List<string> fight, bool onlyCheck = false)
        {
            if (!onlyCheck)
            {
                enemyWounds += 1;
            }

            bool enemyLost = FightEnemies.Where(x => x.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() == 0;

            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
            {
                fight.Add(String.Empty);
                fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                return true;
            }
            else
            {
                return false;
            }
        }

        private int UlrichFight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int ulrichMastery = Constants.GetUlrichMastery();

            fight.Add(String.Empty);

            Game.Dice.DoubleRoll(out int ulrichRollFirst, out int ulrichRollSecond);
            int ulrichHitStrength = ulrichRollFirst + ulrichRollSecond + ulrichMastery;

            fight.Add(String.Format("Сила удара Ульриха: {0} + {1} + {2} = {3}",
                Game.Dice.Symbol(ulrichRollFirst), Game.Dice.Symbol(ulrichRollSecond), ulrichMastery, ulrichHitStrength));

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

        private int VanRichtenFight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int vanRichtenMastery = Constants.GetVanRichtenMastery();

            fight.Add(String.Empty);

            Game.Dice.DoubleRoll(out int vanRichtenRollFirst, out int vanRichtenRollSecond);
            int vanRichtenHitStrength = vanRichtenRollFirst + vanRichtenRollSecond + vanRichtenMastery;

            fight.Add(String.Format("Сила удара Ван Рихтена: {0} + {1} + {2} = {3}",
                Game.Dice.Symbol(vanRichtenRollFirst), Game.Dice.Symbol(vanRichtenRollSecond), vanRichtenMastery, vanRichtenHitStrength));

            if (vanRichtenHitStrength > enemyHitStrength)
            {
                fight.Add(String.Format("GOOD|{0} ранен", enemyName));
                return 2;
            }
            else
            {
                protagonist.VanRichten -= 2;

                if (protagonist.VanRichten <= 0)
                    fight.Add("BIG|BAD|Ван Рихтен погиб, дальше вам придётся одному :(");
                else
                    fight.Add("BAD|Ван Рихтен ранен");

                return 0;
            }
        }

        private void AddWounds(ref Character protagonist, ref List<string> fight, string diceType,
            int chance, string fail, string win, int wounds = 3, bool hitStrenghtInstead = false)
        {
            int dice = Game.Dice.Roll();

            fight.Add(String.Format("Кубик {0}: {1}", diceType, Game.Dice.Symbol(dice)));

            if ((chance >= dice) && hitStrenghtInstead)
            {
                HitStrengthBonus -= 1;
                fight.Add(String.Format("BAD|{0} {1}", fail, HitStrengthBonus));
            }
            else if (chance >= dice)
            {
                protagonist.Endurance -= wounds;
                fight.Add(String.Format("BAD|{0}", fail));
            }
            else
            {
                fight.Add(win);
            }
        }

        private void CheckAdditionalWounds(ref Character protagonist, ref List<string> fight, int wounds)
        {
            if (Specificity == Specifics.ElectricDamage)
            {
                AddWounds(ref protagonist, ref fight, "электрического разряда", 5,
                    "Вы потеряли ещё 3 Выносливость от разряда", "Разряд прошёл мимо");
            }

            if (Specificity == Specifics.AcidDamage)
            {
                AddWounds(ref protagonist, ref fight, "ожога кислотой", 6,
                    "Вы потеряли ещё 3 Выносливость от кислоты", "Обошлось...");
            }

            if (Specificity == Specifics.IcyTouch)
            {
                AddWounds(ref protagonist, ref fight, "пронизывающего холода", 5,
                    "Пронизывающий мистический холод притупил ваши чувства: теперь из Силы удара нужно будет вычитать",
                    "Вы справились с холодом, пока что...", hitStrenghtInstead: true);
            }

            if (Specificity == Specifics.ToadVenom)
            {
                AddWounds(ref protagonist, ref fight, "яда", 5, "Вы потеряли ещё 2 Выносливость от яда", "Обошлось...", wounds: 2);
            }

            if ((Specificity == Specifics.Plague) && (wounds > 2))
            {
                protagonist.Mastery -= 1;
                fight.Add("BAD|Ваше мастерство снизилось из-за чумы, которой заражены крысы");
            }
        }

        private void SnakeFight(ref Character protagonist, ref List<string> fight, int round)
        {
            if (round < 3)
            {
                protagonist.Endurance -= 3;
                fight.Add("BAD|Удушающие Кольца - теряете 3 Выносливости");
            }
            else if (round == 3)
            {
                protagonist.Mastery -= 1;
                protagonist.Endurance -= 4;
                fight.Add("BAD|Поцелуй Кобры - теряете 1 Мастерство и 4 Выносливости");
            }
            else if (round == 4)
            {
                protagonist.Endurance -= 2;
                HitStrengthBonus = -1;
                fight.Add("BAD|Удар Плетью – теряете 2 Выносливости и в следующий раз Сила Удара уменьшается на 1");
            }
            else
            {
                protagonist.Endurance -= 2;
                fight.Add("BAD|Хищные Когти - теряете 2 Выносливости");
            }
        }
        
        private void WitchFight(ref Character protagonist, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(witchAttack)));

            if (witchAttack < 3)
            {
                protagonist.Endurance -= 2;
                fight.Add("BAD|Вы потеряли 2 Выносливости");
            }
            else if (witchAttack < 5)
            {
                protagonist.Endurance -= 3;
                fight.Add("BAD|Вы потеряли 3 Выносливости");
            }
            else if (witchAttack == 5)
            {
                protagonist.Endurance -= 2;
                protagonist.Luck -= 1;
                fight.Add("BAD|Вы потеряли 2 Выносливости и 1 Удачу");
            }
            else
            {
                protagonist.Endurance -= 2;
                protagonist.Change += 1;
                fight.Add(String.Format("BAD|Вы потеряли 2 Выносливости и Трансформация продолжилась (Изменение достигло {0})", protagonist.Change));
            }
        }
        
        private bool GlassKnightFight(ref List<string> fight)
        {
            if (!Game.Option.IsTriggered("Палица"))
                return false;

            int clubAttack = Game.Dice.Roll();

            fight.Add(String.Format("Удар палицы: {0}", Game.Dice.Symbol(clubAttack)));

            if (clubAttack == 6)
            { 
                fight.Add("GOOD|Точный удар палицы разбивает рыцаря вдребезги!");
                return true;
            }
            else
            {
                fight.Add("Удар не так силён, чтобы рыцарь разбился...");
                return false;
            }
        }

        private void MasteryRoundsToFight(ref List<string> fight)
        {
            RoundsToFight = 15 - protagonist.Mastery;
            fight.Add(String.Format("Вам необходимо продержаться: 15 - {0} = {1} раундов", protagonist.Mastery, RoundsToFight));
            fight.Add(String.Empty);
        }
        
        private void MasteryRoundToWin(ref List<string> fight)
        {
            RoundsToWin = protagonist.Mastery - 1;
            fight.Add(String.Format("Вам необходимо победить за: {0} - 1 = {1} раундов", protagonist.Mastery, RoundsToWin));
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
            int shots = protagonist.Mastery - fightEnemies[0].Mastery;

            if (shots <= 0)
                fight.Add("BAD|Противник так ловок, что вы не успеваете выстрелить из пистолета");

            else
            {
                if (protagonist.Gun < shots)
                    shots = protagonist.Gun;

                fight.Add(String.Format("Вы успеваете сделать выстрелов: {0} - {1} = {2}",
                    protagonist.Mastery, fightEnemies[0].Mastery, shots));

                int enemyIndex = 0;

                for (int shot = 1; shot <= shots; shot++)
                {
                    int shotRoll = Game.Dice.Roll();

                    fight.Add(String.Format("{0} выстрел по {1}: {2}",
                        shot, fightEnemies[enemyIndex].Name, Game.Dice.Symbol(shotRoll)));

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
            protagonist.Crossbow -= 1;

            enemyWounds += 1;

            fight.Add(String.Format("GOOD|Вы стреляете из арбалета: {0} теряет 2 Выносливости", fightEnemies[0].Name));
            fight.Add(String.Empty);
        }

        private int BlackWidow(ref Character protagonist, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(witchAttack)));

            if (witchAttack < 3)
            {
                protagonist.Endurance -= 2;
                fight.Add("Удар когтями: вы потеряли 2 Выносливости");
            }
            else if (witchAttack == 3)
            {
                protagonist.Endurance -= 3;
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
                protagonist.Endurance -= 4;
                fight.Add("Ядовитый укус: вы потеряли 4 Выносливости");
            }
            else
            {
                int spiders = Game.Dice.Roll();
                protagonist.Endurance -= spiders;

                fight.Add(String.Format("Стая пауков: вы теряете {0}, но и она теряет 2 Выносливости", Game.Dice.Symbol(spiders)));

                return 6;
            }

            return 0;
        }

        private bool WerewolfDeadFight(ref Character protagonist, ref List<string> fight)
        {
            int werewolfAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(werewolfAttack)));

            if (werewolfAttack == 6)
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

        private void PassageDice(out int dice, out int passage)
        {
            dice = Game.Dice.Roll();
            passage = (int)Math.Ceiling(dice / 2.0);
        }

        public List<string> WolfFight()
        {
            List<string> fight = new List<string>();
            Character enemy = Enemies[0];
            Actions action = this;

            PassageDice(out int dice, out int protagonistPassage);

            fight.Add(String.Format("Вы обороняете: {0} / 2 = {1}, это {2}",
                Game.Dice.Symbol(dice), protagonistPassage, Constants.GetPassageName()[protagonistPassage]));

            fight.Add(String.Empty);

            int woulfCount = 0;

            for (int wolf = 1; wolf <= 8; wolf++)
            {
                PassageDice(out int wolfDice, out int wolfPassage);

                fight.Add(String.Format("{0} волк: {1} / 2 = {2}, ломится через {3}",
                    wolf, Game.Dice.Symbol(wolfDice), wolfPassage, Constants.GetPassageName()[wolfPassage]));

                if (protagonistPassage == wolfPassage)
                    woulfCount += 1;
            }

            fight.Add(String.Empty);

            if (woulfCount <= 0)
            {
                fight.Add("GOOD|BIG|Вам повезло: всю работу за вас сделали товарищи :)");
                return fight;
            }
            else
                fight.Add(String.Format("BOLD|Вам предстоит сразиться с волками в количестве: {0}", woulfCount));

            fight.Add(String.Empty);

            Enemies.Clear();

            Paragraphs.EnemyMultiplier(woulfCount, ref action, enemy);

            fight.AddRange(Fight());

            return fight;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, protagonistWounds = 0, enemyWounds = 0, roundWins = 0, roundFails = 0;
            
            int blackWidowLastAttack = 0;

            bool invulnerable = (Specificity == Specifics.Invulnerable);
            bool speed = ((Specificity == Specifics.NeedForSpeed) || (Specificity == Specifics.NeedForSpeedAndDead));

            if (Specificity == Specifics.Bats)
                MasteryRoundsToFight(ref fight);

            if (Specificity == Specifics.WaterWitch)
                MasteryRoundToWin(ref fight);

            if (Specificity == Specifics.IncompleteCorpse)
                IncompleteCorpse(ref FightEnemies, ref fight);

            if ((protagonist.Crossbow > 0) && !invulnerable)
                CrossbowShot(ref FightEnemies, ref fight, ref enemyWounds);

            if ((protagonist.Gun > 0) && !invulnerable && GunShot(ref FightEnemies, ref fight, ref enemyWounds))
                return fight;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

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
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Mastery + HitStrengthBonus;

                        string bonus = String.Empty;

                        if (HitStrengthBonus > 0)
                            bonus = String.Format(" + {0} бонус", HitStrengthBonus);

                        else if (HitStrengthBonus < 0)
                            bonus = String.Format(" - {0} пенальти", Math.Abs(HitStrengthBonus));

                        else if (blackWidowLastAttack == 4)
                        {
                            bonus = " - 1 пенальти за паутину";
                            protagonistHitStrength -= 1;
                        }

                        else if (speed && !Game.Option.IsTriggered("Скорость"))
                        {
                            bonus = " - 1 за остутствие Скорости";
                            protagonistHitStrength -= 1;
                        }

                        fight.Add(String.Format("Сила вашего удара: {0} + {1} + {2}{3} = {4}",
                            Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                            protagonist.Mastery, bonus, protagonistHitStrength));

                        if ((Specificity == Specifics.SnakeFight) && (round >= 2))
                            HitStrengthBonus = 0;
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    fight.Add(String.Format("Сила его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Mastery, enemyHitStrength));

                    bool webLastAttack = (blackWidowLastAttack == 3);

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready && !invulnerable && !webLastAttack)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Endurance -= (Specificity == Specifics.StoneGriffin ? 1 : 2);

                        roundWins += 1;

                        if ((Specificity == Specifics.GlassKnight) && GlassKnightFight(ref fight))
                            enemy.Endurance = 0;

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
                            WitchFight(ref protagonist, ref fight);

                        else if (Specificity == Specifics.NeedForSpeedAndDead)
                        {
                            if (WerewolfDeadFight(ref protagonist, ref fight))
                                return fight;
                        }
                        else if (Specificity == Specifics.BlackWidow)
                        {
                            blackWidowLastAttack = BlackWidow(ref protagonist, ref fight);

                            if (blackWidowLastAttack == 6)
                            {
                                enemy.Endurance -= 2;

                                if (EnemyWound(FightEnemies, ref enemyWounds, ref fight))
                                    return fight;
                            }
                        }
                        else if (Specificity == Specifics.SnakeFight)
                            SnakeFight(ref protagonist, ref fight, round);

                        else if (Game.Option.IsTriggered("Кольчуга") && evenHit)
                        {
                            fight.Add("Кольчуга защитила вас: вы теряете лишь 1 Выносливость");
                            protagonist.Endurance -= 1;
                        }
                        else
                            protagonist.Endurance -= (ExtendedDamage > 0 ? ExtendedDamage : 2);

                        roundFails += 1;
                        protagonistWounds += 1;

                        CheckAdditionalWounds(ref protagonist, ref fight, protagonistWounds);

                        if (protagonistWounds == WoundsForTransformation)
                        {
                            protagonist.Change += 1;

                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Трансформация продолжается!");
                            fight.Add(String.Format("BAD|Изменение увеличилось на единицу и достигло {0}", protagonist.Change));
                        }

                        if ((protagonist.Endurance <= 0) || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
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
                        enemy.Endurance -= UlrichFight(enemy.Name, ref fight, enemyHitStrength);
                    
                    if (protagonist.VanRichten > 0) 
                        enemy.Endurance -= VanRichtenFight(enemy.Name, ref fight, enemyHitStrength);

                    if (EnemyWound(FightEnemies, ref enemyWounds, ref fight, onlyCheck: true))
                        return fight;

                    bool enoughRounds = (RoundsToFight > 0) && (RoundsToFight <= round);
                    bool notEnoughRounds = (RoundsToWin > 0) && (RoundsToWin <= round);
                    bool enoughRoundsWin = (RoundsWinToWin > 0) && (RoundsWinToWin <= roundWins);
                    bool enoughRoundsFail = (RoundsFailToFail > 0) && (RoundsFailToFail <= roundFails);

                    if (notEnoughRounds || notEnoughRounds || enoughRoundsWin || enoughRoundsFail)
                    {
                        fight.Add(String.Empty);

                        if (notEnoughRounds)
                            fight.Add("BIG|BAD|Отведённые на победу раунды истекли :(");

                        else if (enoughRoundsFail)
                            fight.Add("BIG|BAD|Вы проиграли слишком много раундов :(");

                        else if (enoughRoundsWin)
                            fight.Add("BIG|GOOD|Вы выиграли необходимое количество раундов :)");

                        else
                            fight.Add("BIG|GOOD|Вы продержались все отведённые на бой раунды :)");

                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled()
        {
            bool enduranceDamage = protagonist.Endurance < protagonist.MaxEndurance;
            bool masteryDamage = protagonist.Mastery < protagonist.MaxMastery;
            bool luckDamage = protagonist.Luck < protagonist.MaxLuck;

            return (enduranceDamage || masteryDamage || luckDamage);
        }

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
                protagonist.Endurance = protagonist.MaxEndurance;

            else if (healingLevel == -2)
                protagonist.Mastery = protagonist.MaxMastery;

            else if (healingLevel == -3)
                protagonist.Luck = protagonist.MaxLuck;

            else
                protagonist.Endurance += healingLevel;
        }
    }
}

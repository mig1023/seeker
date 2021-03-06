﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

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
                string gold = Game.Other.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { String.Format("{0}, {1} {2}", Text, Price, gold) };
            }

            if (!String.IsNullOrEmpty(Text) || (Name == "Get"))
                return new List<string> { Text };

            if (Name == "WolfFight")
                return new List<string> { "Битва с волками" };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                if (enemy.Endurance > 0)
                    enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));
                else
                    enemies.Add(String.Format("{0}\nмастерство {1} ", enemy.Name, enemy.Mastery));

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Мастерство: {0}", Character.Protagonist.Mastery),
            String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
            String.Format("Удача: {0}", Character.Protagonist.Luck)
        };

        public override List<string> AdditionalStatus()
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

            if (Character.Protagonist.VanRichten > 0)
                statusLines.Add(String.Format("Выносливость Ван Рихтена: {0}", Character.Protagonist.VanRichten));

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);

        public List<string> Luck()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodLuck = (fisrtDice + secondDice) <= Character.Protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка удачи: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), Character.Protagonist.Luck) };

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

            int mastery = (Value > 0 ? Value : Character.Protagonist.Mastery);
            bool masteryOk = (fisrtDice + secondDice) <= mastery;

            List<string> masteryCheck = new List<string> { String.Format(
                "Проверка мастерства: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (masteryOk ? "<=" : ">"), mastery) };

            if (Value > 0)
                masteryCheck.Add(masteryOk ? "BIG|BAD|Мастерства ХВАТИЛО :(" : "BIG|GOOD|Мастерства НЕ хватило :)");
            else
                masteryCheck.Add(masteryOk ? "BIG|GOOD|Мастерства ХВАТИЛО :)" : "BIG|BAD|Мастерства НЕ хватило :(");

            return masteryCheck;
        }

        public List<string> Transformation()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            int result = fisrtDice + secondDice;

            string bonusLine = String.Empty;

            if ((Specificity == Specifics.Dehctaw) && Game.Data.Triggers.Contains("Dehctaw"))
            {
                result -= 2;
                bonusLine = " - 2 за Dehctaw";
            }
            else if ((Specificity == Specifics.Moonstone) && Game.Data.Triggers.Contains("Лунный камень"))
            {
                result += 3;
                bonusLine = " + 3 за Лунный камень";
            }

            bool changeOk = result > Character.Protagonist.Change;
            string cmpLine = Game.Other.Сomparison(result, Character.Protagonist.Change);

            List<string> changeCheck = new List<string> { String.Format(
                "Проверка: {0} + {1}{2} {3} {4} изменение",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), bonusLine, cmpLine, Character.Protagonist.Change) };

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

                competition.Add(String.Format("{0} выстрел: {1} + {2}{3} = {4}",
                    i, Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), penaltyLine, result));

                if (result > Character.Protagonist.Mastery)
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

            string line = "BIG|GOOD|Восстановлен";

            if (dice < 3)
            {
                Character.Protagonist.Mastery = Character.Protagonist.MaxMastery;
                line += "о Мастерство";
            }
            else if (dice > 4)
            {
                Character.Protagonist.Luck = Character.Protagonist.MaxLuck;
                line += "а Удача";
            }
            else
            {
                Character.Protagonist.Endurance = Character.Protagonist.MaxEndurance;
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

        public override bool IsButtonEnabled()
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

                if (BenefitList != null)
                    foreach (Modification modification in BenefitList)
                        modification.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (option.Contains("ЗОЛОТО >=") && (level > Character.Protagonist.Gold))
                            return false;

                        if (option.Contains("КИНЖАЛЫ >=") && (level > Character.Protagonist.SilverDaggers))
                            return false;

                        if (option.Contains("КИНЖАЛЫ <") && (level <= Character.Protagonist.SilverDaggers))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        private bool EnemyWound(List<Character> FightEnemies, ref int enemyWounds, ref List<string> fight, bool onlyCheck = false)
        {
            if (!onlyCheck)
                enemyWounds += 1;

            bool enemyLost = FightEnemies.Where(x => x.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() == 0;

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

            int vanRichtenRollFirst = Game.Dice.Roll();
            int vanRichtenRollSecond = Game.Dice.Roll();
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
                Character.Protagonist.VanRichten -= 2;

                if (Character.Protagonist.VanRichten <= 0)
                    fight.Add("BIG|BAD|Ван Рихтен погиб, дальше вам придётся одному :(");
                else
                    fight.Add("BAD|Ван Рихтен ранен");

                return 0;
            }
        }

        private void AddWounds(ref Character hero, ref List<string> fight, string diceType,
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
                hero.Endurance -= wounds;
                fight.Add(String.Format("BAD|{0}", fail));
            }
            else
                fight.Add(win);
        }

        private void CheckAdditionalWounds(ref Character hero, ref List<string> fight, int wounds)
        {
            if (Specificity == Specifics.ElectricDamage)
                AddWounds(ref hero, ref fight, "электрического разряда", 5, "Вы потеряли ещё 3 Выносливость от разряда", "Разряд прошёл мимо");

            if (Specificity == Specifics.AcidDamage)
                AddWounds(ref hero, ref fight, "ожога кислотой", 6, "Вы потеряли ещё 3 Выносливость от кислоты", "Обошлось...");

            if (Specificity == Specifics.IcyTouch)
                AddWounds(ref hero, ref fight, "пронизывающего холода", 5,
                    "Пронизывающий мистический холод притупил ваши чувства: теперь из Силы удара нужно будет вычитать",
                    "Вы справились с холодом, пока что...", hitStrenghtInstead: true);

            if (Specificity == Specifics.ToadVenom)
                AddWounds(ref hero, ref fight, "яда", 5, "Вы потеряли ещё 2 Выносливость от яда", "Обошлось...", wounds: 2);

            if ((Specificity == Specifics.Plague) && (wounds > 2))
            {
                hero.Mastery -= 1;
                fight.Add("BAD|Ваше мастерство снизилось из-за чумы, которой заражены крысы");
            }
        }

        private void SnakeFight(ref Character hero, ref List<string> fight, int round)
        {
            if (round < 3)
            {
                hero.Endurance -= 3;
                fight.Add("BAD|Удушающие Кольца - теряете 3 Выносливости");
            }
            else if (round == 3)
            {
                hero.Mastery -= 1;
                hero.Endurance -= 4;
                fight.Add("BAD|Поцелуй Кобры - теряете 1 Мастерство и 4 Выносливости");
            }
            else if (round == 4)
            {
                hero.Endurance -= 2;
                HitStrengthBonus = -1;
                fight.Add("BAD|Удар Плетью – теряете 2 Выносливости и в следующий раз Сила Удара уменьшается на 1");
            }
            else
            {
                hero.Endurance -= 2;
                fight.Add("BAD|Хищные Когти - теряете 2 Выносливости");
            }
        }
        
        private void WitchFight(ref Character hero, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(witchAttack)));

            if (witchAttack < 3)
            {
                hero.Endurance -= 2;
                fight.Add("BAD|Вы потеряли 2 Выносливости");
            }
            else if (witchAttack < 5)
            {
                hero.Endurance -= 3;
                fight.Add("BAD|Вы потеряли 3 Выносливости");
            }
            else if (witchAttack == 5)
            {
                hero.Endurance -= 2;
                hero.Luck -= 1;
                fight.Add("BAD|Вы потеряли 2 Выносливости и 1 Удачу");
            }
            else
            {
                hero.Endurance -= 2;
                hero.Change += 1;
                fight.Add(String.Format("BAD|Вы потеряли 2 Выносливости и Трансформация продолжилась (Изменение достигло {0})", hero.Change));
            }
        }
        
        private bool GlassKnightFight(ref List<string> fight)
        {
            if (!Game.Data.Triggers.Contains("Палица"))
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
            RoundsToFight = 15 - Character.Protagonist.Mastery;
            fight.Add(String.Format("Вам необходимо продержаться: 15 - {0} = {1} раундов", Character.Protagonist.Mastery, RoundsToFight));
            fight.Add(String.Empty);
        }
        
        private void MasteryRoundToWin(ref List<string> fight)
        {
            RoundsToWin = Character.Protagonist.Mastery - 1;
            fight.Add(String.Format("Вам необходимо победить за: {0} - 1 = {1} раундов", Character.Protagonist.Mastery, RoundsToWin));
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
                    Character.Protagonist.Mastery, fightEnemies[0].Mastery, shots));

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

            PassageDice(out int dice, out int heroPassage);

            fight.Add(String.Format("Вы обороняете: {0} / 2 = {1}, это {2}",
                Game.Dice.Symbol(dice), heroPassage, Constants.GetPassageName()[heroPassage]));

            fight.Add(String.Empty);

            int woulfCount = 0;

            for (int wolf = 1; wolf <= 8; wolf++)
            {
                PassageDice(out int wolfDice, out int wolfPassage);

                fight.Add(String.Format("{0} волк: {1} / 2 = {2}, ломится через {3}",
                    wolf, Game.Dice.Symbol(wolfDice), wolfPassage, Constants.GetPassageName()[wolfPassage]));

                if (heroPassage == wolfPassage)
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

            int round = 1, heroWounds = 0, enemyWounds = 0, roundWins = 0, roundFails = 0;
            
            int blackWidowLastAttack = 0;

            bool invulnerable = (Specificity == Specifics.Invulnerable);
            bool speed = ((Specificity == Specifics.NeedForSpeed) || (Specificity == Specifics.NeedForSpeedAndDead));

            Character hero = Character.Protagonist;

            if (Specificity == Specifics.Bats)
                MasteryRoundsToFight(ref fight);

            if (Specificity == Specifics.WaterWitch)
                MasteryRoundToWin(ref fight);

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
                            bonus = String.Format(" - {0} пенальти", Math.Abs(HitStrengthBonus));

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
                            hero.Mastery, bonus, protagonistHitStrength));

                        if ((Specificity == Specifics.SnakeFight) && (round >= 2))
                            HitStrengthBonus = 0;
                    }

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
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
                        else if (Specificity == Specifics.SnakeFight)
                            SnakeFight(ref hero, ref fight, round);

                        else if (Game.Data.Triggers.Contains("Кольчуга") && evenHit)
                        {
                            fight.Add("Кольчуга защитила вас: вы теряете лишь 1 Выносливость");
                            hero.Endurance -= 1;
                        }
                        else
                            hero.Endurance -= (ExtendedDamage > 0 ? ExtendedDamage : 2);

                        roundFails += 1;
                        heroWounds += 1;

                        CheckAdditionalWounds(ref hero, ref fight, heroWounds);

                        if (heroWounds == WoundsForTransformation)
                        {
                            hero.Change += 1;

                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Трансформация продолжается!");
                            fight.Add(String.Format("BAD|Изменение увеличилось на единицу и достигло {0}", hero.Change));
                        }

                        if ((hero.Endurance <= 0) || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
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
                    
                    if (Character.Protagonist.VanRichten > 0) 
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

        public override bool IsHealingEnabled() => true;

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
                Character.Protagonist.Endurance = Character.Protagonist.MaxEndurance;

            else if (healingLevel == -2)
                Character.Protagonist.Mastery = Character.Protagonist.MaxMastery;

            else if (healingLevel == -3)
                Character.Protagonist.Luck = Character.Protagonist.MaxLuck;

            else
                Character.Protagonist.Endurance += healingLevel;
        }
    }
}

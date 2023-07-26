using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Services
    {
        public static bool EnemyWound(List<Character> FightEnemies, ref int enemyWounds, ref List<string> fight,
            int WoundsToWin, int WoundsLimit, bool onlyCheck = false)
        {
            if (!onlyCheck)
            {
                enemyWounds += 1;
            }

            bool enemyLost = FightEnemies.Where(x => x.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() == 0;

            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
            {
                fight.Add(String.Empty);
                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int UlrichFight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int ulrichMastery = Constants.GetUlrichMastery();

            fight.Add(String.Empty);

            Game.Dice.DoubleRoll(out int ulrichRollFirst, out int ulrichRollSecond);
            int ulrichHitStrength = ulrichRollFirst + ulrichRollSecond + ulrichMastery;

            fight.Add($"Сила удара Ульриха: " +
                $"{Game.Dice.Symbol(ulrichRollFirst)} + " +
                $"{Game.Dice.Symbol(ulrichRollSecond)} + " +
                $"{ulrichMastery} = {ulrichHitStrength}");

            if (ulrichHitStrength > enemyHitStrength)
            {
                fight.Add($"GOOD|{enemyName} ранен");
                return 2;
            }
            else
            {
                fight.Add("BOLD|Ульрих не смог ранить врага");
                return 0;
            }
        }

        public static int VanRichtenFight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int vanRichtenMastery = Constants.GetVanRichtenMastery();

            fight.Add(String.Empty);

            Game.Dice.DoubleRoll(out int vanRichtenRollFirst, out int vanRichtenRollSecond);
            int vanRichtenHitStrength = vanRichtenRollFirst + vanRichtenRollSecond + vanRichtenMastery;

            fight.Add($"Сила удара Ван Рихтена: " +
                $"{Game.Dice.Symbol(vanRichtenRollFirst)} + " +
                $"{Game.Dice.Symbol(vanRichtenRollSecond)} + " +
                $"{vanRichtenMastery} = {vanRichtenHitStrength}");

            if (vanRichtenHitStrength > enemyHitStrength)
            {
                fight.Add($"GOOD|{enemyName} ранен");
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

        public static int AddWounds(ref Character protagonist, ref List<string> fight, string diceType,
            int chance, string fail, string win, int hisStrength, int wounds = 3, bool hitStrenghtInstead = false)
        {
            int dice = Game.Dice.Roll();

            fight.Add($"Кубик {diceType}: {Game.Dice.Symbol(dice)}");

            if ((chance >= dice) && hitStrenghtInstead)
            {
                fight.Add($"BAD|{fail} {hisStrength}");
                return -1;
            }
            else if (chance >= dice)
            {
                protagonist.Endurance -= wounds;
                fight.Add($"BAD|{fail}");
            }
            else
            {
                fight.Add(win);
            }

            return 0;
        }

        public static int CheckAdditionalWounds(ref Character protagonist, ref List<string> fight,
            int wounds, int hisStrength, Actions.Specifics Specificity)
        {
            if (Specificity == Actions.Specifics.ElectricDamage)
            {
                return AddWounds(ref protagonist, ref fight, "электрического разряда", 5,
                    "Вы потеряли ещё 3 Выносливость от разряда", "Разряд прошёл мимо", hisStrength);
            }

            if (Specificity == Actions.Specifics.AcidDamage)
            {
                return AddWounds(ref protagonist, ref fight, "ожога кислотой", 6,
                    "Вы потеряли ещё 3 Выносливость от кислоты", "Обошлось...", hisStrength);
            }

            if (Specificity == Actions.Specifics.IcyTouch)
            {
                return AddWounds(ref protagonist, ref fight, "пронизывающего холода", 5,
                    "Пронизывающий мистический холод притупил ваши чувства: теперь из Силы удара нужно будет вычитать",
                    "Вы справились с холодом, пока что...", hisStrength, hitStrenghtInstead: true);
            }

            if (Specificity == Actions.Specifics.ToadVenom)
            {
                return AddWounds(ref protagonist, ref fight, "яда", 5, "Вы потеряли ещё 2 Выносливость от яда",
                    "Обошлось...", hisStrength, wounds: 2);
            }

            if ((Specificity == Actions.Specifics.Plague) && (wounds > 2))
            {
                protagonist.Mastery -= 1;
                fight.Add("BAD|Ваше мастерство снизилось из-за чумы, которой заражены крысы");
            }

            return 0;
        }

        public static int SnakeFight(ref Character protagonist, ref List<string> fight, int round)
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
                fight.Add("BAD|Удар Плетью – теряете 2 Выносливости и в следующий раз Сила Удара уменьшается на 1");
                return -1;
            }
            else
            {
                protagonist.Endurance -= 2;
                fight.Add("BAD|Хищные Когти - теряете 2 Выносливости");
            }

            return 0;
        }

        public static void WitchFight(ref Character protagonist, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add($"Кубик атаки: {Game.Dice.Symbol(witchAttack)}");

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

        public static bool GlassKnightFight(ref List<string> fight)
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

        public static int MasteryRoundsToFight(ref List<string> fight)
        {
            int roundsToFight = 15 - Character.Protagonist.Mastery;
            fight.Add(String.Format("Вам необходимо продержаться: 15 - {0} = {1} раундов", Character.Protagonist.Mastery, roundsToFight));
            fight.Add(String.Empty);

            return roundsToFight;
        }

        public static int MasteryRoundToWin(ref List<string> fight)
        {
            int roundsToWin = Character.Protagonist.Mastery - 1;
            fight.Add(String.Format("Вам необходимо победить за: {0} - 1 = {1} раундов", Character.Protagonist.Mastery, roundsToWin));
            fight.Add(String.Empty);

            return roundsToWin;
        }

        public static void IncompleteCorpse(ref List<Character> fightEnemies, ref List<string> fight)
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

        public static bool GunShot(ref List<Character> fightEnemies, ref List<string> fight,
            ref int enemyWounds, int WoundsToWin, int WoundsLimit)
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

                    if (EnemyWound(fightEnemies, ref enemyWounds, ref fight, WoundsToWin, WoundsLimit))
                        return true;

                    if (fightEnemies[enemyIndex].Endurance <= 0)
                        enemyIndex += 1;
                }
            }

            fight.Add(String.Empty);

            return false;
        }

        public static void CrossbowShot(ref List<Character> fightEnemies, ref List<string> fight, ref int enemyWounds)
        {
            fightEnemies[0].Endurance -= 2;
            Character.Protagonist.Crossbow -= 1;

            enemyWounds += 1;

            fight.Add(String.Format("GOOD|Вы стреляете из арбалета: {0} теряет 2 Выносливости", fightEnemies[0].Name));
            fight.Add(String.Empty);
        }

        public static int BlackWidow(ref Character protagonist, ref List<string> fight)
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

        public static bool WerewolfDeadFight(ref Character protagonist, ref List<string> fight)
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

        public static void PassageDice(out int dice, out int passage)
        {
            dice = Game.Dice.Roll();
            passage = (int)Math.Ceiling(dice / 2.0);
        }
    }
}

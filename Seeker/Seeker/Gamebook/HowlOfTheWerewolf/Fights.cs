using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Fights
    {
        public static bool EnemyWound(List<Character> FightEnemies, ref int enemyWounds, ref List<string> fight,
            int WoundsToWin, int WoundsLimit, bool onlyCheck = false)
        {
            if (!onlyCheck)
            {
                enemyWounds += 1;
            }

            bool enemyLost = FightEnemies
                .Where(x => x.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0))
                .Count() == 0;

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

        public static int MasteryRoundsToFight(ref List<string> fight)
        {
            int roundsToFight = 15 - Character.Protagonist.Mastery;

            fight.Add($"Вам необходимо продержаться: 15 - " +
                $"{Character.Protagonist.Mastery} = {roundsToFight} раундов");

            fight.Add(String.Empty);

            return roundsToFight;
        }

        public static int MasteryRoundToWin(ref List<string> fight)
        {
            int roundsToWin = Character.Protagonist.Mastery - 1;

            fight.Add($"Вам необходимо победить за: " +
                $"{Character.Protagonist.Mastery} - 1 = {roundsToWin} раундов");

            fight.Add(String.Empty);

            return roundsToWin;
        }

        public static bool GunShot(ref List<Character> fightEnemies, ref List<string> fight,
            ref int enemyWounds, int WoundsToWin, int WoundsLimit)
        {
            int shots = Character.Protagonist.Mastery - fightEnemies[0].Mastery;

            if (shots <= 0)
            {
                fight.Add("BAD|Противник так ловок, что вы не успеваете выстрелить из пистолета");
            }
            else
            {
                if (Character.Protagonist.Gun < shots)
                    shots = Character.Protagonist.Gun;

                fight.Add($"Вы успеваете сделать выстрелов: " +
                    $"{Character.Protagonist.Mastery} - " +
                    $"{fightEnemies[0].Mastery} = {shots}");

                int enemyIndex = 0;

                for (int shot = 1; shot <= shots; shot++)
                {
                    int shotRoll = Game.Dice.Roll();

                    fight.Add($"{shot} выстрел по {fightEnemies[enemyIndex].Name}: " +
                        $"{Game.Dice.Symbol(shotRoll)}");

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

            fight.Add($"GOOD|Вы стреляете из арбалета: {fightEnemies[0].Name} теряет 2 Выносливости");
            fight.Add(String.Empty);
        }

        public static void PassageDice(out int dice, out int passage)
        {
            dice = Game.Dice.Roll();
            passage = (int)Math.Ceiling(dice / 2.0);
        }
    }
}

using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Fights
    {
        public static int WoundConverter(bool Wound, bool LastWound)
        {
            if (LastWound)
            {
                return 1;
            }
            else if (Wound)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public static string SeverityOfWound(int Health, bool hero = false)
        {
            if (hero)
            {
                switch (Health)
                {
                    default:
                    case 2:
                        return "ранен";
                    case 1:
                        return "тяжело ранен";
                    case 0:
                        return "убит";
                }
            }
            else
            {
                switch (Health)
                {
                    default:
                    case 2:
                        return "ранил";
                    case 1:
                        return "тяжело ранил";
                    case 0:
                        return "убил";
                }

            }
        }

        public static int UseGloryInFight(Character enemy, int protagonistHitStrength,
            bool autoHit, bool autoFail, out string usedGlory)
        {
            bool cantFightOtherwise = protagonistHitStrength < enemy.Defence;
            int availableGlory = (Character.Protagonist.Glory - Character.Protagonist.Shame);

            if (autoHit || autoFail || !cantFightOtherwise)
            {
                usedGlory = String.Empty;
                return 0;
            }

            if (cantFightOtherwise && (availableGlory < 1))
            {
                usedGlory = "Кажется, что положение безнадёжно...";
                return 0;
            }

            int needGlory = enemy.Defence - protagonistHitStrength + 1;

            if (needGlory > availableGlory)
            {
                usedGlory = "Не хватит очков Славы, чтобы что-то исправить...";
                return -1;
            }
            else
            {
                Character.Protagonist.Glory -= needGlory;

                usedGlory = "Вам придётся использовать Славу!";
                return needGlory;
            }
        }

        public static void OnlyOneDice(ref List<string> fight) =>
            fight.Add($"GRAY|Из-за тяжести ранения, мощность удара будет определяться только одним кубом");

        public static bool EveryoneIsSeriouslyWounded(Character protagonist, List<Character> enemies)
        {
            if (protagonist.Health > 1)
                return false;

            return NoMoreEnemies(enemies, noHealthy: true);
        }

        public static bool NoMoreEnemies(List<Character> enemies, bool noHealthy = false)
        {
            foreach (Character enemy in enemies)
            {
                if (!noHealthy && enemy.Health > 0)
                    return false;

                if (noHealthy && (enemy.Health > 1))
                    return false;
            }

            return true;
        }

        public static int ComradeBonus(List<Character> enemies, int currentEnemy)
        {
            int bonus = 0;

            if ((currentEnemy + 1) == enemies.Count)
                return bonus;

            for (int i = currentEnemy + 1; i < enemies.Count; i++)
            {
                if (enemies[i].Health > 1)
                    bonus += 1;
            }

            return bonus;
        }
    }
}

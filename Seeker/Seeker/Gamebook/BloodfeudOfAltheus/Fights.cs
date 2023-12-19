﻿using System.Collections.Generic;

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

        public static int UseGloryInFight(Character enemy, int protagonistHitStrength,
            bool autoHit, bool autoFail, ref List<string> fight)
        {
            bool cantFightOtherwise = protagonistHitStrength < enemy.Defence;
            int availableGlory = (Character.Protagonist.Glory - Character.Protagonist.Shame);

            if (autoHit || autoFail || !cantFightOtherwise)
            {
                return 0;
            }

            if (cantFightOtherwise && (availableGlory < 1))
            {
                fight.Add("Кажется, что положение безнадёжно...");
                return 0;
            }

            int needGlory = enemy.Defence - protagonistHitStrength + 1;

            if (needGlory > availableGlory)
            {
                fight.Add("Не хватит очков Славы, чтобы что-то исправить...");
                return -1;
            }
            else
            {
                fight.Add("Вам придётся использовать Славу!");

                Character.Protagonist.Glory -= needGlory;
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

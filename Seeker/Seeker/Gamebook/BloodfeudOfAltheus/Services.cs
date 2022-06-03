using System.Collections.Generic;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Services
    {
        public static bool IsPosibleResurrection()
        {
            bool normal = (Character.Protagonist.Resurrection > 0);
            bool brooch = (Character.Protagonist.BroochResurrection > 0) && (
                (Character.Protagonist.Glory - Character.Protagonist.Shame) >= 10);

            return (normal || brooch);
        }

        public static int UseGloryInFight(Character enemy, ref List<string> fight)
        {
            bool graveInjury = (Character.Protagonist.Health < 2);
            bool cantFightOtherwise = (Character.Protagonist.Strength + (graveInjury ? 6 : 12) < enemy.Defence);

            int availableGlory = (Character.Protagonist.Glory - Character.Protagonist.Shame);

            if (cantFightOtherwise && (availableGlory < 1))
            {
                fight.Add("Кажется, что положение безнадёжно...");
                return -1;
            }

            if (!cantFightOtherwise)
                return 0;

            int needGlory = (enemy.Defence - Character.Protagonist.Strength + (graveInjury ? 6 : 12) + 2);

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
                if (enemies[i].Health > 1)
                    bonus += 1;

            return bonus;
        }
    }
}

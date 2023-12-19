using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.OrcsDay
{
    class Fights
    {
        public static int Protection(ref List<string> fight)
        {
            if (Character.Protagonist.Wits > Character.Protagonist.Luck)
            {
                fight.Add("Используем мозги, т.к. их больше, чем удачи");
                return Character.Protagonist.Wits;
            }
            else
            {
                fight.Add("Полагаемся на удачу, т.к. на неё больше надежды, чем на мозги");
                return Character.Protagonist.Luck;
            }
        }

        public static void Bonus(Character enemy, bool sub = false, int bonusLevel = 2)
        {
            enemy.Attack += bonusLevel * (sub ? -1 : 1);
            enemy.Defense += bonusLevel * (sub ? -1 : 1);
        }

        public static void WinTriggers(string enemyName, bool GirlHelp)
        {
            List<string> enemies = new List<string> { "Галрос Бессмертный", "Мортимер Нечихающий" };

            if (enemies.Contains(enemyName))
                Game.Option.Trigger(enemyName);

            if (GirlHelp && !Game.Option.IsTriggered("Девушка погибла"))
                Game.Option.Trigger("Вместе с ней");
        }

        public static bool VsAdvanturer(string name) =>
            name == "Приключенцы";
    }
}

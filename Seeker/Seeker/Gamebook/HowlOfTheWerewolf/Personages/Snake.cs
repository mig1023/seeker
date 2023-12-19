using System.Collections.Generic;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class Snake
    {
        public static int Fight(ref Character protagonist, ref List<string> fight, int round)
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
    }
}

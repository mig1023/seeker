using System.Collections.Generic;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class Witch
    {
        public static void Fight(ref Character protagonist, ref List<string> fight)
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
                fight.Add($"BAD|Вы потеряли 2 Выносливости и " +
                    $"Трансформация продолжилась (Изменение достигло {protagonist.Change})");
            }
        }
    }
}

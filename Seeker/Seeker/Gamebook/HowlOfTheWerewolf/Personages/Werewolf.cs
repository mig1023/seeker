using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class Werewolf
    {
        public static bool DeadFight(ref Character protagonist, ref List<string> fight)
        {
            int werewolfAttack = Game.Dice.Roll();

            fight.Add($"Кубик атаки: {Game.Dice.Symbol(werewolfAttack)}");

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
    }
}

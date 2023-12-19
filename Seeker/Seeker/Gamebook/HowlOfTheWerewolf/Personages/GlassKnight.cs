using System.Collections.Generic;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class GlassKnight
    {
        public static bool Fight(ref List<string> fight)
        {
            if (!Game.Option.IsTriggered("Палица"))
                return false;

            int clubAttack = Game.Dice.Roll();

            fight.Add($"Удар палицы: {Game.Dice.Symbol(clubAttack)}");

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
    }
}

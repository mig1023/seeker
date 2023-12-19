using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class BlackWidow
    {
        public static int Fight(ref Character protagonist, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add($"Кубик атаки: {Game.Dice.Symbol(witchAttack)}");

            if (witchAttack < 3)
            {
                protagonist.Endurance -= 2;
                fight.Add("Удар когтями: вы потеряли 2 Выносливости");
            }
            else if (witchAttack == 3)
            {
                protagonist.Endurance -= 3;
                fight.Add("Сильный удар: вы потеряли 3 Выносливости " +
                    "и в следующий раунд не сможете атаковать пытаясь подняться на ноги");

                return 3;
            }
            else if (witchAttack == 4)
            {
                fight.Add("Плевок паутиной: вы не ранены, " +
                    "но следующий Раунд Атаки не можете защититься");

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

                fight.Add($"Стая пауков: вы теряете {Game.Dice.Symbol(spiders)}, " +
                    $"но и она теряет 2 Выносливости");

                return 6;
            }

            return 0;
        }
    }
}

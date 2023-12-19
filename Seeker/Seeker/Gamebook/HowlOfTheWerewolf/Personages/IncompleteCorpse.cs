using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class IncompleteCorpse
    {
        public static void Specificity(ref List<Character> fightEnemies, ref List<string> fight)
        {
            Character corpse = fightEnemies[0];

            int incomplete = Game.Dice.Roll();

            fight.Add($"Кубик вскрытия: {Game.Dice.Symbol(incomplete)}");

            if (incomplete < 3)
            {
                corpse.Mastery -= 1;

                fight.Add($"Доктор вырезал мозг: Мастерство мертвеца " +
                    $"снижается на единицу до {corpse.Mastery}");
            }
            else if (incomplete == 5)
            {
                corpse.Mastery -= 1;
                corpse.Endurance -= 1;

                fight.Add($"Доктор вырезал мозг и сердце: Мастерство " +
                    $"мертвеца снижается на единицу до {corpse.Mastery}," +
                    $"Выносливость мертвеца снижается на единицу " +
                    $"до {corpse.Endurance}");
            }
            else if (incomplete == 6)
            {
                fight.Add("Доктор вырезал кишечник: " +
                    "Никакого эффекта, мертвецу он уже не нужен");
            }
            else
            {
                corpse.Endurance -= 1;

                fight.Add($"Доктор вырезал сердце: Выносливость мертвеца " +
                    $"снижается на единицу до {corpse.Endurance}");
            }

            fight.Add(String.Empty);
        }
    }
}

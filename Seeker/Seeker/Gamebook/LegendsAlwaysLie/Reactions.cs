using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Reactions
    {
        public static bool Good(ref List<string> reaction)
        {
            int reactionLevel = (int)Math.Floor((double)Character.Protagonist.Hitpoints / 5);
            reaction.Add($"Уровнь реакции: {Character.Protagonist.Hitpoints} / 5 = {reactionLevel}");

            if (Game.Option.IsTriggered("EvilEye"))
            {
                reactionLevel -= 1;
                reaction.Add($"Из-за сглаза уровнь реакции снижается на единицу: {reactionLevel}");
            }

            int reactionDice = Game.Dice.Roll();
            bool goodReaction = reactionDice <= reactionLevel;
            string compare = goodReaction ? "<=" : ">";
            reaction.Add($"Реакция: {Game.Dice.Symbol(reactionDice)} {compare} {reactionLevel}");

            return goodReaction;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Reactions
    {
        public static bool Good(ref List<string> reaction, bool showResult = false)
        {
            int reactionLevel = (int)Math.Floor((double)Character.Protagonist.Hitpoints / 5);
            reaction.Add($"Уровнь реакции: {Character.Protagonist.Hitpoints} / 5 = {reactionLevel}");

            int reactionDice = Game.Dice.Roll();
            bool goodReaction = reactionDice <= reactionLevel;
            string reactionLine = goodReaction ? "<=" : ">";
            reaction.Add($"Реакция: {Game.Dice.Symbol(reactionDice)} {reactionLine} {reactionLevel}");

            if (showResult)
                reaction.Add(goodReaction ? "BOLD|Реакции хватило" : "BOLD|Реакция подвела");

            return goodReaction;
        }
    }
}

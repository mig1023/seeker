using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.RendezVous
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public int Dices { get; set; }

        public override List<string> Status() => new List<string> { String.Format("Осознание: {0}", protogonist.Awareness) };

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("ОСОЗНАНИЕ >") && (level >= protogonist.Awareness))
                            return false;

                        else if (oneOption.Contains("ОСОЗНАНИЕ <=") && (level < protogonist.Awareness))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string> { };

            int firstDice = Game.Dice.Roll();
            int dicesResult = firstDice;

            if (Dices == 1)
                diceCheck.Add(String.Format("На кубикe выпало: {0}", Game.Dice.Symbol(firstDice)));
            else
            {
                int secondDice = Game.Dice.Roll();
                dicesResult += secondDice;
                diceCheck.Add(String.Format("На кубиках выпало: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (firstDice + secondDice)));
            }

            diceCheck.Add(dicesResult % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }
    }
}

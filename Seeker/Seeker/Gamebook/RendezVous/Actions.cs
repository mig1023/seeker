using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.RendezVous
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public int Dices { get; set; }

        public override List<string> Status() =>
            new List<string> { $"Осознание: {protagonist.Awareness}" };

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("ОСОЗНАНИЕ >") && (level >= protagonist.Awareness))
                        {
                            return false;
                        }
                        else if (oneOption.Contains("ОСОЗНАНИЕ <=") && (level < protagonist.Awareness))
                        {
                            return false;
                        }
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
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
            {
                diceCheck.Add($"На кубикe выпало: {Game.Dice.Symbol(firstDice)}");
            }
            else
            {
                int secondDice = Game.Dice.Roll();
                dicesResult += secondDice;

                diceCheck.Add($"На кубиках выпало:" +
                    $"{Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} = " +
                    $"{firstDice + secondDice}");
            }

            diceCheck.Add(dicesResult % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }
    }
}

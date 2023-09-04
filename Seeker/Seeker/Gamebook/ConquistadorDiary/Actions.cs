using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string Round { get; set; }
        public int Bet { get; set; }

        public override List<string> Status()
        {
            List<string> score = new List<string>();

            if (protagonist.Points > 0)
                score.Add($"Очки интервью: {protagonist.Points}");

            if (protagonist.Score > 0)
                score.Add($"Баллы: {protagonist.Score}");

            return score;
        }

        public override List<string> Representer()
        {
            List<string> represent = new List<string>();

            if (Type == "Get-Decrease")
            {
                string diffLine = String.Empty;
                int bet = protagonist.CurrentBet;

                if ((bet >= 0) && (protagonist.Points == 0))
                {
                    diffLine = " всё, что осталось";
                }
                else if (bet > 0)
                {
                    string points = Game.Services.CoinsNoun(bet, "очко", "очка", "очков");
                    diffLine = $" {bet} {points}";
                }
                else
                {
                    diffLine = " ни одного очка";
                }

                represent.Add($"{Head}{diffLine}");
            }
            else if (Type == "CountScore")
            {
                represent.Add("Результат");
            }

            return represent;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            //if (Type == "CountScore")
            //{
            //    return protagonist.Round < Round;
            //}
            if (Type == "Get-Decrease")
            {
                if (secondButton)
                    return protagonist.CurrentBet > 1;
                else
                    return (protagonist.Points > 0) && (protagonist.CurrentBet < 6);
            }
            else
            {
                return true;
            }
        }

        public List<string> Get()
        {
            protagonist.CurrentBet += 1;
            protagonist.Points -= 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease()
        {
            protagonist.CurrentBet -= 1;
            protagonist.Points += 1;

            return new List<string> { "RELOAD" };
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(","))
            {
                bool isTriggered = option
                    .Split(',')
                    .Select(x => x.Trim().Trim('!'))
                    .Any(x => Game.Option.IsTriggered(x));

                bool negative = option.Contains("!");

                return negative ? !isTriggered : isTriggered;
            }
            else if (option.Contains(">"))
            {
                return protagonist.Score > Game.Services.LevelParse(option);
            }
            else if (option.Contains("<"))
            {
                return protagonist.Score < Game.Services.LevelParse(option);
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }

        public List<string> RollCoin()
        {
            bool coin = Game.Dice.Roll() % 2 == 0;

            if (coin)
                return new List<string> { "BIG|GOOD|На монетке выпал ОРЁЛ" };
            else
                return new List<string> { "BIG|BAD|На монетке выпала РЕШКА" };
        }

        public List<string> CountScore()
        {
            List<string> result = new List<string>();

            if (Round == "1")
            {
                if (protagonist.LastBet > 0)
                {
                    protagonist.Score += 1;

                    result.Add("BIG|GOOD|Курт победил этот раунд!");
                    result.Add("BIG|+1 балл");
                }
                else
                {
                    result.Add("BIG|BAD|Курт проиграл этот раунд...");
                    result.Add("BIG|1 балл получает Диего");
                }   
            }
            else if (Round == "2a")
            {
                if (protagonist.LastBet > 2)
                {
                    protagonist.Score += 2;

                    result.Add("BIG|GOOD|Курт победил этот раунд!");
                    result.Add("BIG|+2 балл");
                }
                else
                {
                    result.Add("BIG|BAD|Курт проиграл этот раунд...");
                    result.Add("BIG|2 балла получает Диего");
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public int Round { get; set; }
        public int Bet { get; set; }
        public int DiegoPoints { get; set; }

        public override List<string> Status()
        {
            if (Constants.WithStatuses.Contains(Game.Data.CurrentParagraphID))
            {
                return new List<string>
                {
                    $"Очки интервью: {Character.Protagonist.Points}",
                    $"Баллы: {Character.Protagonist.Score}"
                };
            }
            else
            {
                return null;
            }
        }

        public override List<string> Representer()
        {
            List<string> represent = new List<string>();

            if (Type == "Get-Decrease")
            {
                string diffLine = String.Empty;
                int bet = Character.Protagonist.CurrentBet;

                if ((bet >= 0) && (Character.Protagonist.Points == 0))
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
                represent.Add("Узнать итог раунда");
            }

            return represent;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Type == "CountScore")
            {
                return Character.Protagonist.Round < Round;
            }
            if (Type == "Get-Decrease")
            {
                if (secondButton)
                {
                    return Character.Protagonist.CurrentBet > 1;
                }
                else
                {
                    return (Character.Protagonist.Points > 0) && (Character.Protagonist.CurrentBet < 6);
                }
            }
            else
            {
                return true;
            }
        }

        public List<string> Get()
        {
            Character.Protagonist.CurrentBet += 1;
            Character.Protagonist.Points -= 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease()
        {
            Character.Protagonist.CurrentBet -= 1;
            Character.Protagonist.Points += 1;

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
                return Character.Protagonist.Score > Game.Services.LevelParse(option);
            }
            else if (option.Contains("<"))
            {
                return Character.Protagonist.Score < Game.Services.LevelParse(option);
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }

        public List<string> RollCoin()
        {
            bool coin = Game.Dice.Roll() % 2 == 0;
            string line = coin ? "GOOD|На монетке выпал ОРЁЛ" : "BAD|На монетке выпала РЕШКА";

            return new List<string> { $"BIG|{line}" };
        }

        public List<string> CountScore()
        {
            List<string> result = new List<string> { $"Раунд {Round}:"} ;

            if (DiegoPoints < 0)
            {
                DiegoPoints = Character.Protagonist.DiegoPoints;
                Character.Protagonist.DiegoPoints = 0;
            }

            if ((Round == 4) && (Character.Protagonist.Points > 0) && (Character.Protagonist.LastBet == 0))
            {
                Character.Protagonist.LastBet = Character.Protagonist.Points;
                Character.Protagonist.Points = 0;
            }

            result.Add($"Ставка Курта: {Character.Protagonist.LastBet} " +
                $"{Game.Services.CoinsNoun(Character.Protagonist.LastBet, "очко", "очка", "очков")}");

            result.Add($"Ставка Диего: {DiegoPoints} " +
                $"{Game.Services.CoinsNoun(DiegoPoints, "очко", "очка", "очков")}");

            if (Character.Protagonist.LastBet > DiegoPoints)
            {
                Character.Protagonist.Score += Round;
                result.Add("BIG|GOOD|Курт победил этот раунд!");
            }
            else
            {
                result.Add("BIG|BAD|Диего победил этот раунд...");
            }

            result.Add($"BIG|Он получил {Round} " +
                $"{Game.Services.CoinsNoun(Round, "балл", "балла", "баллов")}");

            Character.Protagonist.Round = Round;
            return result;
        }
    }
}

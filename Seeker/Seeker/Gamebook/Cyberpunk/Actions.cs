using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Cyberpunk
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public string Stat { get; set; }
        public bool MultipliedLuck { get; set; }
        public bool DividedLuck { get; set; }
        public bool ReverseCheck { get; set; }

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                $"Планирование: {protagonist.Planning}",
                $"Подготовка: {protagonist.Preparation}",
                $"Везение: {protagonist.Luck}",
            };

            if (protagonist.Cybernetics > 1)
                statusLines.Add($"Кибернетика: {protagonist.Cybernetics}");

            if (protagonist.Morality > 1)
                statusLines.Add($"Мораль: {protagonist.Morality}");

            if (protagonist.Careerism > 1)
                statusLines.Add($"Карьеризм: {protagonist.Careerism}");

            if (protagonist.BlackMarket > 1)
                statusLines.Add($"Чёрный рынок: {protagonist.BlackMarket}");

            if (protagonist.Clan > 1)
                statusLines.Add($"Клан: {protagonist.Clan}");

            return statusLines;
        }

        public override List<string> Representer()
        {
            if (Type == "Get, Decrease")
            {
                int statValue = GetProperty(protagonist, Stat);
                string statName = Constants.CharactersParams[Stat];

                return new List<string> { $"{statName.ToUpper()} (значение: {statValue})" };
            }
            else if ((Type == "DiceRoll") || (Type == "OddDiceRoll"))
            {
                return new List<string> { };
            }
            else if (String.IsNullOrEmpty(Head))
            {
                string line = "Проверка: ";

                foreach (string stat in Stat.Split(','))
                    line += $"{Constants.CharactersParams[stat.Trim()]} + ";

                return new List<string> { line.TrimEnd(' ', '+') };
            }
            else
            {
                return new List<string> { Head };
            }
        }

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
            else if (option == "BlackMarketDominate")
            {
                return (protagonist.BlackMarket > protagonist.Clan);
            }
            else if (option == "ClantDominate")
            {
                return (protagonist.BlackMarket < protagonist.Clan);
            }
            else if (option == "BlackMarketEquality")
            {
                return (protagonist.BlackMarket == protagonist.Clan);
            }
            else if (option.Contains(">") || option.Contains("<") || option.Contains("="))
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("ПЛАНИРОВАНИЕ <=") && (level < protagonist.Planning))
                    return false;

                else if (option.Contains("ПОДГОТОВКА <=") && (level < protagonist.Preparation))
                    return false;

                else if (option.Contains("ВЕЗЕНИЕ <=") && (level < protagonist.Luck))
                    return false;

                else if (option.Contains("МОРАЛЬ =") && (level != protagonist.Morality))
                    return false;

                else if (option.Contains("КАРЬЕРИЗМ =") && (level != protagonist.Careerism))
                    return false;

                return true;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        private static string GetParamLine(int level, string stat) =>
            $"{level} ({Constants.CharactersParams[stat].ToLower()}) +";

        public List<string> Test()
        {
            List<string> test = new List<string>();

            int paramsLevel = 0;
            string paramsLine = String.Empty;

            if (MultipliedLuck)
            {
                paramsLevel = protagonist.Luck * 2;
                paramsLine += $"{protagonist.Luck} (везение) x 2";
            }
            else if (DividedLuck)
            {
                paramsLevel = protagonist.Luck / 2;
                paramsLine += $"{protagonist.Luck} (везение) / 2";
            }
            else if (Stat.StartsWith("Selfcontrol"))
            {
                paramsLevel = int.Parse(Stat.Replace("Selfcontrol", String.Empty));
                paramsLine += GetParamLine(paramsLevel, Stat.Trim());

                Game.Option.Trigger(Stat, remove: true);
            }
            else if (!Stat.Contains(','))
            {
                paramsLevel = GetProperty(protagonist, Stat.Trim());
                paramsLine += GetParamLine(paramsLevel, Stat.Trim());
            }
            else
            {
                foreach (string stat in Stat.Split(','))
                {
                    int param = GetProperty(protagonist, stat.Trim());
                    paramsLevel += param;
                    paramsLine += GetParamLine(param, stat.Trim());
                }
            }

            if (Stat.Contains(','))
            {
                test.Add($"Параметры: {paramsLine.TrimEnd(' ', '+')} = {paramsLevel}");
            }
            else
            {
                test.Add($"Параметр: {paramsLine.TrimEnd(' ', '+')}");
            }

            int dice = Game.Dice.Roll(size: 100);
            bool success = ReverseCheck ? dice > paramsLevel : dice <= paramsLevel;

            test.Add($"BOLD|BIG|Бросок кубика: {dice} - {Game.Services.Сomparison(dice, paramsLevel)}!");
            test.Add(Result(success, "УСПЕХ|НЕУДАЧА"));

            return test;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if ((Type == "Get, Decrease") && (!String.IsNullOrEmpty(Stat)))
            {
                int stat = GetProperty(protagonist, Stat);
                bool cybernetics = (Stat == "Cybernetics");

                if (secondButton)
                    return stat > (cybernetics ? 1 : 0);
                else
                    return stat < (cybernetics ? 99 : 100);
            }
            else if ((Type == "Test") && Stat.StartsWith("Selfcontrol"))
            {
                return Game.Option.IsTriggered(Stat);
            }
            else
            {
                return true;
            }
        }

        public List<string> DiceRoll() =>
            new List<string> { $"BIG|Кубик: {Game.Dice.Roll(size: 100)}" };

        public List<string> OddDiceRoll()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll(size: 100);

            diceCheck.Add($"На кубикe выпало: {dice}");
            diceCheck.Add(dice % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }

        public List<string> Get() =>
            ChangeProtagonistParam(Stat, protagonist, String.Empty);

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, protagonist, String.Empty, decrease: true);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Cyberpunk
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string Stat { get; set; }
        public bool MultipliedLuck { get; set; }
        public bool DividedLuck { get; set; }

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Везение: {0}", protagonist.Luck),
                String.Format("Подготовка: {0}", protagonist.Preparation),
                String.Format("Планирование: {0}", protagonist.Planning),
            };

            if (protagonist.Cybernetics > 1)
                statusLines.Insert(0, String.Format("Кибернетика: {0}", protagonist.Cybernetics));

            if (protagonist.Morality > 1)
                statusLines.Insert(0, String.Format("Мораль: {0}", protagonist.Morality));

            if (protagonist.Careerism > 1)
                statusLines.Insert(0, String.Format("Карьеризм: {0}", protagonist.Careerism));

            if (protagonist.BlackMarket > 1)
                statusLines.Insert(0, String.Format("Чёрный рынок: {0}", protagonist.BlackMarket));

            if (protagonist.Clan > 1)
                statusLines.Insert(0, String.Format("Клан: {0}", protagonist.Clan));

            return statusLines;
        }

        public override List<string> Representer()
        {
            if (Type == "Get, Decrease")
            {
                int statValue = GetProperty(protagonist, Stat);
                string statName = Constants.CharactersParams()[Stat];

                return new List<string> { String.Format("{0} (значение: {1})", statName.ToUpper(), statValue) };
            }
            else if ((Type == "DiceRoll") || (Type == "OddDiceRoll"))
            {
                return new List<string> { };
            }
            else if (String.IsNullOrEmpty(Text))
            {
                string line = "Проверка: ";

                foreach (string stat in Stat.Split(','))
                    line += String.Format("{0} + ", Constants.CharactersParams()[stat.Trim()]);

                return new List<string> { line.TrimEnd(' ', '+') };
            }
            else
            {
                return new List<string> { Text };
            }
        }

        public override bool CheckOnlyIf(string option)
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
                return CheckOnlyIfTrigger(option);
            }
        }

        public List<string> Test()
        {
            List<string> test = new List<string>();

            int paramsLevel = 0;
            string paramsLine = "Параметры: ";

            if (MultipliedLuck)
            {
                paramsLevel = protagonist.Luck * 2;
                paramsLine += String.Format("{0} (везение) x 2", protagonist.Luck);
            }
            else if (DividedLuck)
            {
                paramsLevel = protagonist.Luck / 2;
                paramsLine += String.Format("{0} (везение) / 2", protagonist.Luck);
            }
            else if (Stat.StartsWith("Selfcontrol"))
            {
                paramsLevel = int.Parse(Stat.Replace("Selfcontrol", String.Empty));
                paramsLine += String.Format("{0} ({1}) + ",
                    paramsLevel, Constants.CharactersParams()[Stat.Trim()].ToLower());

                Game.Option.Trigger(Stat, remove: true);
            }
            else
            {
                foreach (string stat in Stat.Split(','))
                {
                    int param = GetProperty(protagonist, stat.Trim());
                    paramsLevel += param;
                    paramsLine += String.Format("{0} ({1}) + ",
                        param, Constants.CharactersParams()[stat.Trim()].ToLower());
                }
            }

            test.Add(String.Format("{0} = {1}", paramsLine.TrimEnd(' ', '+'), paramsLevel));

            int dice = Game.Dice.Roll(size: 100);

            test.Add(String.Format("BOLD|BIG|Бросок кубика: {0} - {1}!", dice, Game.Services.Сomparison(dice, paramsLevel)));
            test.Add(Result((dice <= paramsLevel), "УСПЕХ|НЕУДАЧА"));

            return test;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if ((Type == "Get, Decrease") && (!String.IsNullOrEmpty(Stat)))
            {
                int stat = GetProperty(protagonist, Stat);
                bool cybernetics = (Stat == "Cybernetics");

                if (secondButton)
                    return (stat > (cybernetics ? 1 : 0));
                else
                    return (stat < (cybernetics ? 99 : 100));
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

        public List<string> DiceRoll() => new List<string> { String.Format("BIG|Кубик: {0}", Game.Dice.Roll(size: 100)) };

        public List<string> OddDiceRoll()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll(size: 100);

            diceCheck.Add(String.Format("На кубикe выпало: {0}", dice));
            diceCheck.Add(dice % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }

        public List<string> Get() => ChangeProtagonistParam(Stat, protagonist, String.Empty);

        public List<string> Decrease() => ChangeProtagonistParam(Stat, protagonist, String.Empty, decrease: true);
    }
}

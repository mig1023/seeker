using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Ants
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                $"Количество: {protagonist.Quantity}",
                $"Прирост: {protagonist.Increase}"
            };

            if (protagonist.Defence > 0)
                statusLines.Add($"Защита: {protagonist.Defence}");

            if (protagonist.EnemyHitpoints > 0)
                statusLines.Add($"{protagonist.EnemyName}: {protagonist.EnemyHitpoints}");

            return statusLines;
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
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<") || oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("ДАЙС =") && !protagonist.Dice[level])
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО >=") && (level > protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО <") && (level <= protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("ВРАГ >=") && (level > protagonist.EnemyHitpoints))
                            return false;

                        if (oneOption.Contains("ВРАГ <") && (level <= protagonist.EnemyHitpoints))
                            return false;

                        if (oneOption.Contains("ЗАЩИТА >=") && (level > protagonist.Defence))
                            return false;

                        if (oneOption.Contains("СТАРТ =") && (protagonist.Start != level))
                            return false;
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

        public List<string> Result()
        {
            List<string> results = new List<string>();

            bool queen = Game.Option.IsTriggered("Королева Антуанетта Плодовитая");
            bool prince = Game.Option.IsTriggered("Принц Мурадин Крылатый");
            bool soldier = Game.Option.IsTriggered("Солдат Руф Твердожвалый");

            List<string> ending = queen || prince || soldier ? Constants.EndingOne : Constants.EndingTwo;

            foreach (string endingLine in Constants.EndingOne)
                results.Add(endingLine.Replace(';', ','));

            results.Add(String.Empty);

            int speed = 300 - protagonist.Time;

            string line = String.Empty;

            foreach (KeyValuePair<string, int> timelist in Constants.Rating.OrderBy(x => x.Value))
            {
                if (speed < timelist.Value)
                {
                    break;
                }
                else
                {
                    line = timelist.Key;
                }
            }

            List<string> resultLines = line.Split('!').ToList();

            results.Add($"{resultLines[0]}!");
            results.Add($"BIG|BOLD|{resultLines[1].Trim()}");

            return results;
        }

        public List<string> Changes()
        {
            List<string> changes = new List<string>();

            foreach (string head in Constants.Government.Keys)
            {
                if (Game.Option.IsTriggered(head))
                    changes.Add(Constants.Government[head]);
            }

            return changes;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Ants
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                $"Количество: {Character.Protagonist.Quantity}",
                $"Прирост: {Character.Protagonist.Increase}"
            };

            string government = String.Empty;

            foreach (string name in Constants.Government.Keys)
            {
                if (Game.Option.IsTriggered(name))
                    government = name;
            }

            if (!String.IsNullOrEmpty(government))
                statusLines.Insert(0, $"Правит: {government}");

            if (Character.Protagonist.Defence > 0)
                statusLines.Add($"Защита: {Character.Protagonist.Defence}");

            if (Character.Protagonist.EnemyHitpoints > 0)
                statusLines.Add($"{Character.Protagonist.EnemyName}: {Character.Protagonist.EnemyHitpoints}");

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

                        if (oneOption.Contains("ДАЙС =") && !Character.Protagonist.Dice[level])
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО >=") && (level > Character.Protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО <") && (level <= Character.Protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("ВРАГ >=") && (level > Character.Protagonist.EnemyHitpoints))
                            return false;

                        if (oneOption.Contains("ВРАГ <") && (level <= Character.Protagonist.EnemyHitpoints))
                            return false;

                        if (oneOption.Contains("ЗАЩИТА >=") && (level > Character.Protagonist.Defence))
                            return false;

                        if (oneOption.Contains("СТАРТ =") && (Character.Protagonist.Start != level))
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

            int speed = 300 - Character.Protagonist.Time;

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

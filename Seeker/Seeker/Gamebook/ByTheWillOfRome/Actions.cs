﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status()
        {
            if (Game.Data.CurrentParagraphID < Constants.AddonStartParagraph)
            {
                return new List<string>
                {
                    $"Сестерциев: {Character.Protagonist.Sestertius}",
                    $"Честь: {Character.Protagonist.Honor}",
                };
            }
            else
            {
                return null;
            }
        }           

        private string Squad(string symbol, int size)
        {
            string legionaries = new string('x', size).Replace("x", symbol);
            string squad = String.IsNullOrEmpty(legionaries) ? "ни одного" : legionaries;

            return squad;
        }

        public override List<string> AdditionalStatus()
        {
            if (Character.Protagonist.Legionaries > 0)
            {
                string legioner = Character.Protagonist.Discipline >= 0 ? "🙂" : "😡";

                return new List<string>
                {
                    $"Легионеров: {Squad(legioner, Character.Protagonist.Legionaries)}",
                    $"Дисциплина: {Game.Services.NegativeMeaning(Character.Protagonist.Discipline)}",
                };
            }
            else if (Character.Protagonist.Horsemen > 0)
            {
                return new List<string>
                {
                    $"Всадников: {Squad("🐎", Character.Protagonist.Horsemen)}",
                    $"Навыки рукопашного боя: 2",
                };
            }
            else
            {
                return null;
            }
        }

        public override List<string> Representer()
        {
            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "сестерций", "сестерция", "сестерциев");
                return new List<string> { $"{Head}\n{Price} {gold}" };
            }
            else
            {
                return new List<string> { };
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Ущерб чести слишком велик, лучше броситься на меч, а игру начать сначала";

            return Character.Protagonist.Honor <= 0;
        }

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !(Used || ((Price > 0) && (Character.Protagonist.Sestertius < Price)));

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                List<string> options = option
                    .Split('|')
                    .Select(x => x.Trim())
                    .ToList();

                foreach (string optionsPart in options)
                {
                    if (Game.Option.IsTriggered(optionsPart))
                        return true;
                }

                return false;
            }
            else
            {
                List<string> options = option
                    .Split(',')
                    .ToList();

                foreach (string oneOption in options)
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("СЕСТЕРЦИЕВ >=") && (level <= Character.Protagonist.Sestertius))
                            return true;

                        if (oneOption.Contains("ДИСЦИПЛИНА >=") && (level <= Character.Protagonist.Discipline))
                            return true;
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

        public List<string> Get()
        {
            Character.Protagonist.Sestertius -= Price;

            Used = true;

            return new List<string> { "RELOAD" };
        }
    }
}
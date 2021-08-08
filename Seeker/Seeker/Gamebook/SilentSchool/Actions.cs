﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SilentSchool
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public int HarmedMyself { get; set; }
        public int Dices { get; set; }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string> { String.Format("Жизнь: {0}", protagonist.Life) };

            if (protagonist.Grail > 0)
                statusLines.Add(String.Format("Грааль: {0}", protagonist.Grail));

            if (!String.IsNullOrEmpty(protagonist.Weapon))
                statusLines.Add(String.Format("Оружие: {0}", protagonist.Weapon));

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Triggers.Contains("Шоколадка"))
                staticButtons.Add("Съесть шоколадку");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            Game.Option.Trigger("Шоколадка", remove: true);

            protagonist.Life += 3;

            return true;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled() =>
            !((HarmedMyself > 0) && ((protagonist.HarmSelfAlready > 0) || (protagonist.Life <= HarmedMyself)));

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                bool not = options[0].Contains("!");
                int optionMustBe = int.Parse(options[0].Replace("!", String.Empty));
                int optionCount = options.Where(x => Game.Data.Triggers.Contains(x.Trim())).Count();

                if (not)
                    return optionCount < optionMustBe;
                else
                    return optionCount >= optionMustBe;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(option);

                        if (oneOption.Contains("ГРААЛЬ >=") && (level > protagonist.Grail))
                            return false;

                        if (oneOption.Contains("РАНА >=") && (level > protagonist.HarmSelfAlready))
                            return false;

                        if (oneOption.Contains("РАНА <") && (level <= protagonist.HarmSelfAlready))
                            return false;
                    }
                    else if (oneOption.Contains("ОРУЖИЕ"))
                    {
                        string value = oneOption.Split('=')[1].Trim();

                        if (oneOption.Contains("!") && (value == protagonist.Weapon))
                            return false;

                        else if (!oneOption.Contains("!") && (value != protagonist.Weapon))
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

        public override List<string> Representer() => String.IsNullOrEmpty(Text) ? new List<string> { } : new List<string> { Text.ToUpper() };

        public List<string> Get()
        {
            if (HarmedMyself > 0)
            {
                protagonist.Life -= HarmedMyself;
                protagonist.HarmSelfAlready = HarmedMyself;
            }
            else
                protagonist.Weapon = Text;

            return new List<string> { "RELOAD" };
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;

            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            protagonist.Life -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Я потерял жизней: {0}", Game.Dice.Symbol(dices)));

            return diceCheck;
        }
    }
}

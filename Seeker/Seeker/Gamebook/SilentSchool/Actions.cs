using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SilentSchool
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public int HarmedMyself { get; set; }
        public int Dices { get; set; }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string> { $"Жизнь: {Character.Protagonist.Life}" };

            if (Character.Protagonist.Grail > 0)
                statusLines.Add($"Грааль: {Character.Protagonist.Grail}");

            if (!String.IsNullOrEmpty(Character.Protagonist.Weapon))
                statusLines.Add($"Оружие: {Character.Protagonist.Weapon}");

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Option.IsTriggered("Шоколадка"))
                staticButtons.Add("Съесть шоколадку");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            Game.Option.Trigger("Шоколадка", remove: true);

            Character.Protagonist.Life += 3;

            return true;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !((HarmedMyself > 0) && ((Character.Protagonist.HarmSelfAlready > 0) || (Character.Protagonist.Life <= HarmedMyself)));

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
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                bool not = options[0].Contains("!");
                int optionMustBe = int.Parse(options[0].Replace("!", String.Empty));
                int optionCount = options.Where(x => Game.Option.IsTriggered(x.Trim())).Count();

                if (not)
                {
                    return optionCount < optionMustBe;
                }
                else
                {
                    return optionCount >= optionMustBe;
                }
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(option);

                        if (oneOption.Contains("ГРААЛЬ >=") && (level > Character.Protagonist.Grail))
                            return false;

                        if (oneOption.Contains("РАНА >=") && (level > Character.Protagonist.HarmSelfAlready))
                            return false;

                        if (oneOption.Contains("РАНА <") && (level <= Character.Protagonist.HarmSelfAlready))
                            return false;
                    }
                    else if (oneOption.Contains("ОРУЖИЕ"))
                    {
                        string value = oneOption.Split('=')[1].Trim();

                        if (oneOption.Contains("!") && (value == Character.Protagonist.Weapon))
                        {
                            return false;
                        }
                        else if (!oneOption.Contains("!") && (value != Character.Protagonist.Weapon))
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

        public override List<string> Representer() =>
            String.IsNullOrEmpty(Head) ? new List<string> { } : new List<string> { Head.ToUpper() };

        public List<string> Get()
        {
            if (HarmedMyself > 0)
            {
                Character.Protagonist.Life -= HarmedMyself;
                Character.Protagonist.HarmSelfAlready = HarmedMyself;
            }
            else
            {
                Character.Protagonist.Weapon = Head;
            }

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
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            Character.Protagonist.Life -= dices;

            diceCheck.Add($"BIG|BAD|Я потерял жизней: {dices}");

            return diceCheck;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Stat { get; set; }
        public int StatStep { get; set; }
        public int Level { get; set; }
        public bool GreatKhanSpecialCheck { get; set; }
        public bool GuessBonus { get; set; }
        public bool TablesGame { get; set; }

        static bool NextTestWithKumis = false;

        public override List<string> Representer()
        {
            if (Level > 0)
            {
                return new List<string> {
                    $"Проверка {Constants.StatNames[Stat]}, уровень {Level}" };
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = GetProperty(Character.Protagonist, Stat);
                string diffLine = currentStat > 1 ? $" (+{currentStat - 1})" : String.Empty;

                return new List<string> { $"{Head}{diffLine}" };
            }
            else if (Price > 0)
            {
                return new List<string> { $"{Head}, {Price} таньга" };
            }
            else if (!String.IsNullOrEmpty(Head))
            {
                return new List<string> { Head };
            }
            else
            {
                return new List<string> { };
            }
        }

        public override List<string> Status() => new List<string>
        {
            $"Деньги: {Character.Protagonist.Tanga}",
            $"Кумыс: {Character.Protagonist.Kumis}",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            statusLines.Add($"Популярность: {Character.Protagonist.Popularity}");

            if (Character.Protagonist.AkynGlory != null)
                statusLines.Add($"Слава акына: {Character.Protagonist.AkynGlory}");

            if (Character.Protagonist.UnitOfTime != null)
                statusLines.Add($"Ед.времени: {Character.Protagonist.UnitOfTime}");

            if (Character.Protagonist.Strength > 1)
                statusLines.Add($"Сила: {Character.Protagonist.Strength}");

            if (Character.Protagonist.Skill > 1)
                statusLines.Add($"Ловкость: {Character.Protagonist.Skill}");

            if (Character.Protagonist.Wisdom > 1)
                statusLines.Add($"Мудрость: {Character.Protagonist.Wisdom}");

            if (Character.Protagonist.Cunning > 1)
                statusLines.Add($"Хитрость: {Character.Protagonist.Cunning}");

            if (Character.Protagonist.Oratory > 1)
                statusLines.Add($"Красноречие: {Character.Protagonist.Oratory}");

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            bool withoutStaticButtons = Game.Data.Constants
                .GetParagraphsWithoutStaticsButtons()
                .Contains(Game.Data.CurrentParagraphID);

            if (withoutStaticButtons)
                return staticButtons;

            bool testInParagraph = Game.Buttons.ExistsInParagraph(actionName: "TEST");

            if (testInParagraph && (Character.Protagonist.Kumis > 0) && !NextTestWithKumis)
                staticButtons.Add("ВЫПИТЬ КУМЫСА");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ВЫПИТЬ КУМЫСА")
            {
                Character.Protagonist.Kumis -= 1;
                NextTestWithKumis = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 150;
            toEndText = "Задуматься о судьбе";

            return Character.Protagonist.Popularity <= 0;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Type == "SellHorse")
            {
                return Game.Option.IsTriggered("ArabianHorse");
            }
            else if (Level > 0)
            {
                return true;
            }
            else if ((Price <= 0) && secondButton)
            {
                return GetProperty(Character.Protagonist, Stat) > 1;
            }
            else if ((Price <= 0) && !secondButton)
            {
                return Character.Protagonist.StatBonuses > 0;
            }
            else if (Used)
            {
                return false;
            }
            else
            {
                return Character.Protagonist.Tanga >= Price;
            }
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("ТАНЬГА >=") && (level > Character.Protagonist.Tanga))
                            return false;

                        else if (oneOption.Contains("СЛАВА_АКЫНА >=") && (level > Character.Protagonist.AkynGlory))
                            return false;

                        else if (oneOption.Contains("ПОПУЛЯРНОСТЬ >") && (level >= Character.Protagonist.Popularity))
                            return false;

                        else if (oneOption.Contains("ЕДИНИЦЫ_ВРЕМЕНИ >") && (level >= Character.Protagonist.UnitOfTime))
                            return false;

                        else if (oneOption.Contains("ЕДИНИЦЫ_ВРЕМЕНИ <=") && (level < Character.Protagonist.UnitOfTime))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public List<string> Test()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            if (GuessBonus && Game.Option.IsTriggered("guess"))
                Level = 7;

            if (GreatKhanSpecialCheck)
                Level -= (Character.Protagonist.Popularity + (Game.Option.IsTriggered("KhansRing") ? 3 : 0));

            if (NextTestWithKumis)
                Level -= 2;

            int currentStat = GetProperty(Character.Protagonist, Stat);
            bool testIsOk = (firstDice + secondDice) + currentStat >= Level;

            List<string> testLines = new List<string>();

            if (TablesGame && Game.Option.IsTriggered("Tables"))
            {
                testLines.Add("Алдар владеет игрой в нарды в совершенстве!");
                testIsOk = true;
            }
            else
            {
                string okLine = testIsOk ? ">=" : "<";

                testLines.Add($"Проверка {Constants.StatNames[Stat]}: {Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} + {currentStat} {okLine} {Level}");

                if (GuessBonus && Game.Option.IsTriggered("guess"))
                    testLines.Insert(0, "Бонус за догадку: -4");

                if (GreatKhanSpecialCheck)
                {
                    if (Game.Option.IsTriggered("KhansRing"))
                        testLines.Insert(0, "Бонус за ханское кольцо: -3");

                    if (Character.Protagonist.Popularity > 0)
                        testLines.Insert(0, $"Бонус за популярность: -{Character.Protagonist.Popularity}");
                }

                if (NextTestWithKumis)
                    testLines.Insert(0, "Бонус за кумыс: -2");
            }

            testLines.Add(Result(testIsOk, "АЛДАР СПРАВИЛСЯ", "АЛДАР НЕ СПРАВИЛСЯ"));

            NextTestWithKumis = false;

            Game.Buttons.Disable(testIsOk, "В случае успеха", "В случае провала");

            return testLines;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Tanga >= Price))
            {
                Character.Protagonist.Tanga -= Price;

                Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            else if(!String.IsNullOrEmpty(Stat))
                ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses");

            return new List<string> { "RELOAD" };
        }

        public List<string> SellHorse()
        {
            Character.Protagonist.Tanga += 20;
            Game.Option.Trigger("ArabianHorse", remove: true);

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);
    }
}

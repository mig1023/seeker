using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

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
                return new List<string> { String.Format("Проверка {0}, уровень {1}", Constants.StatNames[Stat], Level) };

            else if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = GetProperty(protagonist, Stat);
                string diffLine = (currentStat > 1 ? String.Format(" (+{0})", (currentStat - 1)) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Head, diffLine) };
            }
            else if (Price > 0)
                return new List<string> { String.Format("{0}, {1} таньга", Head, Price) };

            else if (!String.IsNullOrEmpty(Head))
                return new List<string> { Head };

            else
                return new List<string> { };
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Деньги: {0}", protagonist.Tanga),
            String.Format("Кумыс: {0}", protagonist.Kumis),
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            statusLines.Add(String.Format("Популярность: {0}", protagonist.Popularity));

            if (protagonist.AkynGlory != null)
                statusLines.Add(String.Format("Слава акына: {0}", protagonist.AkynGlory));

            if (protagonist.UnitOfTime != null)
                statusLines.Add(String.Format("Ед.времени: {0}", protagonist.UnitOfTime));

            if (protagonist.Strength > 1)
                statusLines.Add(String.Format("Сила: {0}", protagonist.Strength));

            if (protagonist.Skill > 1)
                statusLines.Add(String.Format("Ловкость: {0}", protagonist.Skill));

            if (protagonist.Wisdom > 1)
                statusLines.Add(String.Format("Мудрость: {0}", protagonist.Wisdom));

            if (protagonist.Cunning > 1)
                statusLines.Add(String.Format("Хитрость: {0}", protagonist.Cunning));

            if (protagonist.Oratory > 1)
                statusLines.Add(String.Format("Красноречие: {0}", protagonist.Oratory));

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if (Game.Checks.ExistsInParagraph(actionName: "TEST") && (protagonist.Kumis > 0) && !NextTestWithKumis)
                staticButtons.Add("ВЫПИТЬ КУМЫСА");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ВЫПИТЬ КУМЫСА")
            {
                protagonist.Kumis -= 1;
                NextTestWithKumis = true;
                return true;
            }
            else
                return false;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 150;
            toEndText = "Задуматься о судьбе";

            return protagonist.Popularity <= 0;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Type == "SellHorse")
                return Game.Option.IsTriggered("ArabianHorse");

            else if (Level > 0)
                return true;

            else if ((Price <= 0) && secondButton)
                return GetProperty(protagonist, Stat) > 1;

            else if ((Price <= 0) && !secondButton)
                return protagonist.StatBonuses > 0;

            else if (Used)
                return false;

            else
                return protagonist.Tanga >= Price;
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

                        if (oneOption.Contains("ТАНЬГА >=") && (level > protagonist.Tanga))
                            return false;

                        else if (oneOption.Contains("СЛАВА_АКЫНА >=") && (level > protagonist.AkynGlory))
                            return false;

                        else if (oneOption.Contains("ПОПУЛЯРНОСТЬ >") && (level >= protagonist.Popularity))
                            return false;

                        else if (oneOption.Contains("ЕДИНИЦЫ_ВРЕМЕНИ >") && (level >= protagonist.UnitOfTime))
                            return false;

                        else if (oneOption.Contains("ЕДИНИЦЫ_ВРЕМЕНИ <=") && (level < protagonist.UnitOfTime))
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
                Level -= (protagonist.Popularity + (Game.Option.IsTriggered("KhansRing") ? 3 : 0));

            if (NextTestWithKumis)
                Level -= 2;

            int currentStat = GetProperty(protagonist, Stat);
            bool testIsOk = (firstDice + secondDice) + currentStat >= Level;

            List<string> testLines = new List<string>();

            if (TablesGame && Game.Option.IsTriggered("Tables"))
            {
                testLines.Add("Алдар владеет игрой в нарды в совершенстве!");
                testIsOk = true;
            }
            else
            {
                testLines.Add(String.Format(
                    "Проверка {0}: {1} + {2} + {3} {4} {5}",
                    Constants.StatNames[Stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice),
                    currentStat, (testIsOk ? ">=" : "<"), Level));

                if (GuessBonus && Game.Option.IsTriggered("guess"))
                    testLines.Insert(0, "Бонус за догадку: -4");

                if (GreatKhanSpecialCheck)
                {
                    if (Game.Option.IsTriggered("KhansRing"))
                        testLines.Insert(0, "Бонус за ханское кольцо: -3");

                    if (protagonist.Popularity > 0)
                        testLines.Insert(0, String.Format("Бонус за популярность: -{0}", protagonist.Popularity));
                }

                if (NextTestWithKumis)
                    testLines.Insert(0, "Бонус за кумыс: -2");
            }

            testLines.Add(Result(testIsOk, "АЛДАР СПРАВИЛСЯ|АЛДАР НЕ СПРАВИЛСЯ"));

            NextTestWithKumis = false;

            return testLines;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (protagonist.Tanga >= Price))
            {
                protagonist.Tanga -= Price;

                Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            else if(!String.IsNullOrEmpty(Stat))
                ChangeProtagonistParam(Stat, protagonist, "StatBonuses");

            return new List<string> { "RELOAD" };
        }

        public List<string> SellHorse()
        {
            protagonist.Tanga += 20;
            Game.Option.Trigger("ArabianHorse", remove: true);

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, protagonist, "StatBonuses", decrease: true);
    }
}

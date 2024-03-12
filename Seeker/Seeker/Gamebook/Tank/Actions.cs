using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Tank
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Crew { get; set; }

        public override List<string> Status()
        {
            return new List<string> { $"Состояние танка: исправен" };
        }

        private void CrewNames(string name, out string nominative, out string accusative)
        {
            string crewName = Constants.CrewNames[name];
            List<string> crew = crewName.ToUpper().Split('|').ToList();
            accusative = crew[0];
            nominative = crew[1];
        }

        public override List<string> Representer()
        {
            CrewNames(Crew, out string nominative, out string accusative);
            string line = String.Empty;

            if (Type == "Test")
            {
                line = $"ТЕСТ {accusative}";
            }
            else
            {
                int currentStat = GetProperty(Character.Protagonist, Crew);
                string points = Game.Services.CoinsNoun(currentStat, "очко", "очка", "очков");
                line = $"{nominative}" + (currentStat > 0 ? $" (опыт {currentStat} {points})" : String.Empty);
            }

            return new List<string> { line };
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (String.IsNullOrEmpty(Crew) || (Type != "Get-Decrease"))
                return true;

            int stat = GetProperty(Character.Protagonist, Crew);

            if (secondButton)
                return (stat > 0);
            else
                return (Character.Protagonist.StatBonuses > 0);
        }

        public List<string> Get() =>
            ChangeProtagonistParam(Crew, Character.Protagonist, "StatBonuses");

        public List<string> Decrease() =>
            ChangeProtagonistParam(Crew, Character.Protagonist, "StatBonuses", decrease: true);

        public List<string> Test()
        {
            List<string> testLines = new List<string>();

            CrewNames(Crew, out string nominative, out string accusative);
            testLines.Add($"ТЕСТ НА ОПЫТ {nominative}:");

            int experience = GetProperty(Character.Protagonist, Crew);
            string points = Game.Services.CoinsNoun(experience, "очко", "очка", "очков");
            testLines.Add($"Опыт: {experience} {points}");

            int dice = Game.Dice.Roll();
            bool testIsOk = dice <= experience;
            string diffLine = testIsOk ? "<=" : ">";
            testLines.Add($"Проверка: {Game.Dice.Symbol(dice)} {diffLine} {experience}");

            testLines.Add(Result(testIsOk, $"BOLD|{accusative} СПРАВИЛСЯ", $"BOLD|{accusative} НЕ СПРАВИЛСЯ"));

            //Game.Buttons.Disable(testIsOk, "В случае успеха, Обе проверки успешны", "В случае провала");

            return testLines;
        }
    }
}

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

        private void CrewNames(string name, out string first, out string second)
        {
            string crewName = Constants.CrewNames[name];
            List<string> crew = crewName.ToUpper().Split('|').ToList();
            first = crew[0];
            second = crew[1];
        }

        public override List<string> Representer()
        {
            CrewNames(Constants.CrewNames[Crew], out string _, out string crew);
            int currentStat = GetProperty(Character.Protagonist, Crew);
            string points = Game.Services.CoinsNoun(currentStat, "очко", "очка", "очков");
            string line = currentStat > 0 ? $"ТЕСТ {crew} (опыт {currentStat} {points})" : String.Empty;

            return new List<string> { $"{Head}{line}" };
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (String.IsNullOrEmpty(Crew))
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

            CrewNames(Constants.CrewNames[Crew], out string crew, out string crewResult);
            testLines.Add($"ТЕСТ {crew}:");

            int experience = GetProperty(Character.Protagonist, Crew);
            string points = Game.Services.CoinsNoun(experience, "очко", "очка", "очков");
            testLines.Add($"Опыт: {experience} {points}");

            int dice = Game.Dice.Roll();
            bool testIsOk = dice <= experience;
            string diffLine = testIsOk ? "<=" : ">";
            testLines.Add($"Проверка: {Game.Dice.Symbol(dice)} {diffLine} {experience}");

            testLines.Add(Result(testIsOk, $"BOLD|{crewResult} СПРАВИЛСЯ", $"BOLD|{crewResult} НЕ СПРАВИЛСЯ"));

            //Game.Buttons.Disable(testIsOk, "В случае успеха, Обе проверки успешны", "В случае провала");

            return testLines;
        }
    }
}

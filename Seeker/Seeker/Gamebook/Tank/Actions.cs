using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Tank
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Stat { get; set; }

        public override List<string> Status()
        {
            return new List<string> { $"Состояние танка: исправен" };
        }

        public override List<string> Representer()
        {
            int currentStat = GetProperty(Character.Protagonist, Stat);
            string points = Game.Services.CoinsNoun(currentStat, "очко", "очка", "очков");
            string line = currentStat > 0 ? $" (опыт {currentStat} {points})" : String.Empty;

            return new List<string> { $"{Head}{line}" };
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            int stat = GetProperty(Character.Protagonist, Stat);

            if (secondButton)
                return (stat > 0);
            else
                return (Character.Protagonist.StatBonuses > 0);
        }

        public List<string> Get() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses");

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);
    }
}

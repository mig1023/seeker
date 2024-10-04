using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.SeasOfBlood
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() => new List<string>
        {
            $"Сила команды: {Character.Protagonist.TeamStrength}",
            $"Численность: {Character.Protagonist.TeamSize}/{Character.Protagonist.MaxTeamSize}",
            $"Судовой журнал: {Character.Protagonist.Logbook}/50",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Mastery}",
            $"Выносливость: {Character.Protagonist.Endurance}/{Character.Protagonist.MaxEndurance}",
            $"Удачливость: {Character.Protagonist.Luck}/{Character.Protagonist.MaxLuck}",
            $"Золото: {Character.Protagonist.Coins}",
            $"Добыча: {Character.Protagonist.Spoils}",
        };
    }
}

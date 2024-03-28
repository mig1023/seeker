using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Insight
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Bonus { get; set; }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Bonus))
            {
                int diff = GetProperty(Character.Protagonist, Bonus) - Constants.GetStartValues[Bonus];
                string diffLine = diff > 0 ? $" (+{diff})" : String.Empty;

                return new List<string> { $"{Head}{diffLine}" };
            }

            return new List<string>();
        }

        public override List<string> Status() => new List<string>
        {
            $"Время: {Character.Protagonist.Time} / 30",
            $"Ячейки памяти: {Character.Protagonist.Memory} / 8",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Здоровье: {Character.Protagonist.Life}",
            $"Аура: {Character.Protagonist.Aura}",
            $"Ловкость: {Character.Protagonist.Skill}",
            $"Меткость: {Character.Protagonist.Weapon}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledByBonusesRemove = !String.IsNullOrEmpty(Bonus) &&
                ((GetProperty(Character.Protagonist, Bonus) - Constants.GetStartValues[Bonus]) <= 0) &&
                secondButton;

            bool disabledByBonusesAdd = (!String.IsNullOrEmpty(Bonus)) &&
                (Character.Protagonist.Bonuses <= 0) && !secondButton;

            return !(disabledByBonusesRemove || disabledByBonusesAdd);
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
                    if (oneOption.Contains(">") || oneOption.Contains("<") || oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(option);

                        if (oneOption.Contains("АУРА >") && (level >= Character.Protagonist.Aura))
                            return false;

                        if (oneOption.Contains("АУРА <") && (level <= Character.Protagonist.Aura))
                            return false;

                        if (oneOption.Contains("ЛОВКОСТЬ >") && (level >= Character.Protagonist.Skill))
                            return false;

                        if (oneOption.Contains("ЛОВКОСТЬ <") && (level <= Character.Protagonist.Skill))
                            return false;

                        if (oneOption.Contains("МЕТКОСТЬ >") && (level >= Character.Protagonist.Weapon))
                            return false;

                        if (oneOption.Contains("МЕТКОСТЬ =") && (level != Character.Protagonist.Weapon))
                            return false;

                        if (oneOption.Contains("МЕТКОСТЬ <") && (level >= Character.Protagonist.Weapon))
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

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Bonus) && (Character.Protagonist.Bonuses >= 0))
                ChangeProtagonistParam(Bonus, Character.Protagonist, "Bonuses");

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Bonus, Character.Protagonist, "Bonuses", decrease: true);

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Life < 22;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Life += healingLevel;
    }
}

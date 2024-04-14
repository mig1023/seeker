using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.PrisonerOfMoritaiCastle
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() => new List<string>
        {
            $"Жизненные силы: {Character.Protagonist.Hitpoints}",
            $"Стрелы: {Character.Protagonist.Arrows}",
            $"Сюрикены: {Character.Protagonist.Shuriken}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Hitpoints, out toEndParagraph, out toEndText);

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
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(option);

                        if (oneOption.Contains("ЖИЗНЕЙ >") && (level >= Character.Protagonist.Hitpoints))
                            return false;

                        if (oneOption.Contains("ЖИЗНЕЙ <=") && (level < Character.Protagonist.Hitpoints))
                            return false;

                        if (oneOption.Contains("СЮРИКЕНЫ >") && (level >= Character.Protagonist.Shuriken))
                            return false;

                        if (oneOption.Contains("СЮРИКЕНЫ <=") && (level < Character.Protagonist.Shuriken))
                            return false;

                        if (oneOption.Contains("СТРЕЛЫ >") && (level >= Character.Protagonist.Arrows))
                            return false;

                        if (oneOption.Contains("СТРЕЛЫ <=") && (level < Character.Protagonist.Arrows))
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

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Hitpoints < 5;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Hitpoints = 5;
    }
}

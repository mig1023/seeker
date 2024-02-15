using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.RockOfTerror
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status()
        {
            TimeSpan time = TimeSpan.FromMinutes(protagonist.Time);

            List<string> statusLines = new List<string> {
                $"Прошедшее время: {time.Hours:d2}:{time.Minutes:d2}" };

            if (protagonist.MonksHeart != null)
                statusLines.Add($"Сила сердца монаха: {protagonist.MonksHeart}");

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Время вышло...";

            return protagonist.Time >= 720;
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("СИЛА СЕРДЦА МОНАХА >=") && (level > protagonist.MonksHeart))
                    return false;

                else if (option.Contains("ВРЕМЯ >=") && (level > protagonist.Time))
                    return false;

                else if (option.Contains("ВРЕМЯ <") && (level < protagonist.Time))
                    return false;

                else
                    return true;
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }
    }
}

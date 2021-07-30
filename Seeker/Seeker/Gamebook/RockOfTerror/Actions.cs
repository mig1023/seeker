using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.RockOfTerror
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public override List<string> Status()
        {
            TimeSpan time = TimeSpan.FromMinutes(protogonist.Time);

            List<string> statusLines = new List<string> { String.Format("Прошедшее время: {0:d2}:{1:d2}", time.Hours, time.Minutes) };

            if (protogonist.MonksHeart != null)
                statusLines.Add(String.Format("Сила сердца монаха: {0}", protogonist.MonksHeart));

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Время вышло...";

            return protogonist.Time >= 720;
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Other.LevelParse(option);

                if (option.Contains("СИЛА СЕРДЦА МОНАХА >=") && (level > protogonist.MonksHeart))
                    return false;
                else if (option.Contains("ВРЕМЯ >=") && (level > protogonist.Time))
                    return false;
                else if (option.Contains("ВРЕМЯ <") && (level < protogonist.Time))
                    return false;
                else
                    return true;
            }
            else
                return CheckOnlyIfTrigger(option.Trim());
        }
    }
}

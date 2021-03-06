﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.RockOfTerror
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override List<string> Status()
        {
            TimeSpan time = TimeSpan.FromMinutes(Character.Protagonist.Time);

            List<string> statusLines = new List<string> { String.Format("Прошедшее время: {0:d2}:{1:d2}", time.Hours, time.Minutes) };

            if (Character.Protagonist.MonksHeart != null)
                statusLines.Add(String.Format("Сила сердца монаха: {0}", Character.Protagonist.MonksHeart));

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Время вышло...";

            return Character.Protagonist.Time >= 720;
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Other.LevelParse(option);

                if (option.Contains("СИЛА СЕРДЦА МОНАХА >=") && (level > Character.Protagonist.MonksHeart))
                    return false;
                else if (option.Contains("ВРЕМЯ >=") && (level > Character.Protagonist.Time))
                    return false;
                else if (option.Contains("ВРЕМЯ <") && (level < Character.Protagonist.Time))
                    return false;
                else
                    return true;
            }
            else
                return CheckOnlyIfTrigger(option.Trim());
        }
    }
}

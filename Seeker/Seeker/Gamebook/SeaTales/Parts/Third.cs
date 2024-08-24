using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.SeaTales.Parts
{
    class Third : IParts
    {
        public List<string> Status() => null;

        public List<string> AdditionalStatus() => new List<string>
        {
            $"Героизм: {Character.Protagonist.Heroism}",
            $"Алкоголизм: {Character.Protagonist.Alcoholism}",
            $"Авантюризм: {Character.Protagonist.Adventurism}",
            $"Плавание: {Character.Protagonist.Travel}",
            $"Репутация: {Character.Protagonist.Reputation}",
        };

        public List<string> Representer(Actions action)
        {
            return new List<string>();
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;

            if (Character.Protagonist.Alcoholism >= 10)
            {
                toEndParagraph = 720;
                toEndText = "Алкоголизм зашкалил...";
            }
            else if (Character.Protagonist.Heroism >= 110)
            {
                toEndParagraph = 940;
                toEndText = "Героизм зашкалил!!";
            }
            else if (Character.Protagonist.Adventurism <= 0)
            {
                toEndParagraph = 1050;
                toEndText = "Авантюризм зашкалил...";
            }
            else if (Character.Protagonist.Adventurism >= 10)
            {
                toEndParagraph = 830;
                toEndText = "Авантюризм зашкалил!!";
            }
            else if (Character.Protagonist.Travel >= 100)
            {
                toEndParagraph = 755;
                toEndText = "Плавание достигло своей цели!";
            }
            else if ((Character.Protagonist.Travel <= -100) && !Game.Option.IsTriggered("Параграф 888"))
            {
                toEndParagraph = 888;
                toEndText = "Плавание занесло куда-то не туда...";
            }
            else if (Character.Protagonist.Reputation <= -100)
            {
                toEndParagraph = 1160;
                toEndText = "Репутация зашкалила...";
            }
            else
            {
                toEndText = String.Empty;
                return false;
            }

            return true;
        }

        public List<string> Test(Actions action)
        {
            List<string> test = new List<string>();

            return test;
        }
    }
}

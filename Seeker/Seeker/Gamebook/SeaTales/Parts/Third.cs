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
            toEndText = String.Empty;
            return false;
        }

        public List<string> Test(Actions action)
        {
            List<string> test = new List<string>();

            return test;
        }
    }
}

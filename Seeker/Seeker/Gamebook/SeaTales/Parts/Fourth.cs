using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.SeaTales.Parts
{
    class Fourth : IParts
    {
        public List<string> Status()
        {
            return new List<string> { $"Пункты Повествования: {Character.Protagonist.NarrativePoints}" };
        }

        public List<string> AdditionalStatus() => null;

        public List<string> Representer(Actions action)
        {
            return new List<string> { String.Empty };
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;

            if (Character.Protagonist.NarrativePoints < 0)
            {
                Character.Protagonist.NarrativePoints = 0;

                toEndText = "Так, этот краснобай настолько заврался, " +
                    "что собутыльники перестали воспринимать краснобая " +
                    "всерьёз и подняли на смех. Рассказ начинает другой...";

                return true;
            }
            else if (Character.Protagonist.NarrativePoints >= 100)
            {
                Character.Protagonist.NarrativePoints = 0;

                toEndText = "Это была потрясающая история, но рассказчика " +
                    "перебил другой, чуть больше принявший на грудь и начинается " +
                    "совсем другая история!";

                return true;
            }
            else
            {
                toEndText = String.Empty;
                return false;
            }
        }

        public List<string> RandomOption() =>
            new List<string>();

        public List<string> Test(Actions action)
        {
            List<string> test = new List<string>();
            return test;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Catharsis
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public string Bonus { get; set; }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Bonus))
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Bonus).GetValue(Character.Protagonist, null);

                Dictionary<string, int> startValues = Constants.GetStartValues();

                int diff = (currentStat - startValues[Bonus]);

                string diffLine = (diff > 0 ? String.Format(" (+{0})", diff) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Text, diffLine) };
            }

            return new List<string>();
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Здоровье: {0}", Character.Protagonist.Life),
            String.Format("Аура: {0}", Character.Protagonist.Aura),
        };

        public override List<string> AdditionalStatus() =>  new List<string>
        {
            String.Format("Стелс: {0}", Character.Protagonist.Stealth),
            String.Format("Рукопашный бой: {0}", Character.Protagonist.Fight),
            String.Format("Меткость: {0}", Character.Protagonist.Accuracy),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled() => !((!String.IsNullOrEmpty(Bonus) && (Character.Protagonist.Bonuses <= 0)));

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                Character hero = Character.Protagonist;

                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("СТЕЛС") && (level > hero.Stealth))
                            return false;

                        else if (oneOption.Contains("МЕТКОСТЬ") && (level > hero.Accuracy))
                            return false;

                        else if (oneOption.Contains("РУКОПАШКА") && (level > hero.Fight))
                            return false;

                        else if (oneOption.Contains("АУРА <") && (level <= hero.Aura))
                            return false;

                        else if (oneOption.Contains("АУРА >") && (level > hero.Aura))
                            return false;

                        else if (oneOption.Contains("ЗДОРОВЬЕ") && (level > hero.Life))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Bonus) && (Character.Protagonist.Bonuses >= 0))
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Bonus).GetValue(Character.Protagonist, null);

                currentStat += 1;

                Character.Protagonist.GetType().GetProperty(Bonus).SetValue(Character.Protagonist, currentStat);

                Character.Protagonist.Bonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override bool IsHealingEnabled() => Character.Protagonist.Life < Character.Protagonist.MaxLife;

        public override void UseHealing(int healingLevel) => Character.Protagonist.Life += healingLevel;
    }
}

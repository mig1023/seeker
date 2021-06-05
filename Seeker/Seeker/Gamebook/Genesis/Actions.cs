using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.Genesis
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
            String.Format("Ловкость: {0}", Character.Protagonist.Skill),
            String.Format("Стелс: {0}", Character.Protagonist.Stealth),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled() => !(!String.IsNullOrEmpty(Bonus) && (Character.Protagonist.Bonuses <= 0));

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("РАНЕНИЯ"))
                    {
                        int woundsCount = Game.Data.Triggers.Where(x => x == "Ранение").Count();

                        if (woundsCount < 3)
                            return false;
                    }
                    else if (oneOption.Contains("="))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("СТЕЛС") && (level > Character.Protagonist.Stealth))
                            return false;
                        else if (oneOption.Contains("ЛОВКОСТЬ") && (level > Character.Protagonist.Skill))
                            return false;
                        else if (oneOption.Contains("ХОЛОДНОЕ ОРУЖИЕ") && (level > Character.Protagonist.Weapon))
                            return false;
                        else if (oneOption.Contains("АУРА") && (level > Character.Protagonist.Aura))
                            return false;
                        else if (oneOption.Contains("ЗДОРОВЬЕ") && (level > Character.Protagonist.Life))
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

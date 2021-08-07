using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Genesis
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public string Bonus { get; set; }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Bonus))
            {
                int currentStat = (int)protogonist.GetType().GetProperty(Bonus).GetValue(protogonist, null);

                Dictionary<string, int> startValues = Constants.GetStartValues();

                int diff = (currentStat - startValues[Bonus]);

                string diffLine = (diff > 0 ? String.Format(" (+{0})", diff) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Text, diffLine) };
            }

            return new List<string>();
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Здоровье: {0}", protogonist.Life),
            String.Format("Аура: {0}", protogonist.Aura),
            String.Format("Ловкость: {0}", protogonist.Skill),
            String.Format("Стелс: {0}", protogonist.Stealth),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protogonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled() => !(!String.IsNullOrEmpty(Bonus) && (protogonist.Bonuses <= 0));

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                Character protagonist = protogonist;

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

                        if (oneOption.Contains("СТЕЛС") && (level > protagonist.Stealth))
                            return false;
                        else if (oneOption.Contains("ЛОВКОСТЬ") && (level > protagonist.Skill))
                            return false;
                        else if (oneOption.Contains("ХОЛОДНОЕ ОРУЖИЕ") && (level > protagonist.Weapon))
                            return false;
                        else if (oneOption.Contains("АУРА") && (level > protagonist.Aura))
                            return false;
                        else if (oneOption.Contains("ЗДОРОВЬЕ") && (level > protagonist.Life))
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
            if (!String.IsNullOrEmpty(Bonus) && (protogonist.Bonuses >= 0))
            {
                int currentStat = (int)protogonist.GetType().GetProperty(Bonus).GetValue(protogonist, null);

                currentStat += 1;

                protogonist.GetType().GetProperty(Bonus).SetValue(protogonist, currentStat);

                protogonist.Bonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override bool IsHealingEnabled() => protogonist.Life < protogonist.MaxLife;

        public override void UseHealing(int healingLevel) => protogonist.Life += healingLevel;
    }
}

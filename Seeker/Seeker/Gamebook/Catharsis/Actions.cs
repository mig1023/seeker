using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Catharsis
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
        };

        public override List<string> AdditionalStatus() =>  new List<string>
        {
            String.Format("Стелс: {0}", protogonist.Stealth),
            String.Format("Рукопашный бой: {0}", protogonist.Fight),
            String.Format("Меткость: {0}", protogonist.Accuracy),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protogonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled() => !((!String.IsNullOrEmpty(Bonus) && (protogonist.Bonuses <= 0)));

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("СТЕЛС") && (level > protogonist.Stealth))
                            return false;

                        else if (oneOption.Contains("МЕТКОСТЬ") && (level > protogonist.Accuracy))
                            return false;

                        else if (oneOption.Contains("РУКОПАШКА") && (level > protogonist.Fight))
                            return false;

                        else if (oneOption.Contains("АУРА <") && (level <= protogonist.Aura))
                            return false;

                        else if (oneOption.Contains("АУРА >") && (level > protogonist.Aura))
                            return false;

                        else if (oneOption.Contains("ЗДОРОВЬЕ") && (level > protogonist.Life))
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

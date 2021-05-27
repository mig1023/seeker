using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.Catharsis
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Text { get; set; }
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

        public override bool IsButtonEnabled()
        {
            bool disbledByBonuses = (!String.IsNullOrEmpty(Bonus) && (Character.Protagonist.Bonuses <= 0));

            return !disbledByBonuses;
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("РАНЕНИЯ"))
                    {
                        int woundsCount = 0;

                        foreach (string trigger in Game.Data.Triggers)
                            if (trigger == "Ранение")
                                woundsCount += 1;

                        if (woundsCount < 3)
                            return false;
                    }
                    else if (oneOption.Contains("="))
                    {
                        int level = int.Parse(oneOption.Split('=', '<')[1]);

                        if (oneOption.Contains("СТЕЛС") && (level > Character.Protagonist.Stealth))
                            return false;
                        else if (oneOption.Contains("МЕТКОСТЬ") && (level > Character.Protagonist.Accuracy))
                            return false;
                        else if (oneOption.Contains("РУКОПАШКА") && (level > Character.Protagonist.Fight))
                            return false;
                        else if (oneOption.Contains("АУРА <") && (level <= Character.Protagonist.Aura))
                            return false;
                        else if (oneOption.Contains("АУРА >") && (level > Character.Protagonist.Aura))
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

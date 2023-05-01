using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Catharsis
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string Bonus { get; set; }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Bonus))
            {
                int diff = (GetProperty(protagonist, Bonus) - Constants.GetStartValues[Bonus]);
                string diffLine = (diff > 0 ? String.Format(" (+{0})", diff) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Head, diffLine) };
            }

            return new List<string>();
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Здоровье: {0}/{1}", protagonist.Life, protagonist.MaxLife),
            String.Format("Аура: {0}", protagonist.Aura),
        };

        public override List<string> AdditionalStatus() =>  new List<string>
        {
            String.Format("Меткость: {0}", protagonist.Accuracy),
            String.Format("Рукопашный бой: {0}", protagonist.Fight),
            String.Format("Стелс: {0}", protagonist.Stealth),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledByBonusesRemove = !String.IsNullOrEmpty(Bonus) &&
                ((GetProperty(protagonist, Bonus) - Constants.GetStartValues[Bonus]) <= 0) && secondButton;

            bool disabledByBonusesAdd = (!String.IsNullOrEmpty(Bonus)) && (protagonist.Bonuses <= 0) && !secondButton;

            return !(disabledByBonusesRemove || disabledByBonusesAdd);
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("СТЕЛС") && (level > protagonist.Stealth))
                            return false;

                        else if (oneOption.Contains("МЕТКОСТЬ") && (level > protagonist.Accuracy))
                            return false;

                        else if (oneOption.Contains("РУКОПАШКА") && (level > protagonist.Fight))
                            return false;

                        else if (oneOption.Contains("АУРА <") && (level <= protagonist.Aura))
                            return false;

                        else if (oneOption.Contains("АУРА >") && (level > protagonist.Aura))
                            return false;

                        else if (oneOption.Contains("ЗДОРОВЬЕ") && (level > protagonist.Life))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Bonus) && (protagonist.Bonuses >= 0))
                ChangeProtagonistParam(Bonus, protagonist, "Bonuses");

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Bonus, protagonist, "Bonuses", decrease: true);

        public override bool IsHealingEnabled() =>
            protagonist.Life < protagonist.MaxLife;

        public override void UseHealing(int healingLevel) =>
            protagonist.Life += healingLevel;
    }
}

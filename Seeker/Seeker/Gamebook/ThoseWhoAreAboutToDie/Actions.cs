using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            String.Format("Реакция: {0}/12", protagonist.Reaction),
            String.Format("Сила: {0}/12", protagonist.Strength),
            String.Format("Выносливость: {0}/12", protagonist.Endurance),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => !OneParamFail(x) || Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        if (OneParamFail(oneOption))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private static bool OneParamFail(string oneOption)
        {
            if (ParamFail("СИЛА", oneOption, protagonist.Strength))
                return true;

            else if (ParamFail("РЕАКЦИЯ", oneOption, protagonist.Reaction))
                return true;

            else if (ParamFail("ВЫНОСЛИВОСТЬ", oneOption, protagonist.Endurance))
                return true;

            else
                return false;
        }

        private static bool ParamFail(string paramName, string option, int param)
        {
            int level = Game.Services.LevelParse(option);

            if (option.Contains(String.Format("{0} >", paramName)) && (level >= param))
                return true;

            else if (option.Contains(String.Format("{0} <=", paramName)) && (level < param))
                return true;

            else
                return false;
        }

        public List<string> TryToWound()
        {
            List<string> report = new List<string>();

            int dice = Game.Dice.Roll();
            report.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));

            if (dice > 4)
            {
                protagonist.Reaction += 3;
                report.Add("BIG|GOOD|+3 к Реакции! :)");
            }
            else
            {
                report.Add("BIG|BAD|Не повезло :(");
            }

            return report;
        }
    }
}

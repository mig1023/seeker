using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public int Dices { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Реакция: {0}", Character.Protagonist.Reaction),
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                    if (!OneParamFail(oneOption))
                        return true;
                    else if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
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
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        private static bool OneParamFail(string oneOption)
        {
            if (ParamFail("СИЛА", oneOption, Character.Protagonist.Strength))
                return true;

            else if (ParamFail("РЕАКЦИЯ", oneOption, Character.Protagonist.Reaction))
                return true;

            else if (ParamFail("ВЫНОСЛИВОСТЬ", oneOption, Character.Protagonist.Endurance))
                return true;

            else
                return false;
        }

        private static bool ParamFail(string paramName, string option, int param)
        {
            int level = int.Parse(option.Split('>', '=')[1]);

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
                Character.Protagonist.Reaction += 3;
                report.Add("BIG|GOOD|+3 к Реакции! :)");
            }
            else
                report.Add("BIG|BAD|Не повезло :(");

            return report;
        }
    }
}

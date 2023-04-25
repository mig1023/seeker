using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.AntSurvival
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status()
        {
            List<string> statusLines = new List<string>();

            statusLines.Add(String.Format("Количество: {0}", protagonist.Quantity));
            statusLines.Add(String.Format("Прирост: {0}", protagonist.Increase));

            if (protagonist.Defence > 0)
                statusLines.Add(String.Format("Защита: {0}", protagonist.Defence));

            return statusLines;
        }

        public override List<string> AdditionalStatus() => protagonist.Enemy > 0 ?
            new List<string> { String.Format("{0}: {1}", protagonist.Name, protagonist.Enemy) } : null;

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
                    if (oneOption.Contains(">") || oneOption.Contains("<") || oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("ДАЙС =") && !protagonist.Dice[level])
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО >=") && (level > protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("КОЛИЧЕСТВО <") && (level <= protagonist.Quantity))
                            return false;

                        if (oneOption.Contains("ВРАГ >=") && (level > protagonist.Enemy))
                            return false;

                        if (oneOption.Contains("ВРАГ <") && (level <= protagonist.Enemy))
                            return false;

                        if (oneOption.Contains("ЗАЩИТА >=") && (level > protagonist.Defence))
                            return false;

                        if (oneOption.Contains("СТАРТ =") && (protagonist.Start != level))
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
    }
}

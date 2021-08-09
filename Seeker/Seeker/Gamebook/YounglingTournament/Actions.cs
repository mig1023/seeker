using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.YounglingTournament
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            String.Format("Cветлая сторона: {0}", protagonist.LightSide),
            String.Format("Тёмная сторона: {0}", protagonist.DarkSide),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Понимание Силы: {0}", protagonist.ForceTechniques.Values.Sum()),
            String.Format("Взлом: {0}", protagonist.Hacking),
            String.Format("Скрытность: {0}", protagonist.Stealth),
            String.Format("Пилот: {0}", protagonist.Pilot),
            String.Format("Меткость: {0}", protagonist.Accuracy),
            String.Format("Выносливость: {0}", protagonist.Hitpoints),
        };

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(option);

                        if (oneOption.Contains("ПИЛОТ >") && (level <= protagonist.Pilot))
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
    }
}

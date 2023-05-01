using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            String.Format("Сестерциев: {0}", protagonist.Sestertius),
            String.Format("Честь: {0}", protagonist.Honor),
        };

        public override List<string> AdditionalStatus()
        {
            if (protagonist.Legionaries > 0)
            {
                string legionaries = new string('♙', protagonist.Legionaries);

                return new List<string>
                {
                    String.Format("Легионеров: {0}", legionaries),
                    String.Format("Дисциплина: {0}", Game.Services.NegativeMeaning(protagonist.Discipline)),
                };
            }
            else if (protagonist.Horsemen > 0)
            {
                string horsemen = new string('♘', protagonist.Horsemen);
                return new List<string>
                {
                    String.Format("Всадников: {0}", horsemen),
                    String.Format("Навыки рукопашного боя: {0}", 2),
                };
            }
            else
                return null;
        }

        public override List<string> Representer()
        {
            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "сестерций", "сестерция", "сестерциев");
                return new List<string> { String.Format("{0}, {1} {2}", Head, Price, gold) };
            }
            else
                return new List<string> { };
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Ущерб чести слишком велик, лучше броситься на меч, а игру начать сначала";

            return protagonist.Honor <= 0;
        }

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !(Used || ((Price > 0) && (protagonist.Sestertius < Price)));

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                foreach (string optionsPart in option.Split('|').Select(x => x.Trim()))
                    if (Game.Option.IsTriggered(optionsPart))
                        return true;

                return false;
            }
            else
            {
                string[] options = option.Split(',');

                foreach (string oneOption in options)
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("СЕСТЕРЦИЕВ >=") && (level <= protagonist.Sestertius))
                            return true;

                        if (oneOption.Contains("ДИСЦИПЛИНА >=") && (level <= protagonist.Discipline))
                            return true;
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

        public List<string> Get()
        {
            protagonist.Sestertius -= Price;

            Used = true;

            return new List<string> { "RELOAD" };
        }
    }
}
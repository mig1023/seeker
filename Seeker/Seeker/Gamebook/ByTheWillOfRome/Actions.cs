using System;
using System.Collections.Generic;

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
                    String.Format("Дисциплина: {0}", protagonist.Discipline),
                    String.Format("Легионеров: {0}", legionaries),
                };
            }
            else if (protagonist.Horsemen > 0)
            {
                string horsemen = new string('♘', protagonist.Horsemen);
                return new List<string>
                {
                    String.Format("Навыки рукопашного боя: {0}", 2),
                    String.Format("Всадников: {0}", horsemen),
                };
            }
            else
                return null;
        }
            
        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Ущерб чести слишком велик, лучше броситься на меч, а игру начать сначала";

            return protagonist.Honor <= 0;
        }

        public override bool CheckOnlyIf(string option)
        {
            string[] options = option.Split('|', ',');

            foreach (string oneOption in options)
            {
                if (oneOption.Contains("!"))
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
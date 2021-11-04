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

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Ущерб чести слишком велик, лучше броситься на меч, а игру начать сначала";

            return protagonist.Honor <= 0;
        }
    }
}
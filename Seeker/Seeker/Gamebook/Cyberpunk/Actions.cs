using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Cyberpunk
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            String.Format("Планирование: {0}", protagonist.Planning),
            String.Format("Подготовка: {0}", protagonist.Preparation),
            String.Format("Везение: {0}", protagonist.Luck),
        };

        public override bool CheckOnlyIf(string option) => CheckOnlyIfTrigger(option);
    }
}

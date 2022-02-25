using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Moonrunner
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            String.Format("Мастерство: {0}", protagonist.Mastery),
            String.Format("Выносливость: {0}/{1}", protagonist.Endurance, protagonist.MaxEndurance),
            String.Format("Удача: {0}", protagonist.Luck),
            String.Format("Золото: {0}", protagonist.Gold)
        };

        public override bool CheckOnlyIf(string option) => CheckOnlyIfTrigger(option);
    }
}

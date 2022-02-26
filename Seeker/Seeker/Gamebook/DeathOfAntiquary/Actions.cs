using System;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override bool CheckOnlyIf(string option) => CheckOnlyIfTrigger(option);
    }
}

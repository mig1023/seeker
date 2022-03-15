using System;

namespace Seeker.Gamebook.Cyberpunk
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override bool CheckOnlyIf(string option) => CheckOnlyIfTrigger(option);
    }
}

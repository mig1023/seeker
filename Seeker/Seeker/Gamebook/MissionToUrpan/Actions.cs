using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.MissionToUrpan
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override List<string> Status() =>
            new List<string> { String.Format("Репутация: {0}", Character.Protagonist.Reputation) };

        public override bool CheckOnlyIf(string option) => CheckOnlyIfTrigger(option);
    }
}

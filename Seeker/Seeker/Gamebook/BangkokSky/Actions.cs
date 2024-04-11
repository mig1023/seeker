using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.BangkokSky
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<string> Get()
        {
            return new List<string> { "RELOAD" };
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool triggeredAlready = (Type == "Get") && Game.Option.IsTriggered(Trigger);
            bool advantagesCount = (Type == "Get") && (Game.Data.Triggers.Count >= 4);

            return !(triggeredAlready || advantagesCount);
        }
    }
}

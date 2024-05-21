using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.WrongWayGoBack
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status()
        {
            TimeSpan time = TimeSpan.FromSeconds(Character.Protagonist.Time);
            return new List<string> { "Оставшееся время:" + time.ToString(@"hh\:mm\:ss") };
        }
    }
}

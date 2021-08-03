using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.YounglingTournament
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            String.Format("Cветлая сторона: {0}", protogonist.LightSide),
            String.Format("Тёмная сторона: {0}", protogonist.DarkSide),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Взлом: {0}", protogonist.Hacking),
            String.Format("Скрытность: {0}", protogonist.Stealth),
            String.Format("Пилот: {0}", protogonist.Pilot),
        };
    }
}

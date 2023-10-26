using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.AlamutFortress
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            $"Сила: {protagonist.Strength}",
            $"Здоровье: {protagonist.Hitpoints}/{protagonist.MaxHitpoints}",
            $"Золото: {protagonist.Gold}"
        };
    }
}

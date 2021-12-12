using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.OrcsDay
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string>
        {
            String.Format("Оркишность: {0}", protagonist.Orcishness),
            String.Format("Здоровье: {0}", protagonist.Hitpoints),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Деньги: {0}", protagonist.Money),
            String.Format("Удача: {0}", protagonist.Luck),
            String.Format("Смелость: {0}", protagonist.Courage),
            String.Format("Мозги: {0}", protagonist.Wits),
            String.Format("Мышцы: {0}", protagonist.Muscle),
        };
    }
}

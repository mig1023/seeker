using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.VWeapons
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override List<string> Status() => new List<string>
        {
            String.Format("Подозрение: {0}/5", Character.Protagonist.Suspicions),
            String.Format("Время: {0}/12", Character.Protagonist.Time),
            String.Format("Меткость: {0}/5", Character.Protagonist.Accuracy),
            String.Format("Патроны: {0}", Character.Protagonist.Cartridges),
        };
    }
}

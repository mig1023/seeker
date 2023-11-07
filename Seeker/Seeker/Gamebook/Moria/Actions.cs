using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moria
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status()
        {
            bool canMakeMagic = protagonist.MagicPause == 0;

            string magic = canMakeMagic ? "доступно" :
                $"устал (ещё {protagonist.MagicPause} параграфа)";

            return new List<string> { $"Волшебство Гэндальфа: {magic}" };
        }

        public override List<string> AdditionalStatus()
        {
            List<string> fellowship = Constants.Fellowship
                .OrderByDescending(x => x.Value)
                .Where(x => protagonist.Fellowship.Contains(x.Key))
                .Select(x => x.Key)
                .ToList();

            return fellowship;
        }
    }
}

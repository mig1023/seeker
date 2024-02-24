using System.Collections.Generic;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<char, char> TranslateReplaces = new Dictionary<char, char>
        {
            ['б'] = 'а',
            ['ж'] = 'е',
            ['к'] = 'и',
            ['п'] = 'о',
            ['ф'] = 'у',

            ['Б'] = 'А',
            ['Ж'] = 'Е',
            ['К'] = 'И',
            ['П'] = 'О',
            ['Ф'] = 'У',
        };
    }
}

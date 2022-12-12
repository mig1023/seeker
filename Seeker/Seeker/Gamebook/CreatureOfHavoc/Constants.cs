using System.Collections.Generic;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

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

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            Availability = Actions.StaticInstance.Availability,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}

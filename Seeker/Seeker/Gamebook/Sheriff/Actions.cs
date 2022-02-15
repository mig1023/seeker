using System;

namespace Seeker.Gamebook.Sheriff
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("ВЖУХ >=") && (level > protagonist.Whoosh))
                    return false;

                if (option.Contains("ВЖУХ <") && (level <= protagonist.Whoosh))
                    return false;

                return true;
            }
            else
            {
                return CheckOnlyIfTrigger(option);
            }
        }
    }
}

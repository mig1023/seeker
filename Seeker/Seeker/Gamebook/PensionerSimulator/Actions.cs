using System;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
                return true;

            foreach (string oneOption in option.Split(','))
            {
                bool negativeTrigger = Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim());

                if (oneOption.Contains("!") && negativeTrigger)
                    return false;

                bool positiveTrigger = Game.Option.IsTriggered(oneOption.Trim());

                if (!oneOption.Contains("!") && !positiveTrigger)
                    return false;
            }

            return true;
        }
    }
}

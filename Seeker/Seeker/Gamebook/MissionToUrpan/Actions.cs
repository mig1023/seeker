using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.MissionToUrpan
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() =>
            new List<string> { String.Format("Репутация: {0}", protagonist.Reputation) };

        public override bool Availability(string option) => AvailabilityTrigger(option);

        public List<string> Result()
        {
            List<string> results = new List<string> { "BOLD|CЧИТАЕМ:" };

            int result = 0, lines = 0;

            if (protagonist.Reputation > 0)
            {
                result = protagonist.Reputation * 5;
                results.Add(String.Format("GOOD|+{0} очков за Репутацию", result));
                lines += 1;
            }

            foreach (KeyValuePair<string, int> condition in Constants.ResultCalculation())
            {
                string head = (condition.Value > 0 ? "GOOD|+" : "BAD|-");

                if (!Game.Option.IsTriggered(condition.Key))
                    continue;

                results.Add(String.Format("{0}{1} очков за «{2}»", head, Math.Abs(condition.Value), condition.Key));

                result += condition.Value;
                lines += 1;
            }

            if (lines == 0)
                results.Add("Да уж, вообще нечего вспомнить...");

            results.Add(String.Format("BIG|BOLD|ИТОГО: {0}", result));

            return results;
        }
    }
}

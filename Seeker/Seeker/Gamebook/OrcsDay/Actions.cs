using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.OrcsDay
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string Stat { get; set; }      

        public override List<string> Status() => new List<string>
        {
            String.Format("Оркишность: {0}", Game.Other.NegativeMeaning(protagonist.Orcishness)),
            String.Format("Здоровье: {0}", protagonist.Hitpoints),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Деньги: {0}", protagonist.Money),
            String.Format("Удача: {0}", Game.Other.NegativeMeaning(protagonist.Luck)),
            String.Format("Смелость: {0}", Game.Other.NegativeMeaning(protagonist.Courage)),
            String.Format("Мозги: {0}", Game.Other.NegativeMeaning(protagonist.Wits)),
            String.Format("Мышцы: {0}", Game.Other.NegativeMeaning(protagonist.Muscle)),
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Stat))
            {
                return new List<string> { String.Format("{0}\n(текущее значение: {1})",
                    Text, Game.Other.NegativeMeaning(GetProperty(protagonist, Stat))) };
            }

            return enemies;
        }

        public override bool IsButtonEnabled(bool secondButton = false) =>
            String.IsNullOrEmpty(Stat) || (protagonist.StatBonuses > 0) || secondButton;

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = GetProperty(protagonist, Stat);

                currentStat += 1;

                protagonist.GetType().GetProperty(Stat).SetValue(protagonist, currentStat);

                protagonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease()
        {
            int currentStat = GetProperty(protagonist, Stat);

            currentStat -= 1;

            protagonist.GetType().GetProperty(Stat).SetValue(protagonist, currentStat);

            protagonist.StatBonuses += 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> OrcishnessInit()
        {
            Character orc = protagonist;

            orc.Orcishness = 6;

            List<string> orcishness = new List<string> { "BOLD|Изначальное значение: 6" };

            if ((orc.Muscle < 0) || (orc.Wits < 0) || (orc.Courage < 0) || (orc.Luck < 0))
            {
                orcishness.Add("-1 Оркишность за отрицательный параметр");
                orc.Orcishness -= 1;
            }

            if (orc.Wits > orc.Muscle)
            {
                orcishness.Add("-1 Оркишность за то, что Мозги круче Мускулов");
                orc.Orcishness -= 1;
            }

            if (orc.Luck > 0)
            {
                orcishness.Add("-1 Оркишность за Удачу");
                orc.Orcishness -= 1;
            }
                
            if ((orc.Muscle > orc.Wits) && (orc.Muscle > orc.Courage) || (orc.Muscle > orc.Luck))
            {
                orcishness.Add("+1 Оркишность за то, что Мышцы самое крутое");
                orc.Orcishness += 1;
            }

            if (orc.Courage > orc.Wits)
            {
                orcishness.Add("+1 Оркишность за то, что Смелости больше, чем Мозгов");
                orc.Orcishness += 1;
            }
            if (orc.Courage > 2)
            {
                orcishness.Add("-1 Оркишность за то, что Смелости слишком много");
                orc.Orcishness -= 1;
            }

            orcishness.Add(String.Format("BIG|BOLD|Итоговая Оркишность: {0}", orc.Orcishness));

            return orcishness;

        }
    }
}

using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.BangkokSky
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Stat { get; set; }

        public override List<string> Representer()
        {
            List<string> test = new List<string>();

            if (!String.IsNullOrEmpty(Stat))
            {
                return new List<string> { $"{Head}\n(текущее значение: " +
                    $"{GetProperty(Character.Protagonist, Stat)})" };
            }

            return test;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool getAvantages = (Type == "Get") && String.IsNullOrEmpty(Stat);
            bool triggeredAlready = getAvantages && Game.Option.IsTriggered(Trigger);
            bool advantagesCount = getAvantages && (Game.Data.Triggers.Count >= 4);

            bool min = false, max = false;

            if (Type == "Get-Decrease")
            {
                int value = GetProperty(Character.Protagonist, Stat);
                min = secondButton && (value < 1);
                max = !secondButton && (value > 2);
            }

            return !(triggeredAlready || advantagesCount || min || max);
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
                ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses");

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);
    }
}

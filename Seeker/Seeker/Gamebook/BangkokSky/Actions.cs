using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.BangkokSky
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Stat { get; set; }

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.MartialArts > 0)
                statusLines.Add($"Боевые искусства: {Character.Protagonist.MartialArts}");

            if (Character.Protagonist.Physical > 0)
                statusLines.Add($"Физподготовка: {Character.Protagonist.Physical}");

            if (Character.Protagonist.Driving > 0)
                statusLines.Add($"Вождение: {Character.Protagonist.Driving}");

            if (Character.Protagonist.Firearms > 0)
                statusLines.Add($"Огнестрел: {Character.Protagonist.Firearms}");

            if (Character.Protagonist.Diplomacy > 0)
                statusLines.Add($"Дипломатия: {Character.Protagonist.Diplomacy}");

            if (Character.Protagonist.ConcreteJungle > 0)
                statusLines.Add($"Бетонные джунгли: {Character.Protagonist.ConcreteJungle}");

            return statusLines.Count <= 0 ? null : statusLines;
        }

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
                max = !secondButton && ((value > 2) || (Character.Protagonist.StatBonuses <= 0));
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

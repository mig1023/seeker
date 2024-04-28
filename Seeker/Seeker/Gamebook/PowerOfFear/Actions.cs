using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.PowerOfFear
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Stat { get; set; }
        public int Level { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Здоровье: {Character.Protagonist.Hitpoints}",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.Hunting > 0)
                statusLines.Add($"Охота: {Character.Protagonist.Hunting}");

            if (Character.Protagonist.Agility > 0)
                statusLines.Add($"Ловкость: {Character.Protagonist.Agility}");

            if (Character.Protagonist.Luck > 0)
                statusLines.Add($"Удача: {Character.Protagonist.Luck}");

            if (Character.Protagonist.Weapon > 0)
                statusLines.Add($"Владение оружием: {Character.Protagonist.Weapon}");

            if (Character.Protagonist.Theft > 0)
                statusLines.Add($"Взлом: {Character.Protagonist.Theft}");

            if (Character.Protagonist.Stealth > 0)
                statusLines.Add($"Скрытность: {Character.Protagonist.Stealth}");

            if (Character.Protagonist.Knowledge > 0)
                statusLines.Add($"Знания: {Character.Protagonist.Knowledge}");

            if (Character.Protagonist.Strength > 0)
                statusLines.Add($"Сила: {Character.Protagonist.Strength}");

            return statusLines.Count <= 0 ? null : statusLines;
        }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                return new List<string> { $"{Head}\n(текущее значение: " +
                    $"{GetProperty(Character.Protagonist, Stat)})" };
            }

            return new List<string>();
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
                min = secondButton && (value < 0);
                max = !secondButton && ((value >= 8);
            }

            return !(triggeredAlready || advantagesCount || min || max);
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses");
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);
    }
}

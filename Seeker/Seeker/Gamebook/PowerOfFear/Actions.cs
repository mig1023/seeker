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

        private int GetPropertiesCountByLevel(int level = 0)
        {
            int count = 0;

            foreach (string name in Constants.PropertiesNames)
            {
                bool anyLevel = level == 0;
                int current = GetProperty(Character.Protagonist, name);

                if (!anyLevel && (current == level))
                    count += 1;

                if (anyLevel && (current > 0))
                    count += 1;
            }

            return count;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool min = false, max = false, count = false;

            if (Type == "Get-Decrease")
            {
                int value = GetProperty(Character.Protagonist, Stat);
                min = secondButton && (value <= 0);
                max = !secondButton && (value >= 8);

                int countMax = GetPropertiesCountByLevel(8);
                bool alreadyAll = (GetPropertiesCountByLevel() >= 5) && (value <= 0);
                bool alreadyMax = (countMax >= 3) && (GetPropertiesCountByLevel(7) >= 2);
                bool alreadyMiddle = (countMax >= 3) && (value >= 7);

                count = (alreadyAll || alreadyMax || alreadyMiddle) && !secondButton;
            }

            return !(min || max || count);
        }

        private int GetSteps(int current)
        {
            if (current == 0)
                return 7;

            if (current == 7)
                return 1;

            return 0;
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                int current = GetProperty(Character.Protagonist, Stat);
                int bonus = GetSteps(current);
                SetProperty(Character.Protagonist, Stat, current + bonus);
            }

            return new List<string> { "RELOAD" };
        }

        private int DecreaseSteps(int current)
        {
            if (current == 8)
                return 1;

            if (current == 7)
                return 7;

            return 0;
        }

        public List<string> Decrease()
        {
            int current = GetProperty(Character.Protagonist, Stat);
            int decrease = DecreaseSteps(current);
            SetProperty(Character.Protagonist, Stat, current - decrease);

            return new List<string> { "RELOAD" };
        }
    }
}

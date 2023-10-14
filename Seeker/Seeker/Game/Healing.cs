using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Game
{
    class Healing
    {
        private string Name { get; set; }
        private int Level { get; set; }
        private int Portion { get; set; }

        private static List<Healing> HealingList = new List<Healing>();

        public static void Add(string fullLine)
        {
            List<string> healingParams = fullLine.Split(',').ToList();

            int count = (healingParams.Count > 2 ? int.Parse(healingParams[2]) : 1);

            Add(healingParams[0], int.Parse(healingParams[1]), count);
        }

        public static void Add(string name, int healing, int portions)
        {
            foreach(Healing currentHealing in HealingList.Where(x => x.Name == name))
            {
                currentHealing.Portion += portions;
                return;
            }

            Healing newHealing = new Healing
            {
                Name = name,
                Level = healing,
                Portion = portions,
            };

            HealingList.Add(newHealing);
        }

        public static void Use(string name)
        {
            List<string> healingName = name
                .Split('(')
                .ToList();

            List<Healing> healings = HealingList
                .Where(x => x.Name == healingName[0].Trim())
                .ToList();

            foreach (Healing healing in healings)
            {
                healing.Portion -= 1;
                Data.Actions.UseHealing(healing.Level);
                return;
            }
        }

        public static List<string> List()
        {
            List<string> allHealing = new List<string>();

            if (!Data.Actions.IsHealingEnabled())
                return allHealing;

            List<Healing> healings = HealingList
                .Where(x => x.Portion > 0)
                .ToList();

            foreach (Healing healing in healings)
                allHealing.Add($"{healing.Name} (осталось {healing.Portion})");

            return allHealing;
        }

        public static List<string> Debug()
        {
            List<string> allHealing = new List<string>();

            foreach (Healing currentHealing in HealingList)
            {
                allHealing.Add($"{currentHealing.Name} " +
                    $"(восстанавливает {currentHealing.Level}, " +
                    $"осталось {currentHealing.Portion})");
            }

            return allHealing;
        }

        public static string Save()
        {
            List<string> allHealing = new List<string>();

            foreach(Healing healing in HealingList)
                allHealing.Add($"{healing.Name},{healing.Level},{healing.Portion}");

            return String.Join("|", allHealing);
        }

        public static void Load(string saveLine)
        {
            HealingList.Clear();

            if (String.IsNullOrEmpty(saveLine))
                return;

            List<string> allHealing = saveLine.Split('|').ToList();

            foreach (string currentHealing in allHealing)
            {
                List<string> healingParam = currentHealing.Split(',').ToList();

                Healing healing = new Healing
                {
                    Name = healingParam[0],
                    Level = int.Parse(healingParam[1]),
                    Portion = int.Parse(healingParam[2]),
                };

                HealingList.Add(healing);
            }
        }

        public static void Clear() =>
            HealingList.Clear();
    }
}

using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string Stat { get; set; }

        public List<Character> Enemies { get; set; }
        public bool FireFight { get; set; }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = GetProperty(protagonist, Stat);
                string diffLine = String.Empty;

                if (currentStat > 11)
                {
                    diffLine = " (максимум)";
                }
                else if (currentStat > 1)
                {
                    diffLine = $" (значение {currentStat})";
                }

                return new List<string> { $"{Head}{diffLine}" };
            }
            else if (!String.IsNullOrEmpty(Head))
            {
                return new List<string> { Head };
            }
            else if (Enemies != null)
            {
                List<string> enemies = new List<string>();

                foreach (Character enemy in Enemies)
                    enemies.Add($"{enemy.Name}\n{enemy.Weapon} {enemy.Skill}  Ранений {enemy.Hitpoints}");

                return enemies;
            }
            else
            {
                return new List<string> { };
            }
        }

        public override List<string> Status() => new List<string>
        {
            $"Ранений: {protagonist.Hitpoints} из {protagonist.MaxHitpoints}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Сила: {protagonist.Strength}",
            $"Ловкость: {protagonist.Agility}",
            $"Удача: {protagonist.Luck}",
            $"Красноречие: {protagonist.Speech}",
            $"Стрельба: {protagonist.Firearms}",
            $"Фехтование: {protagonist.Fencing}",
            $"Верховая езда: {protagonist.HorseRiding}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
           GameOverBy((protagonist.MaxHitpoints - protagonist.Hitpoints), out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                int stat = GetProperty(protagonist, Stat);

                if (secondButton)
                    return (stat > 2) && (stat < 12);
                else
                    return ((protagonist.StatBonuses > 0) && (stat < 12));
            }
            else
            {
                return true;
            }
        }

        public List<string> Get()
        {
            if (protagonist.StatBonuses >= 0)
            {
                SetProperty(protagonist, Stat, GetProperty(protagonist, Stat) + 1);
                protagonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, protagonist, "StatBonuses", decrease: true);
    }
}

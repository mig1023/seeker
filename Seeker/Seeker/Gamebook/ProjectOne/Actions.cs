using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.ProjectOne
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {protagonist.Skill}/{protagonist.MaxSkill}",
            $"Сила: {protagonist.Endurance}/{protagonist.MaxEndurance}",
            $"Удача: {protagonist.Luck}/{protagonist.MaxLuck}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}  сила {enemy.Endurance}");

            return enemies;
        }
    }
}

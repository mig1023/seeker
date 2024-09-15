using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.WalkInThePark.Parts
{
    class First : IParts
    {
        public List<string> Status() => new List<string>
        {
            $"Сила: {Character.Protagonist.Strength}",
            $"Выносливость: {(double)Character.Protagonist.Endurance / 10}",
            $"Удача: {Character.Protagonist.Luck}",
        };

        public List<string> AdditionalStatus() => new List<string>
        {
            $"Оружие: {Character.Protagonist.Weapon} (урон {(double)Character.Protagonist.Damage / 10})",
            $"Деньги: {Character.Protagonist.Money} руб",
            $"Сэмки: {Character.Protagonist.SunflowerSeeds}",
        };

        public bool GameOver(Actions action, out int toEndParagraph, out string toEndText) =>
            action.GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);

        public List<string> Representer(Actions action)
        {
            List<string> enemies = new List<string>();

            if (action.Price > 0)
            {
                string price = Game.Services.CoinsNoun(action.Price, "рубль", "рубля", "рублей");
                return new List<string> { $"{action.Head}\n{action.Price} {price}" };
            }

            if (action.Enemies == null)
                return enemies;

            foreach (Character enemy in action.Enemies)
            {
                enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  " +
                    $"выносливость {enemy.Endurance}  урон {(double)enemy.Damage / 10}");
            }

            return enemies;
        }
    }
}

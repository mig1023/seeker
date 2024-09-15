using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.WalkInThePark.Parts
{
    class Second : IParts
    {
        public List<string> Status() => new List<string>
        {
            $"Самочувствие: {Character.Protagonist.Health} / {Character.Protagonist.MaxHealth}",
            $"Мочесть: {(double)Character.Protagonist.Strength}",
            $"Тихопопость: {Character.Protagonist.Stealth}",
            $"Фавор: {Character.Protagonist.Fortune}",
        };

        public List<string> AdditionalStatus() => new List<string>
        {
            $"Оружие: {Character.Protagonist.Weapon} (урон {Character.Protagonist.Damage})",
            $"Деньги: {Character.Protagonist.Money} руб",
            $"Рейтинг: {Character.Protagonist.Rating}",
            $"Частей карты: {Character.Protagonist.MapParts}",
            $"Сэмки: {Character.Protagonist.SunflowerSeeds}",
        };

        public bool GameOver(Actions action, out int toEndParagraph, out string toEndText)
        {
            if (Game.Data.CurrentParagraphID == 200)
            {
                toEndParagraph = 0;
                toEndText = Output.Constants.GAMEOVER_TEXT;
                return true;
            }
            else
            {
                toEndParagraph = 200;
                toEndText = "Финита ля комедия";
                return action.GameOverBy(Character.Protagonist.Health, out _, out _);
            }
        }

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
                enemies.Add($"{enemy.Name}\nсамочувствие {enemy.Health}  " +
                    $"мочность {enemy.Strength}  урон {(double)enemy.Damage}");
            }

            return enemies;
        }
    }
}

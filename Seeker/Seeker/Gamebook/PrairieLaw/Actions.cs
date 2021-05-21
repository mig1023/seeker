using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.PrairieLaw
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        public string Text { get; set; }
        public int Dices { get; set; }
        public Abstract.IModification Benefit { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Ловкость: {0}", Character.Protagonist.Skill),
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Обаяние: {0}", Character.Protagonist.Charm),
        };


        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Патронов: {0}", Character.Protagonist.Сartridges),
            String.Format("Долларов: {0}", Character.Protagonist.Dollars),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Strength <= 0;
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  сила {2}  патроны {3}",
                    enemy.Name, enemy.Skill, enemy.Strength, enemy.Сartridges));

            return enemies;
        }

        public static bool CheckOnlyIf(string option) => true;

        private string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += String.Format("{0} ", Character.Protagonist.Luck[i] ? Constants.LuckList()[i] : Constants.LuckList()[i + 10]);

            return luckListShow;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + LuckNumbers()
            };

            int goodLuck = Game.Dice.Roll();

            luckCheck.Add(String.Format("Проверка удачи: {0} - {1}зачёркунтый",
                Game.Dice.Symbol(goodLuck), (Character.Protagonist.Luck[goodLuck] ? "не " : String.Empty)));

            luckCheck.Add(Character.Protagonist.Luck[goodLuck] ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
                if (!Character.Protagonist.Luck[i])
                {
                    luckRecovery.Add(String.Format("GOOD|Цифра {0} восстановлена!", i));
                    Character.Protagonist.Luck[i] = true;
                    success = true;

                    break;
                }

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + LuckNumbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodCharm = (fisrtDice + secondDice) <= Character.Protagonist.Charm;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка обаяния: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodCharm ? "<=" : ">"), Character.Protagonist.Charm) };

            if (goodCharm)
            {
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");
                luckCheck.Add("Вы увеличили своё обаяние на единицу");

                Character.Protagonist.Charm += 1;
            }
            else
            {
                luckCheck.Add("BIG|BAD|НЕУДАЧА :(");

                if (Character.Protagonist.Charm > 2)
                {
                    luckCheck.Add("Вы уменьшили своё обаяние на единицу");
                    Character.Protagonist.Charm -= 1;
                }
            }

            return luckCheck;
        }

        public List<string> Skill()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodSkill = (fisrtDice + secondDice) <= Character.Protagonist.Skill;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка ловкости: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), Character.Protagonist.Skill) };

            luckCheck.Add(goodSkill ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            return luckCheck;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;

            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            Character.Protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Moonrunner
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public bool ThisIsSkill { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Мастерство: {0}", protagonist.Mastery),
            String.Format("Выносливость: {0}/{1}", protagonist.Endurance, protagonist.MaxEndurance),
            String.Format("Удача: {0}", protagonist.Luck),
            String.Format("Золото: {0}", protagonist.Gold)
        };

        public override bool CheckOnlyIf(string option) => CheckOnlyIfTrigger(option);

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledSkillSlots = ThisIsSkill && (protagonist.SkillSlots < 1);
            bool disabledSkillAlready = ThisIsSkill && Game.Option.IsTriggered(Text);

            return !(disabledSkillSlots || disabledSkillAlready);
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { String.Format("{0}, {1} {2}", Text, Price, gold) };
            }
            else if (ThisIsSkill)
            {
                return new List<string> { Text };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));

            return enemies;
        }

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool goodLuck = (firstDice + secondDice) <= protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка удачи: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), protagonist.Luck
            ) };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (protagonist.Luck > 1)
            {
                protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Get()
        {
            if (ThisIsSkill && (protagonist.SkillSlots >= 1))
            {
                Game.Option.Trigger(Text);
                protagonist.SkillSlots -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override bool IsHealingEnabled() => protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) => protagonist.Endurance += healingLevel;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moonrunner
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public int DevastatingAttack { get; set; }
        public int RoundsToFight { get; set; }
        public int WoundsLimit { get; set; }
        public bool ThisIsSkill { get; set; }
        public bool BitesEveryRound { get; set; }
        public bool Invulnerable { get; set; }
        public bool EnemyMasteryInc { get; set; }
        public bool DoubleFail { get; set; }

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
            {
                if (enemy.Endurance > 0)
                    enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));
                else
                    enemies.Add(String.Format("{0}\nмастерство {1} ", enemy.Name, enemy.Mastery));
            }

            return enemies;
        }

        private List<string> Luck(out bool goodLuck)
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            goodLuck = (firstDice + secondDice) <= protagonist.Luck;

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

        public List<string> Luck() => Luck(out bool _);

        public List<string> Get()
        {
            if (ThisIsSkill && (protagonist.SkillSlots >= 1))
            {
                Game.Option.Trigger(Text);
                protagonist.SkillSlots -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        private bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() == 0;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                if (BitesEveryRound && (protagonist.Endurance > 1))
                {
                    fight.Add("BAD|Из-за укусов вы теряете одну Выносливость!");
                    protagonist.Endurance -= 1;
                }

                foreach (Character enemy in FightEnemies)
                {
                    if ((enemy.Endurance <= 0) && !Invulnerable)
                        continue;

                    if (Invulnerable)
                        fight.Add(enemy.Name);
                    else
                        fight.Add(String.Format("{0} (выносливость {1})", enemy.Name, enemy.Endurance));

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Mastery;

                        fight.Add(String.Format("Сила вашего удара: {0} + {1} + {2} = {3}",
                            Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                            protagonist.Mastery, protagonistHitStrength));
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    fight.Add(String.Format("Сила его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Mastery, enemyHitStrength));

                    if (DoubleFail && (enemyRollFirst == enemyRollSecond))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|У противника выпал дубль!");
                        return fight;
                    }

                    if ((protagonistHitStrength > enemyHitStrength) && (!attackAlready || Game.Option.IsTriggered("Сражение")))
                    {
                        if (Invulnerable)
                        {
                            fight.AddRange(Luck(out bool goodLuck));

                            if (goodLuck)
                                return fight;
                        }
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                            enemy.Endurance -= 2;

                            bool enemyLost = NoMoreEnemies(FightEnemies);

                            if (enemyLost)
                            {
                                fight.Add(String.Empty);
                                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                                return fight;
                            }
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        protagonist.Endurance -= (DevastatingAttack > 0 ? DevastatingAttack : 2);

                        if (protagonist.Endurance <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }

                        if (EnemyMasteryInc)
                        {
                            fight.Add("BOLD|Мастерство противника увеличилось на единицу");
                            enemy.MaxMastery += 1;
                            enemy.Mastery += 1;
                        }
                    }
                    else
                        fight.Add("BOLD|Ничья в раунде");

                    attackAlready = true;

                    if ((RoundsToFight > 0) && (RoundsToFight <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|Отведённые на победу раунды истекли.");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() => protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) => protagonist.Endurance += healingLevel;
    }
}

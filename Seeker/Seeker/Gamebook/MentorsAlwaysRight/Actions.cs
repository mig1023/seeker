using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public List<Character> Enemies { get; set; }
        public bool ThisIsSpell { get; set; }
        public int OnlyRounds { get; set; }
        public int RoundsToWin { get; set; }
        public bool Regeneration { get; set; }

        public Character.SpecializationType? Specialization { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Жизни: {0}", Character.Protagonist.Hitpoints),
            String.Format("Обращений: {0}", Character.Protagonist.Transformation),
            String.Format("Золото: {0}", Character.Protagonist.Gold),
        };

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (Character.Protagonist.Spells == null)
                return null;

            foreach (string spell in Character.Protagonist.Spells)
            {
                if (currentSpells.ContainsKey(spell.ToLower()))
                    currentSpells[spell.ToLower()] += 1;
                else
                    currentSpells.Add(spell.ToLower(), 1);
            }

            if (currentSpells.Count <= 0)
                return null;

            List<string> statusLines = new List<string>();

            foreach (string spell in currentSpells.Keys.ToList().OrderBy(x => x))
                statusLines.Add(String.Format("{0}: {1}", spell.ToUpper(), currentSpells[spell]));

            return statusLines;
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Other.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { String.Format("{0}, {1} {2}", Text, Price, gold) };
            }
            else if ((Name == "Get") && ThisIsSpell)
            {
                int count = Character.Protagonist.Spells.Where(x => x == Text).Count();
                return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" ({0} шт)", count) : String.Empty)) };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nсила {1}  жизни {2}", enemy.Name, enemy.Strength, enemy.Hitpoints));

            return enemies;
        }

        public override bool IsButtonEnabled()
        {
            bool bySpell = ThisIsSpell && (Character.Protagonist.Magicpoints <= 0);
            bool bySpecButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool byPrice = (Price > 0) && (Character.Protagonist.Gold < Price);

            return !(bySpell || bySpecButton || byPrice);
        }

        public List<string> Get()
        {
            if (ThisIsSpell && (Character.Protagonist.Magicpoints >= 1))
            {
                Character.Protagonist.Spells.Add(Text);
                Character.Protagonist.Magicpoints -= 1;
            }
            else if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                    Character.Protagonist.Strength += 1;

                else if (Specialization == Character.SpecializationType.Thrower)
                    Character.Protagonist.Magicpoints += 1;

                else
                {
                    Character.Protagonist.Magicpoints += 2;
                    Character.Protagonist.Transformation += 2;
                }
            }

            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
                Character.Protagonist.Gold -= Price;

            return new List<string> { "RELOAD" };
        }

        private bool EnemyLostFight(List<Character> FightEnemies, ref List<string> fight)
        {
            if (FightEnemies.Where(x => x.Hitpoints > 0).Count() > 0)
                return false;
            else
            {
                fight.Add(String.Empty);
                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                if (Regeneration)
                {
                    int hitpointsBonus = Game.Dice.Roll();

                    Character.Protagonist.Hitpoints += hitpointsBonus;

                    fight.Add(String.Empty);
                    fight.Add(String.Format("GOOD|Благодаря крови, попавшей на вас, вы восстановили {0} жизни", hitpointsBonus));
                }

                return true;
            }
        }

        private List<string> LostFight(List<string> fight)
        {
            fight.Add(String.Empty);
            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

            return fight;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            bool warriorFight = (Character.Protagonist.Specialization == Character.SpecializationType.Warrior);

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                if ((Character.Protagonist.Specialization == Character.SpecializationType.Thrower) && (round == 1))
                {
                    fight.Add("BOLD|Вы бросаете метательные ножи");

                    FightEnemies[0].Hitpoints -= 3;

                    fight.Add(String.Format("GOOD|{0} ранен метательными ножами и потерял 3 жизни", FightEnemies[0].Name));
                    fight.Add(String.Empty);

                    if (EnemyLostFight(FightEnemies, ref fight))
                        return fight;
                }

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    if (Regeneration && (round % 4 == 0) && (enemy.Hitpoints < Enemies.Where(x => x.Name == enemy.Name).FirstOrDefault().Hitpoints))
                    {
                        enemy.Hitpoints += 1;
                        fight.Add(String.Format("BOLD|{0} восстановил 1 жизнь", enemy.Name));
                    }

                    fight.Add(String.Format("{0} (жизни: {1})", enemy.Name, enemy.Hitpoints));

                    int firstHeroRoll = Game.Dice.Roll();
                    int secondHeroRoll = Game.Dice.Roll();
                    int heroHitStrength = firstHeroRoll + secondHeroRoll + Character.Protagonist.Strength;

                    fight.Add(String.Format(
                        "Ваш удар: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(firstHeroRoll), Game.Dice.Symbol(secondHeroRoll),
                        Character.Protagonist.Strength, heroHitStrength));

                    int firstEnemyRoll = Game.Dice.Roll();
                    int secondEnemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Strength;

                    fight.Add(String.Format(
                        "Его удар: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(firstEnemyRoll), Game.Dice.Symbol(secondEnemyRoll),
                        enemy.Strength, enemyHitStrength));

                    if (warriorFight && (firstHeroRoll == secondHeroRoll) && (firstHeroRoll == 6))
                    {
                        fight.Add("BOLD|Вы сделали 'Крыло ястреба'!");
                        enemy.Hitpoints /= 2;
                        fight.Add(String.Format("GOOD|{0} ранен на половину своих жизней", enemy.Name));
                    }

                    else if (heroHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));
                        enemy.Hitpoints -= 2;

                        if (EnemyLostFight(FightEnemies, ref fight))
                            return fight;
                    }
                    else if (heroHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        Character.Protagonist.Hitpoints -= 2;

                        if (Character.Protagonist.Hitpoints <= 0)
                            return LostFight(fight);
                    }
                    else
                        fight.Add("BOLD|Ничья в раунде");

                    if ((OnlyRounds > 0) && (OnlyRounds <= round))
                    {
                        fight.Add("BOLD|Отведённые на бой раунды истекли.");
                        return fight;
                    }

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        return LostFight(fight);
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StrikeBack
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }

        public bool GroupFight { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public bool NotToDeath { get; set; }
        public int Count { get; set; }
        public bool WoundsByDices { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Атака: {0}", protagonist.Attack),
            String.Format("Защита: {0}", protagonist.Defence),
            String.Format("Выносливость: {0}", protagonist.Endurance),
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if ((Type != "Fight") && String.IsNullOrEmpty(Text))
            {
                return enemies;
            }
            else if (Type != "Fight")
            {
                return new List<string> { Text };
            }

            if ((Allies != null) && GroupFight)
            {
                enemies.Add(String.Format("Вы\nнападение {0}  защита {1}  жизнь {2}",
                    protagonist.Attack, protagonist.Defence, protagonist.Endurance));

                foreach (Character ally in Allies)
                    enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}",
                        ally.Name, ally.Attack, ally.Defence, ally.Endurance));

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}",
                    enemy.Name, enemy.Attack, enemy.Defence, enemy.Endurance));

            return enemies;
        }

        public List<string> Dices()
        {
            List<string> diceCheck = new List<string> { };

            int dices = Count == 0 ? 1 : Count;
            int result = 0;
            string lineFormat = ((dices > 1) || WoundsByDices ? String.Empty : "BIG|") +
                "На{0} кубике выпало: {1}";

            for (int i = 1; i <= dices; i++)
            {
                int dice = Game.Dice.Roll();
                result += dice;
                string diceNum = dices > 1 ? String.Format(" {0}", i) : String.Empty;
                diceCheck.Add(String.Format(lineFormat, diceNum, Game.Dice.Symbol(dice)));
            }

            if (WoundsByDices)
            {
                protagonist.Endurance -= result;
                diceCheck.Add(String.Format("BIG|BAD|Ты потерял выносливостей: {0}", result));
            }

            return diceCheck;
        }

        private static Character FindEnemyIn(List<Character> Enemies) =>
            Enemies.Where(e => e.Endurance > 0).FirstOrDefault();

        private static Character FindEnemy(Character fighter, List<Character> Allies, List<Character> Enemies)
        {
            if (Allies.Contains(fighter))
                return FindEnemyIn(Enemies);

            else if (Enemies.Contains(fighter))
                return FindEnemyIn(Allies);

            else
                return null;
        }

        private static bool IsProtagonist(string name) =>
            name == Character.Protagonist.Name;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1, enemyWounds = 0;

            List<Character> FightAllies = new List<Character> { protagonist };
            List<Character> FightEnemies = new List<Character>();
            List<Character> FightOrder = new List<Character>();
            Dictionary<string, int> WoundsCount = new Dictionary<string, int>();
            Dictionary<string, List<int>> AttackStory = new Dictionary<string, List<int>>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            if (Allies != null)
                foreach (Character ally in Allies)
                    FightAllies.Add(ally.Clone());

            if ((FightEnemies.Count > 1) && (FightAllies.Count == 1))
            {
                fight.Add("Противников много, а ты один, поэтому атакуют они первые :(");
                fight.Add(String.Empty);

                FightOrder.AddRange(FightEnemies);
                FightOrder.AddRange(FightAllies);
            }
            else
            {
                FightOrder.AddRange(FightAllies);
                FightOrder.AddRange(FightEnemies);
            }

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                foreach (Character fighter in FightOrder)
                {
                    if (fighter.Endurance <= 0)
                        continue;

                    Character enemy = FindEnemy(fighter, FightAllies, FightEnemies);

                    if (enemy == null)
                        continue;

                    fight.Add(String.Empty);

                    if (GroupFight)
                        fight.Add(String.Format("BOLD|{0} выбрает противника для атаки: {1}", fighter.Name, enemy.Name));
                    else
                        fight.Add(String.Format("BOLD|{0} атакует", fighter.Name));

                    Game.Dice.DoubleRoll(out int firstRoll, out int secondRoll);
                    int hitStrength = firstRoll + secondRoll + fighter.Attack;

                    fight.Add(String.Format(
                        "{0} cила атаки: {1} + {2} + {3} = {4}",
                        (IsProtagonist(fighter.Name) ? "Ваша" : String.Format("{0} -", fighter.Name)),
                        Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), fighter.Attack, hitStrength));

                    int hitDiff = hitStrength - enemy.Defence;
                    bool success = hitDiff > 0;

                    fight.Add(String.Format(
                        "{0}: {1} это {2} силы атаки",
                        (IsProtagonist(enemy.Name) ? "Ваша защита" : "Защита противника"),
                        enemy.Defence, Game.Services.Сomparison(enemy.Defence, hitStrength)));

                    if (IsProtagonist(enemy.Name))
                    {
                        fight.Add(success ? "BAD|BOLD|Вы ранены" : "GOOD|Атака отбита");
                    }
                    else  if (FightAllies.Contains(enemy))
                    {
                        fight.Add(success ? String.Format("BAD|{0} ранен", enemy.Name) : "GOOD|Атака отбита");
                    }
                    else
                    {
                        fight.Add(success ? String.Format("GOOD|BOLD|{0} ранен", enemy.Name) : "BAD|Атака отбита");
                    }

                    if (success)
                    {
                        enemy.Endurance -= hitDiff;
                        fight.Add(String.Format("{0} потерял вносливости: {1} (осталось: {2})",
                            enemy.Name, hitDiff, enemy.Endurance));
                    }
                }

                bool enemyLost = FightEnemies.Where(x => (x.Endurance > 0) || (x.MaxEndurance == 0)).Count() == 0;

                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                {
                    fight.Add("BIG|GOOD|ВЫ ПОБЕДИЛИ :)");
                    return fight;
                }

                bool allyLost = FightAllies.Where(x => x.Endurance > 0).Count() == 0;

                if (allyLost)
                {
                    fight.Add("BIG|BAD|ВЫ ПРОИГРАЛИ :(");

                    if (NotToDeath)
                        protagonist.Endurance += 1;

                    return fight;
                }

                if ((RoundsToWin > 0) && (RoundsToWin <= round))
                {
                    fight.Add("BAD|Отведённые на победу раунды истекли.");
                    fight.Add("BIG|BAD|ВЫ ПРОИГРАЛИ :(");
                    return fight;
                }

                fight.Add(String.Empty);

                round += 1;
            }
        }
    }
}

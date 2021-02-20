using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public string Text { get; set; }
        public string Stat { get; set; }
        public int StatStep { get; set; }
        public Character.SpecialTechniques SpecialTechnique { get; set; }

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public bool GroupFight { get; set; }


        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };
            else if (Enemies == null)
                return enemies;

            if ((Allies != null) && GroupFight)
            {
                foreach (Character ally in Allies)
                    if (ally.Name == Character.Protagonist.Name)
                        enemies.Add(String.Format(
                            "Вы\nатака {0}  защита {1}  жизнь {2}  инициатива {3}",
                            Character.Protagonist.Attack, Character.Protagonist.Defence,
                            Character.Protagonist.Endurance, Character.Protagonist.Initiative
                        ));
                    else
                        enemies.Add(String.Format(
                            "{0}\nатака {1}  защита {2}  жизнь {3}  инициатива {4}",
                            ally.Name, ally.Attack, ally.Defence, ally.Endurance, ally.Initiative
                        ));

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format(
                    "{0}\nатака {1}  защита {2}  жизнь {3}  инициатива {4}",
                    enemy.Name, enemy.Attack, enemy.Defence, enemy.Endurance, enemy.Initiative
                ));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Нападение: {0}", Character.Protagonist.Attack),
                String.Format("Защита: {0}", Character.Protagonist.Defence),
                String.Format("Жизнь: {0}", Character.Protagonist.Endurance),
                String.Format("Инициатива: {0}", Character.Protagonist.Initiative)
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return (Character.Protagonist.Endurance <= 0);
        }

        public bool IsButtonEnabled()
        {
            bool disabledSpecialTechniqueButton = (SpecialTechnique != Character.SpecialTechniques.Nope) &&
                (Character.Protagonist.SpecialTechnique.Count > 0);

            bool disabledStatBonuses = (!String.IsNullOrEmpty(Stat)) && (Character.Protagonist.Bonuses <= 0);

            return !(disabledSpecialTechniqueButton || disabledStatBonuses);
        }

        public static bool CheckOnlyIf(string option) => true;

        public List<string> Get()
        {
            if ((SpecialTechnique != Character.SpecialTechniques.Nope) && (Character.Protagonist.SpecialTechnique.Count == 0))
                Character.Protagonist.SpecialTechnique.Add(SpecialTechnique);

            else if ((StatStep > 0) && (Character.Protagonist.Bonuses >= 0))
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                currentStat += StatStep;

                Character.Protagonist.GetType().GetProperty("Max" + Stat).SetValue(Character.Protagonist, currentStat);
                Character.Protagonist.GetType().GetProperty(Stat).SetValue(Character.Protagonist, currentStat);

                Character.Protagonist.Bonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        private bool IsHero(string name) => name == Character.Protagonist.Name;

        private Character FindEnemyIn(List<Character> Enemies)
        {
            foreach (Character enemy in Enemies)
                if (enemy.Endurance > 0)
                    return enemy;

            return null;
        }

        private Character FindEnemy(Character fighter, List<Character> Allies, List<Character> Enemies)
        {
            if (Allies.Contains(fighter))
                return FindEnemyIn(Enemies);

            else if (Enemies.Contains(fighter))
                return FindEnemyIn(Allies);

            else
                return null;
        }

        private int InitiativeAndDices(Character character, out string line)
        {
            int firstRoll = Game.Dice.Roll();
            int secondRoll = Game.Dice.Roll();
            int initiative = firstRoll + secondRoll + character.Initiative;

            line = String.Format("{0} + {1} + {2}, итого {3}",
                character.Initiative, Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), initiative
            );

            return initiative;
        }

        private void Attack(Character attacker, Character defender, ref List<string> fight, List<Character> Allies)
        {
            int firstRoll = Game.Dice.Roll();
            int secondRoll = Game.Dice.Roll();
            int attackStrength = firstRoll + secondRoll + attacker.Attack;

            fight.Add(
                String.Format(
                    "Мощность удара: {0} + {1} + {2} = {3}",
                    Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(firstRoll), attacker.Attack, attackStrength
                )
            );

            bool success = attackStrength > defender.Defence;

            fight.Add(
                String.Format(
                    "Защита: {0} {1} {2}",
                    defender.Defence, (success ? "меньше" : "больше или равна"), attackStrength
                )
            );

            if (success)
            {
                defender.Endurance -= 2;

                string defenderName = (IsHero(defender.Name) ? "Вы ранены" : String.Format("{0} ранен", defender.Name));

                fight.Add(String.Format("{0}|{1}", (Allies.Contains(defender) ? "BAD" : "GOOD"), defenderName));
            }
            else
                fight.Add("Атака отбита");
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int iHero, iEnemy, round = 1, enemyWounds = 0;
            string heroLine, enemyLine, iTemplate = "{0} (инициатива {1})";

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();
            List<Character> FightAll = new List<Character>();
            List<Character> FightOrder = null;

            foreach (Character enemy in Enemies)
            {
                Character enemyClone = enemy.Clone();
                FightEnemies.Add(enemyClone);
                FightAll.Add(enemyClone);
            };

            if (Allies == null)
            {
                FightAllies.Add(Character.Protagonist);
                FightAll.Add(Character.Protagonist);
            }
            else
                foreach (Character ally in Allies)
                    if (ally.Name == Character.Protagonist.Name)
                    {
                        FightAllies.Add(Character.Protagonist);
                        FightAll.Add(Character.Protagonist);
                    }
                    else
                    {
                        Character allyClone = ally.Clone();
                        FightAllies.Add(allyClone);
                        FightAll.Add(allyClone);
                    }

            fight.Add("ОЧЕРЁДНОСТЬ УДАРОВ:");

            if (GroupFight)
            {
                FightOrder = FightAll.OrderByDescending(o => o.Initiative).ToList();

                foreach (Character fighter in FightOrder)
                    fight.Add(String.Format(iTemplate, fighter.Name, fighter.Initiative));
            }
            else
            {
                FightOrder = new List<Character>();

                do
                {
                    iHero = InitiativeAndDices(Character.Protagonist, out heroLine);
                    iEnemy = InitiativeAndDices(FightEnemies[0], out enemyLine);
                }
                while (iHero == iEnemy);

                FightOrder.Add(Character.Protagonist);

                if (iHero > iEnemy)
                {
                    fight.Add(String.Format(iTemplate, Character.Protagonist.Name, heroLine));
                    fight.Add(String.Format(iTemplate, FightEnemies[0].Name, enemyLine));

                    FightOrder.Add(FightEnemies[0]);
                }
                else
                {
                    fight.Add(String.Format(iTemplate, FightEnemies[0].Name, enemyLine));
                    fight.Add(String.Format(iTemplate, Character.Protagonist.Name, heroLine));

                    FightOrder.Insert(0, FightEnemies[0]);
                }
            }

            fight.Add(String.Empty);

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach(Character fighter in FightOrder)
                {
                    if (fighter.Endurance <= 0)
                        continue;

                    Character enemy = FindEnemy(fighter, FightAllies, FightEnemies);

                    if (enemy == null)
                        continue;

                    fight.Add(String.Empty);
                    fight.Add(String.Format("BOLD|{0} выбрает противника для атаки: {1}", fighter.Name, enemy.Name));
                    Attack(fighter, enemy, ref fight, FightAllies);
                }

                bool enemyLost = true;

                foreach (Character e in FightEnemies)
                    if (e.Endurance > 0)
                        enemyLost = false;

                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                {
                    fight.Add(String.Empty);
                    fight.Add("BIG|GOOD|ВЫ ПОБЕДИЛИ :)");
                    return fight;
                }

                bool allyLost = true;

                foreach (Character a in FightAllies)
                    if (a.Endurance > 0)
                        allyLost = false;

                if (allyLost)
                {
                    fight.Add(String.Empty);
                    fight.Add("BIG|GOOD|ВЫ ПРОИГРАЛИ :(");
                    return fight;
                }

                if ((RoundsToWin > 0) && (RoundsToWin <= round))
                {
                    fight.Add(String.Empty);
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

using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }

        // Fight
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }

        // Get
        public string Text { get; set; }
        public int Price { get; set; }
        public bool ThisIsSpell { get; set; }

        public List<string> Do(out bool reload, string action = "")
        {
            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Мастерство: {0}", Character.Protagonist.Mastery),
                String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
                String.Format("Удача: {0}", Character.Protagonist.Luck),
                String.Format("Золото: {0}", Character.Protagonist.Gold)
            };

            return statusLines;
        }

        public bool GameOver()
        {
            return (Character.Protagonist.Endurance <= 0 ? true : false);
        }

        public bool IsButtonEnabled()
        {
            return (ThisIsSpell && (Character.Protagonist.SpellSlots <= 0) ? false : true);
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (ActionName == "Get")
            {
                string countMarker = String.Empty;

                if (ThisIsSpell)
                {
                    int count = 0;

                    foreach (string spell in Character.Protagonist.Spells)
                        if (spell == Text)
                            count += 1;

                    if (count > 0)
                        countMarker = String.Format(" (x{0})", count);
                }

                return new List<string> { String.Format("{0}{1}", Text, countMarker) };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));

            return enemies;
        }

        public List<string> Luck()
        {
            bool goodLuck = Game.Dice.Roll(dices: 2) < Character.Protagonist.Luck;

            Character.Protagonist.Luck -= 1;

            return new List<string> { (goodLuck ? "BIG|HEAD|GOOD|УСПЕХ :)" : "BIG|HEAD|BAD|НЕУДАЧА :(") };
        }

        public List<string> Get()
        {
            if (Character.Protagonist.SpellSlots >= 1)
            {
                Character.Protagonist.Spells.Add(Text);
                Character.Protagonist.SpellSlots -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            int enemyWounds = 0;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character enemy in Enemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    fight.Add(String.Format("{0} (выносливость {1})", enemy.Name, enemy.Endurance));

                    int protagonistHitStrength = Game.Dice.Roll(dices: 2) + Character.Protagonist.Mastery;
                    fight.Add(String.Format("Сила вашего удара: {0}", protagonistHitStrength));

                    int enemyHitStrength = Game.Dice.Roll(dices: 2) + enemy.Mastery;
                    fight.Add(String.Format("Сила его удара: {0}", enemyHitStrength));

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("GOOD|Вы ранили противника"));
                        enemy.Endurance -= 2;

                        if (enemy.Endurance <= 0)
                            enemy.Endurance = 0;

                        enemyWounds += 1;

                        bool enemyLost = true;

                        foreach (Character e in Enemies)
                            if (e.Endurance > 0)
                                enemyLost = false;

                        if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|Противник ранил вас"));
                        Character.Protagonist.Endurance -= 2;

                        if (Character.Protagonist.Endurance < 0)
                            Character.Protagonist.Endurance = 0;

                        if (Character.Protagonist.Endurance <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add(String.Format("BAD|Отведённые на победу раунды истекли. Вы ПРОИГРАЛИ :(", RoundsToWin));
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}

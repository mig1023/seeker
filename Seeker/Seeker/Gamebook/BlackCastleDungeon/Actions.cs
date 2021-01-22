using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }

        public string Text { get; set; }
        public int Price { get; set; }
        public bool Used { get; set; }
        public bool Multiple { get; set; }
        public Modification Benefit { get; set; }
        public bool ThisIsSpell { get; set; }

        static Dictionary<string, bool> SpellActivate = new Dictionary<string, bool>
        {
            ["ЗАКЛЯТИЕ КОПИИ"] = false,
            ["ЗАКЛЯТИЕ СИЛЫ"] = false,
            ["ЗАКЛЯТИЕ СЛАБОСТИ"] = false,
        };

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

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

        public List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            foreach (string spell in Character.Protagonist.Spells)
            {
                string shortSpellName = spell.Replace("ЗАКЛЯТИЕ ", String.Empty).ToLower();

                if (currentSpells.ContainsKey(shortSpellName))
                    currentSpells[shortSpellName] += 1;
                else
                    currentSpells.Add(shortSpellName, 1);
            }

            if (currentSpells.Count <= 0)
                return null;

            List<string> statusLines = new List<string>();

            foreach (string spell in currentSpells.Keys.ToList().OrderBy(q => q))
                statusLines.Add(String.Format("{0}: {1}", char.ToUpper(spell[0]) + spell.Substring(1), currentSpells[spell]));

            return statusLines;
        }

        private static bool ParagraphWithFight(string spell)
        {
            if (Game.Data.CurrentParagraph.Actions == null)
                return false;

            foreach (Game.Option option in Game.Data.CurrentParagraph.Options)
                if (option.Text.ToUpper().Contains(spell))
                    return false;

            foreach (Actions action in Game.Data.CurrentParagraph.Actions)
                if (action.Enemies != null)
                    return true;

            return false;
        }

        public List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if (Character.Protagonist.Spells.Contains("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ") && (Character.Protagonist.Endurance < Character.Protagonist.MaxEndurance))
                staticButtons.Add("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ");

            foreach (string spell in new List<string> { "ЗАКЛЯТИЕ КОПИИ", "ЗАКЛЯТИЕ СИЛЫ", "ЗАКЛЯТИЕ СЛАБОСТИ" })
                if (ParagraphWithFight(spell) && Character.Protagonist.Spells.Contains(spell) && !SpellActivate[spell])
                    staticButtons.Add(spell);

            return staticButtons;
        }

        public bool StaticAction(string action)
        {
            Character.Protagonist.Spells.Remove(action);

            if (action.Contains("ИСЦЕЛЕНИЯ"))
                Character.Protagonist.Endurance += 8;

            foreach (string spell in new List<string> { "ЗАКЛЯТИЕ КОПИИ", "ЗАКЛЯТИЕ СИЛЫ", "ЗАКЛЯТИЕ СЛАБОСТИ" })
                if (action == spell)
                    SpellActivate[spell] = true;

            return true;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Endurance <= 0;
        }

        public bool IsButtonEnabled()
        {
            bool disabledSpellButton = ThisIsSpell && (Character.Protagonist.SpellSlots <= 0);
            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Gold < Price);

            return !(disabledSpellButton || disabledGetOptions || disabledByPrice);
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("ЗОЛОТО >="))
                return int.Parse(option.Split('=')[1]) <= Character.Protagonist.Gold;
            else if (option.Contains("ЗАКЛЯТИЕ"))
                return Character.Protagonist.Spells.Contains(option);
            else
                return Game.Data.Triggers.Contains(option);
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
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodLuck = (fisrtDice + secondDice) < Character.Protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), Character.Protagonist.Luck
            ) };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Get()
        {
            if (ThisIsSpell && (Character.Protagonist.SpellSlots >= 1))
            {
                Character.Protagonist.Spells.Add(Text);
                Character.Protagonist.SpellSlots -= 1;
            }
            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
            {
                Character.Protagonist.Gold -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        private bool WinInFight(ref List<string> fight, ref int round, ref Character hero, ref List<Character> FightEnemies,
            ref int enemyWounds, bool copyFight = false)
        {
            if (copyFight)
            {
                fight.Add(String.Format("BOLD|Вместо вас будет сражаться: {0}", hero.Name));
                fight.Add(String.Empty);
            }

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool attackAlready = false;
                int heroHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    fight.Add(String.Format("{0} (выносливость {1})", enemy.Name, enemy.Endurance));

                    if (copyFight)
                        fight.Add(String.Format("{0} (выносливость {1})", hero.Name, hero.Endurance));

                    if (!attackAlready)
                    {
                        int firstHeroRoll = Game.Dice.Roll();
                        int secondHeroRoll = Game.Dice.Roll();
                        heroHitStrength = firstHeroRoll + secondHeroRoll + hero.Mastery;

                        fight.Add(String.Format(
                                "Сила {0}: {1} + {2} + {3} = {4}",
                                (copyFight ? "удара копии" : "вашего удара"),
                                Game.Dice.Symbol(firstHeroRoll), Game.Dice.Symbol(secondHeroRoll), hero.Mastery, heroHitStrength
                        ));
                    }

                    int firstEnemyRoll = Game.Dice.Roll();
                    int secondEnemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Mastery;

                    fight.Add(String.Format(
                            "Сила удара врага: {0} + {1} + {2} = {3}",
                            Game.Dice.Symbol(firstEnemyRoll), Game.Dice.Symbol(secondEnemyRoll), enemy.Mastery, enemyHitStrength
                    ));

                    if ((heroHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));
                        enemy.Endurance -= 2;

                        enemyWounds += 1;

                        bool enemyLost = true;

                        foreach (Character e in FightEnemies)
                            if (e.Endurance > 0)
                                enemyLost = false;

                        if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                            return true;
                    }
                    else if (heroHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог ранить", enemy.Name));
                    }
                    else if (heroHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил {1}", enemy.Name, (copyFight ? "копию" : "вас")));
                        
                        hero.Endurance -= 2;

                        if (hero.Endurance <= 0)
                            return false;
                    }
                    else
                        fight.Add("BOLD|Ничья в раунде");

                    attackAlready = true;

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        return false;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            int enemyWounds = 0;

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            if (SpellActivate["ЗАКЛЯТИЕ СЛАБОСТИ"])
            {
                SpellActivate["ЗАКЛЯТИЕ СЛАБОСТИ"] = false;

                int oldEnemyMastery = FightEnemies[0].Mastery;
                FightEnemies[0].Mastery -= 2;

                fight.Add(String.Format(
                    "BOLD|Заклятье слабости ослабляет вашего противника: {0} теперь имеет ловкость {1} вместо {2}",
                    FightEnemies[0].Name, FightEnemies[0].Mastery, oldEnemyMastery
                ));

                fight.Add(String.Empty);
            }

            if (SpellActivate["ЗАКЛЯТИЕ КОПИИ"])
            {
                SpellActivate["ЗАКЛЯТИЕ КОПИИ"] = false;

                Character enemyCopy = FightEnemies[0].Clone();
                enemyCopy.Name += "-копия";

                bool copyWin = WinInFight(ref fight, ref round, ref enemyCopy, ref FightEnemies, ref enemyWounds, copyFight: true);

                fight.Add(String.Empty);

                if (copyWin)
                {
                    fight.Add("BIG|GOOD|Копия ПОБЕДИЛА :)");
                    return fight;
                }

                else if ((RoundsToWin > 0) && (RoundsToWin <= round))
                {
                    fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                    return fight;
                }

                else
                    fight.Add("BOLD|BAD|Копия проиграла, дальше сражаться придётся вам");

                fight.Add(String.Empty);
            }

            int oldMastery = Character.Protagonist.Mastery;

            if (SpellActivate["ЗАКЛЯТИЕ СИЛЫ"])
            {
                SpellActivate["ЗАКЛЯТИЕ СИЛЫ"] = false;

                Character.Protagonist.Mastery += 2;

                fight.Add(String.Format(
                    "BOLD|Заклятье Силы увеличивает ваше мастерство: на время этого боя она равна {0}",
                    Character.Protagonist.Mastery
                ));

                fight.Add(String.Empty);
            }

            bool win = WinInFight(ref fight, ref round, ref Character.Protagonist, ref FightEnemies, ref enemyWounds);

            Character.Protagonist.Mastery = oldMastery;

            fight.Add(String.Empty);
            fight.Add(win ? "BIG|GOOD|Вы ПОБЕДИЛИ :)" : "BIG|BAD|Вы ПРОИГРАЛИ :(");

            return fight;
        }
    }
}

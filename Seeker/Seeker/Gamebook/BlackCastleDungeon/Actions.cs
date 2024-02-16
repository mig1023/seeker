using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int StrengthPenlty { get; set; }
        public int ExtendedDamage { get; set; }

        public bool ThisIsSpell { get; set; }

        static Dictionary<string, bool> SpellActivate = new Dictionary<string, bool>
        {
            ["ЗАКЛЯТИЕ КОПИИ"] = false,
            ["ЗАКЛЯТИЕ СИЛЫ"] = false,
            ["ЗАКЛЯТИЕ СЛАБОСТИ"] = false,
        };

        public override List<string> Status() => new List<string>
        {
            $"Мастерство: {protagonist.Mastery}",
            $"Выносливость: {protagonist.Endurance}/{protagonist.MaxEndurance}",
            $"Удача: {protagonist.Luck}",
            $"Золото: {protagonist.Gold}"
        };

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (protagonist.Spells == null)
                return null;

            foreach (string spell in protagonist.Spells)
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
                statusLines.Add($"{char.ToUpper(spell[0]) + spell.Substring(1)}: {currentSpells[spell]}");

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            bool healing = protagonist.Spells.Contains("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ");
            bool wounded = protagonist.Endurance < protagonist.MaxEndurance;

            if (healing && wounded)
                staticButtons.Add("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ");

            foreach (string spell in Constants.StaticSpells)
            {
                bool spellAvailable = protagonist.Spells.Contains(spell) && !SpellActivate[spell];

                if (Fights.ParagraphWith(spell) && spellAvailable)
                    staticButtons.Add(spell);
            }

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            protagonist.Spells.Remove(action);

            if (action.Contains("ИСЦЕЛЕНИЯ"))
                protagonist.Endurance += 8;

            if (Constants.StaticSpells.Contains(action))
                SpellActivate[action] = true;

            return true;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledSpellAdd = ThisIsSpell && (protagonist.SpellSlots <= 0) && !secondButton;
            bool disabledSpellRemove = ThisIsSpell && !protagonist.Spells.Contains(Head) && secondButton;
            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (protagonist.Gold < Price);

            return !(disabledSpellAdd || disabledSpellRemove || disabledGetOptions || disabledByPrice);
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("ЗОЛОТО >="))
            {
                return int.Parse(option.Split('=')[1]) <= protagonist.Gold;
            }
            else if (option.Contains("ЗАКЛЯТИЕ"))
            {
                return protagonist.Spells.Contains(option);
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { $"{Head}, {Price} {gold}" };
            }
            else if (ThisIsSpell)
            {
                int count = (ThisIsSpell ? protagonist.Spells.Where(x => x == Head).Count() : 0);
                string line = count > 0 ? $" ({count} шт)" : String.Empty;
                return new List<string> { $"{Head}{line}" };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery}  выносливость {enemy.Endurance}");

            return enemies;
        }

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool goodLuck = (firstDice + secondDice) <= protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {luckLine} {protagonist.Luck}" };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (protagonist.Luck > 2)
            {
                protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Get()
        {
            if (ThisIsSpell && (protagonist.SpellSlots >= 1))
            {
                protagonist.Spells.Add(Head);
                protagonist.SpellSlots -= 1;
            }
            else if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease()
        {
            protagonist.Spells.Remove(Head);
            protagonist.SpellSlots += 1;

            return new List<string> { "RELOAD" };
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

                fight.Add($"BOLD|Заклятье слабости ослабляет вашего противника: " +
                    $"{FightEnemies[0].Name} теперь имеет ловкость " +
                    $"{FightEnemies[0].Mastery} вместо {oldEnemyMastery}");

                fight.Add(String.Empty);
            }

            if (SpellActivate["ЗАКЛЯТИЕ КОПИИ"])
            {
                SpellActivate["ЗАКЛЯТИЕ КОПИИ"] = false;

                Character enemyCopy = FightEnemies[0].Clone();
                enemyCopy.Name += "-копия";

                bool copyWin = Fights.Win(ref fight, ref round, ref enemyCopy,
                    ref FightEnemies, ref enemyWounds, StrengthPenlty, WoundsToWin, RoundsToWin,
                    ExtendedDamage, copyFight: true);

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
                {
                    fight.Add("BOLD|BAD|Копия проиграла, дальше сражаться придётся вам");
                }

                fight.Add(String.Empty);
            }

            int oldMastery = protagonist.Mastery;

            if (SpellActivate["ЗАКЛЯТИЕ СИЛЫ"])
            {
                SpellActivate["ЗАКЛЯТИЕ СИЛЫ"] = false;

                protagonist.Mastery += 2;

                fight.Add($"BOLD|Заклятье Силы увеличивает ваше мастерство: " +
                    $"на время этого боя она равна {protagonist.Mastery}");

                fight.Add(String.Empty);
            }

            bool win = Fights.Win(ref fight, ref round, ref protagonist, ref FightEnemies,
                ref enemyWounds, StrengthPenlty, WoundsToWin, RoundsToWin, ExtendedDamage);

            protagonist.Mastery = oldMastery;

            fight.Add(String.Empty);
            fight.Add(Result(win, "Вы ПОБЕДИЛИ|Вы ПРОИГРАЛИ"));

            return fight;
        }

        public override bool IsHealingEnabled() =>
            protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) =>
            protagonist.Endurance += healingLevel;
    }
}

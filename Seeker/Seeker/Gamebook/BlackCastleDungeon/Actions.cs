using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
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
            $"Мастерство: {Character.Protagonist.Mastery}",
            $"Выносливость: {Character.Protagonist.Endurance}/{Character.Protagonist.MaxEndurance}",
            $"Удача: {Character.Protagonist.Luck}",
            $"Золото: {Character.Protagonist.Gold}"
        };

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (Character.Protagonist.Spells == null)
                return null;

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
                statusLines.Add($"{char.ToUpper(spell[0]) + spell.Substring(1)}: {currentSpells[spell]}");

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            bool healing = Character.Protagonist.Spells.Contains("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ");
            bool wounded = Character.Protagonist.Endurance < Character.Protagonist.MaxEndurance;

            if (healing && wounded)
                staticButtons.Add("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ");

            foreach (string spell in Constants.StaticSpells)
            {
                bool spellAvailable = Character.Protagonist.Spells.Contains(spell) && !SpellActivate[spell];

                if (Fights.ParagraphWith(spell) && spellAvailable)
                    staticButtons.Add(spell);
            }

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            Character.Protagonist.Spells.Remove(action);

            if (action.Contains("ИСЦЕЛЕНИЯ"))
                Character.Protagonist.Endurance += 8;

            if (Constants.StaticSpells.Contains(action))
                SpellActivate[action] = true;

            return true;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledSpellAdd = ThisIsSpell && (Character.Protagonist.SpellSlots <= 0) && !secondButton;
            bool disabledSpellRemove = ThisIsSpell && !Character.Protagonist.Spells.Contains(Head) && secondButton;
            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Gold < Price);

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
                return int.Parse(option.Split('=')[1]) <= Character.Protagonist.Gold;
            }
            else if (option.Contains("ЗАКЛЯТИЕ"))
            {
                return Character.Protagonist.Spells.Contains(option);
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
                int count = (ThisIsSpell ? Character.Protagonist.Spells.Where(x => x == Head).Count() : 0);
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

            bool goodLuck = (firstDice + secondDice) <= Character.Protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {luckLine} {Character.Protagonist.Luck}" };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            Game.Buttons.Disable(goodLuck,
                "Повезло, Дверь сломана", $"Не повезло, {Output.Constants.GAMEOVER_TEXT}");

            return luckCheck;
        }

        public List<string> Get()
        {
            if (ThisIsSpell && (Character.Protagonist.SpellSlots >= 1))
            {
                Character.Protagonist.Spells.Add(Head);
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

        public List<string> Decrease()
        {
            Character.Protagonist.Spells.Remove(Head);
            Character.Protagonist.SpellSlots += 1;

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

            int oldMastery = Character.Protagonist.Mastery;

            if (SpellActivate["ЗАКЛЯТИЕ СИЛЫ"])
            {
                SpellActivate["ЗАКЛЯТИЕ СИЛЫ"] = false;

                Character.Protagonist.Mastery += 2;

                fight.Add($"BOLD|Заклятье Силы увеличивает ваше мастерство: " +
                    $"на время этого боя она равна {Character.Protagonist.Mastery}");

                fight.Add(String.Empty);
            }

            Character protagonist = Character.Protagonist;

            bool win = Fights.Win(ref fight, ref round, ref protagonist, ref FightEnemies,
                ref enemyWounds, StrengthPenlty, WoundsToWin, RoundsToWin, ExtendedDamage);

            Character.Protagonist.Mastery = oldMastery;

            fight.Add(String.Empty);
            fight.Add(Result(win, "Вы ПОБЕДИЛИ|Вы ПРОИГРАЛИ"));

            return fight;
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Endurance < Character.Protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Endurance += healingLevel;
    }
}

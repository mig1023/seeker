using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Services
    {
        public static bool IsProtagonist(string name) =>
            name == Character.Protagonist.Name;

        public static Character FindEnemyIn(List<Character> Enemies) =>
            Enemies.Where(e => e.Endurance > 0).FirstOrDefault();

        public static Character FindEnemy(Character fighter, List<Character> Allies, List<Character> Enemies)
        {
            if (Allies.Contains(fighter))
                return FindEnemyIn(Enemies);

            else if (Enemies.Contains(fighter))
                return FindEnemyIn(Allies);

            else
                return null;
        }

        public static Dictionary<string, bool> GetSpecialRules(Character attacker, Character defender, int round)
        {
            Dictionary<string, bool> specialRules = new Dictionary<string, bool>();

            specialRules["firstStrike"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike);
            specialRules["powerfulStrike"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.PowerfulStrike);
            specialRules["ignoreReaction"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreReaction);
            specialRules["ignoreFirstStrike"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreFirstStrike);
            specialRules["poisonBlade"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.PoisonBlade);

            specialRules["enemyFirstStrike"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike) && (round <= 3);
            specialRules["enemyIgnoreFirstStrike"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreFirstStrike);
            specialRules["enemyIgnorePowerfulStrike"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.IgnorePowerfulStrike);
            specialRules["enemyReaction"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.Reaction);
            specialRules["enemyTotalProtection"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.TotalProtection);

            specialRules["extendedDamage"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.ExtendedDamage);

            if (specialRules["ignoreReaction"])
                specialRules["enemyReaction"] = false;

            if (specialRules["ignoreFirstStrike"])
                specialRules["enemyFirstStrike"] = false;

            if (specialRules["enemyIgnoreFirstStrike"])
                specialRules["firstStrike"] = false;

            if (specialRules["enemyIgnorePowerfulStrike"])
                specialRules["powerfulStrike"] = false;

            specialRules["aggressive"] = attacker.FightStyle == Character.FightStyles.Aggressive;
            specialRules["defensive"] = attacker.FightStyle == Character.FightStyles.Defensive;
            specialRules["fullback"] = attacker.FightStyle == Character.FightStyles.Fullback;


            specialRules["aggressiveEnemy"] = defender.FightStyle == Character.FightStyles.Aggressive;
            specialRules["defensiveEnemy"] = defender.FightStyle == Character.FightStyles.Defensive;
            specialRules["fullbackEnemy"] = defender.FightStyle == Character.FightStyles.Fullback;

            return specialRules;
        }

        public static Character.FightStyles ChangeFightStyle(string motivation, ref List<string> fight,
            string direction, Character.FightStyles newFightStyles)
        {
            bool goodDirectionUp = ((direction == "upTo") &&
                ((int)Character.Protagonist.FightStyle < (int)newFightStyles));

            bool goodDirectionDown = ((direction == "downTo") &&
                ((int)Character.Protagonist.FightStyle > (int)newFightStyles));

            if (goodDirectionUp || goodDirectionDown)
            {
                Character.Protagonist.FightStyle = newFightStyles;
                fight.Add(String.Empty);
                fight.Add($"GRAY|{motivation} Меняем стиль боя на {Constants.FightStyles()[newFightStyles]}");
            }

            return newFightStyles;
        }

        public static Character.FightStyles ChooseFightStyle(ref List<string> fight,
            Dictionary<string, List<int>> AttackStory, List<Character> Enemies)
        {
            if (Character.Protagonist.Endurance < (Character.Protagonist.MaxEndurance / 2))
                return ChangeFightStyle("Дело дрянь!", ref fight, "downTo", Character.FightStyles.Fullback);

            int enemyCount = Enemies.Where(e => e.Endurance > 0).Count();

            if (enemyCount > 2)
                return ChangeFightStyle("Ох, сколько их набежало!", ref fight, "downTo", Character.FightStyles.Fullback);
            else if (enemyCount > 1)
                return ChangeFightStyle("Надо аккуратно бить их по очереди!", ref fight, "downTo", Character.FightStyles.Defensive);

            List<int> story = AttackStory[Character.Protagonist.Name];

            int fightBalance = 0;
            int storyCount = (story.Count > 3 ? 3 : story.Count);

            for (int i = 1; i <= storyCount; i++)
                fightBalance += story[story.Count - i];

            if (fightBalance < -1)
                return ChangeFightStyle("Дела неважно заладились...", ref fight, "downTo", Character.FightStyles.Defensive);
            else if (fightBalance > 3)
                return ChangeFightStyle("Раздаю люлей!!", ref fight, "upTo", Character.FightStyles.Aggressive);
            else
                return ChangeFightStyle("Надо потихоньку.", ref fight, "upTo", Character.FightStyles.Counterattacking);
        }

        public static int InitiativeAndDices(Character character, out string line)
        {
            Game.Dice.DoubleRoll(out int firstRoll, out int secondRoll);
            int initiative = firstRoll + secondRoll + character.Initiative;

            line = $"{character.Initiative} + " +
                $"{Game.Dice.Symbol(firstRoll)} + " +
                $"{Game.Dice.Symbol(secondRoll)}, итого {initiative}";

            return initiative;
        }

        public static void OutputInitiative(ref List<string> fight, List<Character> FightEnemies, List<Character> FightOrder,
            string protagonistLine, string enemyLine, bool reverse = false, bool special = false)
        {
            string fisrtTemplate = (special ? "{0} (особый приём Первый удар)" : "{0} (инициатива {1})");

            if (!reverse)
            {
                fight.Add(String.Format(fisrtTemplate, Character.Protagonist.Name, protagonistLine));
                fight.Add(String.Format("{0} (инициатива {1})", FightEnemies[0].Name, enemyLine));

                FightOrder.Add(FightEnemies[0]);
            }
            else
            {
                fight.Add(String.Format(fisrtTemplate, FightEnemies[0].Name, enemyLine));
                fight.Add(String.Format("{0} (инициатива {1})", Character.Protagonist.Name, protagonistLine));

                FightOrder.Insert(0, FightEnemies[0]);
            }
        }

        public static string Add(bool condition, string line) => condition ? line : String.Empty;

        public static int Attack(Character attacker, Character defender, ref List<string> fight, List<Character> Allies,
            ref Dictionary<string, int> WoundsCount, ref Dictionary<string, List<int>> AttackStory, int round, int coherenceIndex,
            int coherenceValue, out bool reactionSuccess, bool supplAttack = false)
        {
            reactionSuccess = false;

            const int SUCCESSFUL_ATTACK = 3, WOUND = -3, SUCCESSFUL_BLOCK = 1, FAIL_ATTACK = -1;

            Game.Dice.DoubleRoll(out int firstRoll, out int secondRoll);
            int attackStrength = firstRoll + secondRoll + attacker.Attack;

            Dictionary<string, bool> specialRules = GetSpecialRules(attacker, defender, round);

            int coherence = (coherenceIndex * coherenceValue);
            bool coherenceBonus = !Allies.Contains(attacker) && (coherenceIndex >= 1) && !specialRules["enemyTotalProtection"];

            if (specialRules["enemyFirstStrike"])
                attackStrength -= 1;

            if (coherenceBonus)
                attackStrength += coherence;

            if (specialRules["aggressive"])
                attackStrength += 1;

            if (specialRules["defensive"] || specialRules["fullback"])
                attackStrength -= 1;

            bool success = false;

            if (specialRules["firstStrike"] && (round == 1))
            {
                fight.Add("Первая атака (особый приём)");
                success = true;
            }
            else
            {
                string bonuses = String.Empty;

                bonuses += Add(specialRules["enemyFirstStrike"], " - 1 за Первую атаку (особый приём) противника");
                bonuses += Add(specialRules["aggressive"], " + 1 за Агрессивный стиль боя");
                bonuses += Add(specialRules["defensive"], " - 1 за Оборонительный стиль боя");
                bonuses += Add(specialRules["fullback"], " - 1 за Глухую оборону");

                string sign = coherence > 0 ? "+" : "-";
                string coherenceTemplate = $" {sign} {Math.Abs(coherence)} за Слаженность";
                string bonusLine = coherenceBonus ? coherenceTemplate : String.Empty;

                fight.Add($"Мощность удара:" +
                    $"{Game.Dice.Symbol(firstRoll)} + " +
                    $"{Game.Dice.Symbol(secondRoll)} + " +
                    $"{attacker.Attack}{bonuses}{bonusLine} = {attackStrength}");

                bool defenceBonus = defender.SpecialTechnique.Contains(Character.SpecialTechniques.TotalProtection);
                int defence = (defender.Defence + (defenceBonus ? 1 : 0) + 
                    (specialRules["defensiveEnemy"] ? 1 : 0) +
                    (specialRules["fullbackEnemy"] ? 2 : 0) - 
                    (specialRules["aggressiveEnemy"] ? 1 : 0));

                success = attackStrength > defence;

                bonuses = String.Empty;

                bonuses += Add(defenceBonus, " + 1 за Веерную защиту (особый приём)");
                bonuses += Add(specialRules["aggressiveEnemy"], " - 1 за Агрессивный стиль боя");
                bonuses += Add(specialRules["defensiveEnemy"], " + 1 за Оборонительный стиль боя");
                bonuses += Add(specialRules["fullbackEnemy"], " + 2 за Глухую оборону");

                string comparison = Game.Services.Сomparison(defence, attackStrength);

                fight.Add($"Защита: {defender.Defence}{bonuses} — {comparison} Мощности удара");
            }

            if (success)
            {
                if (!supplAttack)
                    WoundsCount[defender.Name] += 1;

                if (specialRules["enemyReaction"] && (WoundsCount[defender.Name] == 3))
                {
                    string type = Allies.Contains(defender) ? "GOOD" : "BAD";
                    fight.Add($"{type}|Уклонение от атаки благодаря Реакции (особый приём)");
                    reactionSuccess = true;
                    WoundsCount[defender.Name] = 0;

                    return 0;
                }

                if (specialRules["extendedDamage"] && !supplAttack)
                    defender.Endurance -= 3;
                else if (supplAttack)
                    defender.Endurance -= 1;
                else
                    defender.Endurance -= 2;

                if (specialRules["firstStrike"] && (round == 1))
                {
                    fight.Add("+2 дополнительный урон от Первого удара (особый приём)");
                    defender.Endurance -= 2;
                }

                if (specialRules["powerfulStrike"] && (round < 3))
                {
                    fight.Add("+3 дополнительный урон от Мощного выпада (особый приём)");
                    defender.Endurance -= 3;
                }

                string defenderName = (IsProtagonist(defender.Name) ? "Вы ранены" : String.Format("{0} ранен", defender.Name));

                if (defender.Endurance > 0)
                    defenderName += String.Format(" (осталось жизней: {0})", defender.Endurance);

                fight.Add(String.Format("{0}|{1}", (Allies.Contains(defender) ? "BAD" : "GOOD"), defenderName));

                AttackStory[attacker.Name].Add(SUCCESSFUL_ATTACK);
                AttackStory[defender.Name].Add(WOUND);

                return (Allies.Contains(defender) ? 0 : 1);
            }
            else
            {
                fight.Add(String.Format("{0}|Атака отбита", (Allies.Contains(defender) ? "GOOD" : "BAD")));

                if (specialRules["powerfulStrike"] && (round < 3))
                {
                    fight.Add("Урон всё равно нанесён от Мощного выпада (особый приём)");
                    defender.Endurance -= 3;
                }

                if (specialRules["poisonBlade"] && (defender.MaxEndurance > defender.Endurance))
                {
                    fight.Add("Урон нанесён от яда отравленного клинка");
                    defender.Endurance -= 1;
                }

                AttackStory[attacker.Name].Add(FAIL_ATTACK);
                AttackStory[defender.Name].Add(SUCCESSFUL_BLOCK);

                return 0;
            }
        }
    }
}

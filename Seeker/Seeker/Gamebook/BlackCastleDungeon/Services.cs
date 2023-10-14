using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Services
    {
        public static bool ParagraphWithFight(string spell)
        {
            if (Game.Data.CurrentParagraph.Actions == null)
                return false;

            int optionsCount = Game.Data.CurrentParagraph.Options
                .Where(x => x.Text.ToUpper().Contains(spell))
                .Count();

            if (optionsCount > 0)
                return false;

            foreach (Actions action in Game.Data.CurrentParagraph.Actions)
            {
                if (action.Enemies != null)
                    return true;
            }

            return false;
        }

        public static bool WinInFight(ref List<string> fight, ref int round, ref Character protagonist,
            ref List<Character> FightEnemies, ref int enemyWounds, int StrengthPenlty, int WoundsToWin,
            int RoundsToWin, int ExtendedDamage, bool copyFight = false)
        {
            if (copyFight)
            {
                fight.Add($"BOLD|Вместо вас будет сражаться: {protagonist.Name}");
                fight.Add(String.Empty);
            }

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    fight.Add($"{enemy.Name} (выносливость {enemy.Endurance})");

                    if (copyFight)
                        fight.Add($"{protagonist.Name} (выносливость {protagonist.Endurance})");

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int firstprotagonistRoll, out int secondprotagonistRoll);
                        protagonistHitStrength = firstprotagonistRoll + secondprotagonistRoll + protagonist.Mastery;

                        string penalty = String.Empty;

                        if (StrengthPenlty > 0)
                        {
                            protagonistHitStrength -= StrengthPenlty;
                            penalty = $" - {StrengthPenlty} по обстоятельствам";
                        }

                        string whofigth = copyFight ? "удара копии" : "вашего удара";

                        fight.Add($"Сила {whofigth}: {Game.Dice.Symbol(firstprotagonistRoll)} + " +
                            $"{Game.Dice.Symbol(secondprotagonistRoll)} + " +
                            $"{protagonist.Mastery}{penalty} = {protagonistHitStrength}");
                    }

                    Game.Dice.DoubleRoll(out int firstEnemyRoll, out int secondEnemyRoll);
                    int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Mastery;

                    fight.Add($"Сила удара врага: {Game.Dice.Symbol(firstEnemyRoll)} + " +
                        $"{Game.Dice.Symbol(secondEnemyRoll)} + {enemy.Mastery} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");
                        enemy.Endurance -= 2;

                        enemyWounds += 1;

                        bool enemyLost = FightEnemies.Where(x => x.Endurance > 0).Count() == 0;

                        if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                            return true;
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог ранить");
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        string copyName = copyFight ? "копию" : "вас";
                        fight.Add($"BAD|{enemy.Name} ранил {copyName}");

                        protagonist.Endurance -= (ExtendedDamage > 0 ? ExtendedDamage : 2);

                        if (protagonist.Endurance <= 0)
                            return false;
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

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
    }
}

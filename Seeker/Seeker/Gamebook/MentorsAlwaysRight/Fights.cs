using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Fights
    {
        public static void WinFightEnding(ref List<string> fight, int wounded, bool Poison, bool Regeneration)
        {
            if (IsPoisonedBlade())
                Game.Option.Trigger("PoisonedBlade", remove: true);

            if (Poison)
            {
                if (wounded > 3)
                {
                    Character.Protagonist.Hitpoints /= 2;

                    fight.Add(String.Empty);
                    fight.Add("BAD|Из-за яда вы теряете половину оставшихся жизней...");
                }

                fight.Add(String.Empty);

                if (Character.Protagonist.Specialization == Character.SpecializationType.Thrower)
                {
                    fight.Add("BOLD|Вы смазали ядом свои метательные ножи, " +
                        "теперь они будут отнимать у противника не 3, а 4 жизни");
                }
                else
                {
                    fight.Add("BOLD|Вы смазали ядом свой меч, в следующем бою " +
                        "он будет отнимать у противника по 5 жизней");
                }

                Game.Option.Trigger("PoisonedBlade");
            }
            else if (Regeneration)
            {
                int hitpointsBonus = Game.Dice.Roll();

                Character.Protagonist.Hitpoints += hitpointsBonus;

                fight.Add(String.Empty);
                fight.Add($"GOOD|Благодаря крови, попавшей на вас, вы восстановили {hitpointsBonus} жизни");
            }

            if (Game.Option.IsTriggered("Rabies"))
            {
                Character.Protagonist.Strength -= 1;

                fight.Add(String.Empty);
                fight.Add("BAD|Вы дополнительно теряете 1 Силу за невылеченную болезнь...");
            }
        }

        public static bool EnemyLostFight(List<Character> FightEnemies, ref List<string> fight, int WoundsLimit,
            bool Invincible, bool Poison, bool Regeneration, int wounded = 0)
        {

            if (FightEnemies.Where(x => x.Hitpoints > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() > 0)
                return false;
            
            if (Invincible)
                return false;

            fight.Add(String.Empty);
            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

            WinFightEnding(ref fight, wounded, Poison, Regeneration);

            return true;
        }

        public static List<string> LostFight(List<string> fight)
        {
            fight.Add(String.Empty);
            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

            return fight;
        }

        public static bool IsPoisonedBlade() =>
            Game.Option.IsTriggered("PoisonedBlade")
            &&
            (Character.Protagonist.Specialization != Character.SpecializationType.Thrower);

        public static bool IsMagicBlade() =>
            Game.Option.IsTriggered("MagicSword");

        public static int HitWounds(ref List<string> fight, int wound, bool wolf)
        {
            if (!wolf)
                return wound;

            int wolfWound = wound / 2;

            string woundWolfLine = Game.Services.CoinsNoun(wolfWound, "Силу", "Силы", "Сил");
            string woundLine = Game.Services.CoinsNoun(wound, "Силы", "Сил", "Сил");

            fight.Add($"Форма волка защищает вас и вы теряете {wolfWound} " +
                $"{woundWolfLine} вместо {wound} {woundLine}!");

            return wolfWound;
        }
    }
}

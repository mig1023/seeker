using System;
using System.Collections.Generic;
using System.Linq;
using static Seeker.Gamebook.YounglingTournament.Character;

namespace Seeker.Gamebook.YounglingTournament
{
    class Fights
    {
        public static Character.SwordTypes GetSwordType()
        {
            int max = Character.Protagonist.SwordTechniques[Character.SwordTypes.Decisiveness];
            Character.SwordTypes swordTechniques = Character.SwordTypes.Decisiveness;

            foreach (Character.SwordTypes swordType in Character.Protagonist.SwordTechniques.Keys)
            {
                if (Character.Protagonist.SwordTechniques[swordType] <= 0)
                    continue;

                int swordResult = SwordSkills(swordType, out string _);

                if (swordResult > max)
                {
                    max = swordResult;
                    swordTechniques = swordType;
                }
            }

            return swordTechniques;
        }

        public static int SwordSkills(Character.SwordTypes skill, out string detail)
        {
            int rang = Character.Protagonist.SwordTechniques[skill];

            switch (skill)
            {
                case Character.SwordTypes.Elasticity:
                    detail = String.Format("4 + {0} ранг", rang);
                    return 4 + rang;

                case Character.SwordTypes.Rivalry:
                    detail = String.Format("4 + (2 x {0} ранг)", rang);
                    return 4 + (2 * rang);

                case Character.SwordTypes.Perseverance:
                    detail = String.Format("8 + {0} ранг", rang);
                    return 8 + rang;

                case Character.SwordTypes.Aggressiveness:
                    detail = String.Format("12 + (2 x {0} ранг)", rang);
                    return 12 + (2 * rang);

                case Character.SwordTypes.Confidence:
                    detail = String.Format("12 + (3 x {0} ранг)", rang);
                    return 12 + (3 * rang);

                case Character.SwordTypes.Vaapad:
                    detail = String.Format("12 + (4 x {0} ранг)", rang);
                    return 12 + (4 * rang);

                case Character.SwordTypes.JarKai:
                    detail = String.Format("12 + (3 x {0} ранг)", rang);
                    return 12 + (3 * rang);

                default:
                case Character.SwordTypes.Decisiveness:
                    detail = String.Format("1 x {0} ранг", rang);
                    return rang;
            }
        }

        public static bool AdditionalAttack(ref List<string> fight, Character enemy, string head, string body)
        {
            fight.Add(String.Format("BOLD|{0}", head));

            int strikeWound = Game.Dice.Roll();

            fight.Add(String.Format("{0}: {1}", body, Game.Dice.Symbol(strikeWound)));

            enemy.Hitpoints -= strikeWound;

            fight.Add(String.Format("GOOD|Вы ранили {0}, он потерял {1} ед.выносливости (осталось {2})",
                enemy.Name, strikeWound, enemy.Hitpoints));

            Character.Protagonist.Thrust += 1;

            fight.Add("GOOD|BOLD|Вы наносите укол противнику!");

            return true;
        }

        public static void SpeedFightHitpointsLoss(ref List<string> fight, Character character)
        {
            bool isProtagonist = (character == Character.Protagonist);

            int technique = (isProtagonist ? Character.Protagonist.ForceTechniques[ForcesTypes.Speed] : character.Speed);
            int wound = (5 - technique);
            character.Hitpoints -= wound;

            if (isProtagonist)
            {
                fight.Add(String.Format("BAD|Из-за применения Скорости Силы вы теряете {0} " +
                    "ед.выносливости (осталось {1})", wound, Character.Protagonist.Hitpoints));
            }
            else
            {
                fight.Add(String.Format("GOOD|Из-за применения Скорости Силы {0} теряет {1} " +
                    "ед.выносливости (осталось {2})", character.Name, wound, character.Hitpoints));
            }
        }

        public static bool SpeedActivation(ref List<string> fight,
            ref bool speedActivate, List<Character> EnemiesList)
        {
            fight.Add(String.Format("BOLD|Вы активируете Скорость Силы {0} ранга!",
                Character.Protagonist.ForceTechniques[ForcesTypes.Speed]));

            foreach (Character enemy in EnemiesList)
            {
                fight.Add(String.Format("{0} активирует Скорость Силы {1} ранга.",
                    enemy.Name, enemy.Speed));
            }

            speedActivate = true;

            return false;
        }

        public static bool UseForcesInFight(ref List<string> fight, ref bool speedActivate,
            List<Character> EnemiesList, bool SpeedActivate, bool WithoutTechnique)
        {
            Character target = EnemiesList.Where(x => x.Hitpoints > 0).FirstOrDefault();

            if (SpeedActivate && !speedActivate)
                return SpeedActivation(ref fight, ref speedActivate, EnemiesList);

            if (WithoutTechnique || speedActivate)
                return false;

            int forceTechniques = 0;

            for (int i = 0; i < Character.Protagonist.ForceTechniquesOrder.Count; i++)
            {
                if (Character.Protagonist.ForceTechniquesOrder[i] != 0)
                {
                    forceTechniques = Character.Protagonist.ForceTechniquesOrder[i];
                    Character.Protagonist.ForceTechniquesOrder[i] = 0;

                    break;
                }
            }

            if (forceTechniques == 0)
                return SpeedActivation(ref fight, ref speedActivate, EnemiesList);

            switch (forceTechniques)
            {
                case 1:
                    fight.Add("BOLD|Вы применяете Толчок Силы!");

                    int pushWound = Game.Dice.Roll();
                    target.Hitpoints -= (pushWound + Character.Protagonist.ForceTechniques[ForcesTypes.Push]);

                    fight.Add(String.Format("GOOD|{0} теряет {1} + {2} (за ранг техники) ед.выносливости (осталось {3})",
                        target.Name, Game.Dice.Symbol(pushWound),
                        Character.Protagonist.ForceTechniques[ForcesTypes.Push], target.Hitpoints));

                    return true;

                case 2:
                    fight.Add("BOLD|Вы применяете Прыжок Силы!");

                    int jump = Game.Dice.Roll();
                    int technique = Character.Protagonist.ForceTechniques[ForcesTypes.Jump];
                    bool success = (jump + technique) > 6;

                    fight.Add(String.Format("Прыжок: {0} + {1} {2} 6 - {3}",
                        Game.Dice.Symbol(jump), technique, Game.Services.Сomparison(jump + technique, 6),
                        (success ? "прыжок удался!" : "прыжок не получился...")));

                    if (success)
                    {
                        target.Hitpoints -= jump;

                        fight.Add(String.Format("GOOD|{0} теряет {1} ед.выносливости (осталось {2})",
                            target.Name, jump, target.Hitpoints));

                        Character.Protagonist.Thrust += 1;

                        fight.Add("GOOD|BOLD|Вы наносите укол противнику!");
                    }

                    return true;

                case 3:
                    fight.Add("BOLD|Вы применяете Удушение Силы!");

                    Character.Protagonist.DarkSide += 50;
                    Game.Option.Trigger("Темная сторона");
                    fight.Add("Вы получаете +50 к очкам Тёмной стороны и ключевое слово 'Тёмная сторона'.");

                    int suffWound = Game.Dice.Roll();
                    target.Hitpoints -= suffWound;

                    fight.Add(String.Format("GOOD|{0} теряет {1} ед.выносливости (осталось {2})",
                        target.Name, Game.Dice.Symbol(suffWound), target.Hitpoints));

                    Character.Protagonist.Thrust += 1;

                    fight.Add("GOOD|BOLD|Вы наносите укол противнику!");

                    return true;

                default:
                    return false;
            }
        }

        public static string GetSwordSkillName(SwordTypes swordTechniques, int? rang = null)
        {
            SwordTypes currectSwordTechniques = GetSwordType();

            string skillName = Constants.SwordSkillsNames()[currectSwordTechniques];
            int skillRang = rang ?? Character.Protagonist.SwordTechniques[currectSwordTechniques];

            return String.Format("{0} ({1} ранг)", skillName, skillRang);
        }
    }
}

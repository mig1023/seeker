using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.OctopusIsland
{
    class Fights
    {
        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Hitpoint > 0).Count() == 0;

        public static void SaveCurrentWarriorHitPoints()
        {
            if (String.IsNullOrEmpty(Character.Protagonist.Name))
                return;

            if (Character.Protagonist.Name == "Тибо")
            {
                Character.Protagonist.ThibautHitpoint = Character.Protagonist.Hitpoint;
            }
            else if (Character.Protagonist.Name == "Ксолотл")
            {
                Character.Protagonist.XolotlHitpoint = Character.Protagonist.Hitpoint;
            }
            else if (Character.Protagonist.Name == "Серж")
            {
                Character.Protagonist.SergeHitpoint = Character.Protagonist.Hitpoint;
            }
            else
            {
                Character.Protagonist.SouhiHitpoint = Character.Protagonist.Hitpoint;
            }
        }

        private static void ShowCurrentWarrior(ref List<string> fight, bool start)
        {
            if (!start)
                fight.Add(String.Empty);

            fight.Add($"HEAD|BOLD|*** В БОЙ ВСТУПАЕТ {Character.Protagonist.Name.ToUpper()} ***");
            
            if (start)
                fight.Add(String.Empty);
        }
        
        public static bool SetCurrentWarrior(ref List<string> fight, bool start = false)
        {
            if ((Character.Protagonist.Hitpoint > 3) && !start)
                return true;

            SaveCurrentWarriorHitPoints();

            if (Character.Protagonist.ThibautHitpoint > 3)
            {
                Character.Protagonist.Name = "Тибо";
                Character.Protagonist.Skill = Character.Protagonist.ThibautSkill;
                Character.Protagonist.Hitpoint = Character.Protagonist.ThibautHitpoint;
            }
            else if (Character.Protagonist.XolotlHitpoint > 3)
            {
                Character.Protagonist.Name = "Ксолотл";
                Character.Protagonist.Skill = Character.Protagonist.XolotlSkill;
                Character.Protagonist.Hitpoint = Character.Protagonist.XolotlHitpoint;
            }
            else if (Character.Protagonist.SergeHitpoint > 3)
            {
                Character.Protagonist.Name = "Серж";
                Character.Protagonist.Skill = Character.Protagonist.SergeSkill;
                Character.Protagonist.Hitpoint = Character.Protagonist.SergeHitpoint;
            }
            else if (Character.Protagonist.SouhiHitpoint > 3)
            {
                Character.Protagonist.Name = "Суи";
                Character.Protagonist.Skill = Character.Protagonist.SouhiSkill;
                Character.Protagonist.Hitpoint = Character.Protagonist.SouhiHitpoint;
            }
            else
            {
                return false;
            }

            ShowCurrentWarrior(ref fight, start);
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.OctopusIsland
{
    class Services
    {
        private static Character protagonist = Character.Protagonist;

        public static int LifeGivingOintmentFor(int protagonistHitpoint)
        {
            while ((protagonist.LifeGivingOintment > 0) && (protagonistHitpoint < 20))
            {
                protagonist.LifeGivingOintment -= 1;
                protagonistHitpoint += 1;
            }

            return protagonistHitpoint;
        }

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Hitpoint > 0).Count() == 0;

        public static void SaveCurrentWarriorHitPoints()
        {
            if (String.IsNullOrEmpty(protagonist.Name))
                return;

            if (protagonist.Name == "Тибо")
            {
                protagonist.ThibautHitpoint = protagonist.Hitpoint;
            }
            else if (Character.Protagonist.Name == "Ксолотл")
            {
                protagonist.XolotlHitpoint = protagonist.Hitpoint;
            }
            else if (Character.Protagonist.Name == "Серж")
            {
                protagonist.SergeHitpoint = protagonist.Hitpoint;
            }
            else
            {
                protagonist.SouhiHitpoint = protagonist.Hitpoint;
            }
        }

        private static void ShowCurrentWarrior(ref List<string> fight, bool start)
        {
            if (!start)
                fight.Add(String.Empty);

            fight.Add($"HEAD|BOLD|*** В БОЙ ВСТУПАЕТ {protagonist.Name.ToUpper()} ***");
            
            if (start)
                fight.Add(String.Empty);
        }
        
        public static bool SetCurrentWarrior(ref List<string> fight, bool start = false)
        {
            if ((protagonist.Hitpoint > 3) && !start)
                return true;

            SaveCurrentWarriorHitPoints();

            if (protagonist.ThibautHitpoint > 3)
            {
                protagonist.Name = "Тибо";
                protagonist.Skill = protagonist.ThibautSkill;
                protagonist.Hitpoint = protagonist.ThibautHitpoint;
            }
            else if (protagonist.XolotlHitpoint > 3)
            {
                protagonist.Name = "Ксолотл";
                protagonist.Skill = protagonist.XolotlSkill;
                protagonist.Hitpoint = protagonist.XolotlHitpoint;
            }
            else if (protagonist.SergeHitpoint > 3)
            {
                protagonist.Name = "Серж";
                protagonist.Skill = protagonist.SergeSkill;
                protagonist.Hitpoint = protagonist.SergeHitpoint;
            }
            else if (protagonist.SouhiHitpoint > 3)
            {
                protagonist.Name = "Суи";
                protagonist.Skill = protagonist.SouhiSkill;
                protagonist.Hitpoint = protagonist.SouhiHitpoint;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.BloodfeudOfAltheus.Character();

        public string Name { get; set; }

        private int _strength;
        public int Strength
        {
            get => _strength;
            set
            {
                if (value < 0)
                    _strength = 0;
                else
                    _strength = value;
            }
        }

        private int _defence;
        public int Defence
        {
            get => _defence;
            set
            {
                if (value < 0)
                    _defence = 0;
                else
                    _defence = value;
            }
        }

        private int _glory;
        public int Glory
        {
            get => _glory;
            set
            {
                if (value < 0)
                    _glory = 0;
                else
                    _glory = value;
            }
        }
        
        private int _shame;
        public int Shame
        {
            get => _shame;
            set
            {
                if (value < 0)
                    _shame = 0;
                else
                    _shame = value;
            }
        }

        private List<string> Weapons { get; set; }
        private List<string> Armour { get; set; } 

        public int Resurrection { get; set; }
        public string Patron { get; set; }
        public int NoIntuitiveSolutionPenalty { get; set; }

        public int Health { get; set; }

        public void Init()
        {
            Name = String.Empty;
            Strength = 4;
            Defence = 10;
            Glory = 7;
            Shame = 0;
            Resurrection = 0;
            Patron = "нет";
            NoIntuitiveSolutionPenalty = 0;

            Armour = new List<string>();
            Weapons = new List<string>();
            Weapons.Add("дубинка, 1, 0");
        }

        public Character Clone(bool lastWound = false)
        {
            return new Character() {

                Name = this.Name,
                Strength = this.Strength,
                Defence = this.Defence,
                Glory = this.Glory,
                Shame = this.Shame,
                Resurrection = this.Resurrection,
                Patron = this.Patron,
                NoIntuitiveSolutionPenalty = this.NoIntuitiveSolutionPenalty,

                Health = (lastWound ? 1 : 3),
            };
        }

        public string Save()
        {
            string weapons = String.Join(":", Weapons);
            string armours = String.Join(":", Weapons);

            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                Strength, Defence, Glory, Shame, armours, weapons, Patron, NoIntuitiveSolutionPenalty, Resurrection
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Defence = int.Parse(save[1]);
            Glory = int.Parse(save[2]);
            Shame = int.Parse(save[3]);
            Armour = save[4].Split(':').ToList();
            Weapons = save[5].Split(':').ToList();
            Patron = save[6];
            NoIntuitiveSolutionPenalty = int.Parse(save[7]);
            Resurrection = int.Parse(save[8]);
        }

        public void AddWeapons(string weapon) => Weapons.Add(weapon);

        public void GetWeapons(out string name, out int strength, out int defence)
        {
            name = "голые руки";
            strength = 0;
            defence = 0;

            foreach (string weapon in Weapons)
            {
                string[] weaponParams = weapon.Split(',');

                if (strength < int.Parse(weaponParams[1]))
                {
                    name = weaponParams[0];
                    strength = int.Parse(weaponParams[1]);
                    defence = int.Parse(weaponParams[2]);
                }
            }
        }

        public void AddArmour(string armour) => Armour.Add(armour);

        public void GetArmour(out int armourDefence, out string armourLine)
        {
            Dictionary<string, int> currentArmour = new Dictionary<string, int>();
            Dictionary<string, Dictionary<string, int>> allArmour = new Dictionary<string, Dictionary<string, int>>();

            int defence = 0;
            string defenceLine = String.Empty;

            GetWeapons(out string name, out int strength, out int weaponDefence);

            if (weaponDefence > 0)
            {
                defence += weaponDefence;
                defenceLine += String.Format(" + {0} {1}", weaponDefence, name);
            }

            foreach (string armour in Armour)
            {
                string[] armourParams = armour.Split(',');

                if (!allArmour.ContainsKey(armourParams[2]))
                    allArmour[armourParams[2]] = new Dictionary<string, int>();

                allArmour[armourParams[2]].Add(armourParams[0], int.Parse(armourParams[1]));
            }

            foreach (string armourType in allArmour.Keys)
            {
                KeyValuePair<string, int> armour = allArmour[armourType].FirstOrDefault(x => x.Value == allArmour[armourType].Values.Max());

                defence += armour.Value;
                defenceLine += String.Format(" + {0} {1}", armour.Value, armour.Key);
            }

            armourDefence = defence;
            armourLine = defenceLine;
        }
    }
}

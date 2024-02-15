using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, _strength, this);
        }

        private int _defence;
        public int Defence
        {
            get => _defence;
            set => _defence = Game.Param.Setter(value, _defence, this);
        }

        private int _glory;
        public int Glory
        {
            get => _glory;
            set => _glory = Game.Param.Setter(value, _glory, this);
        }
        
        private int _shame;
        public int Shame
        {
            get => _shame;
            set => _shame = Game.Param.Setter(value, _shame, this);
        }

        private List<string> Weapons { get; set; }
        private List<string> Armour { get; set; } 
        private List<string> FavorOfTheGods { get; set; }
        private List<string> DisfavorOfTheGods { get; set; }

        public int Resurrection { get; set; }
        public int BroochResurrection { get; set; }
        public string Patron { get; set; }
        public int Ichor { get; set; }
        
        public int Health { get; set; }

        public override void Init()
        {
            base.Init();

            Strength = 4;
            Defence = 10;
            Glory = 7;
            Shame = 0;
            Resurrection = 1;
            BroochResurrection = 0;
            Patron = "нет";
            Ichor = 0;

            Armour = new List<string>();
            Weapons = new List<string>();
            FavorOfTheGods = new List<string>();
            DisfavorOfTheGods = new List<string>();

            Weapons.Add("дубинка, 1, 0");
        }

        public Character Clone(int wound) => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Strength = this.Strength,
            Defence = this.Defence,
            Glory = this.Glory,
            Shame = this.Shame,
            Resurrection = this.Resurrection,
            BroochResurrection = this.Resurrection,
            Patron = this.Patron,
            Ichor = this.Ichor,

            Health = wound,
        };

        public override string Save()
        {
            string weapons = String.Join(":", Weapons).TrimEnd(':');
            string armours = String.Join(":", Armour).TrimEnd(':');
            string favor = String.Join(":", FavorOfTheGods).TrimEnd(':');
            string disfavor = String.Join(":", DisfavorOfTheGods).TrimEnd(':');

            return String.Join("|",
                Strength, Defence, Glory, Shame, armours, weapons, Patron, Resurrection,
                favor, disfavor, BroochResurrection, Ichor);
        }

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Defence = int.Parse(save[1]);
            Glory = int.Parse(save[2]);
            Shame = int.Parse(save[3]);
            Armour = save[4].Split(':').ToList();
            Weapons = save[5].Split(':').ToList();
            Patron = save[6];
            Resurrection = int.Parse(save[7]);
            FavorOfTheGods = save[8].Split(':').ToList();
            DisfavorOfTheGods = save[9].Split(':').ToList();
            BroochResurrection = int.Parse(save[10]);
            Ichor = int.Parse(save[11]);

            IsProtagonist = true;
        }

        public void FellIntoFavor(string godName, bool fellOut = false,
            bool indifferent = false, bool indifferentToAll = false)
        {
            if (fellOut)
            {
                DisfavorOfTheGods.Add(godName);
                FavorOfTheGods.RemoveAll(item => item == godName.Trim());
            }
            else if (indifferentToAll)
            {
                FavorOfTheGods.Clear();
                DisfavorOfTheGods.Clear();
            }
            else if (indifferent)
            {
                DisfavorOfTheGods.RemoveAll(item => item == godName.Trim());
            }
            else
            {
                FavorOfTheGods.Add(godName);
                DisfavorOfTheGods.RemoveAll(item => item == godName.Trim());
            }
        }

        public bool IsGodsFavor(string godName) =>
            FavorOfTheGods.Contains(godName);

        public bool IsGodsDisFavor(string godName) =>
            DisfavorOfTheGods.Contains(godName);

        public void AddWeapons(string weapon) =>
            Weapons.Add(weapon);

        public void GetWeapons(out string name, out int strength, out int defence)
        {
            name = "голые руки";
            strength = 0;
            defence = 0;

            foreach (string weapon in Weapons)
            {
                string[] weaponParams = weapon.Split(',');

                if (strength >= int.Parse(weaponParams[1]))
                    continue;

                name = weaponParams[0];
                strength = int.Parse(weaponParams[1]);
                defence = int.Parse(weaponParams[2]);
            }
        }

        public void AddArmour(string armour) =>
            Armour.Add(armour);

        public void GetArmour(out int armourDefence, out string armourLine, bool status = false)
        {
            int defence = 0;
            string defenceLine = String.Empty;

            if (!status)
            {
                GetWeapons(out string name, out int _, out int weaponDefence);

                if (weaponDefence > 0)
                {
                    defence += weaponDefence;
                    defenceLine += $" + {weaponDefence} {name}";
                }
            }

            List<ArmourAnnotation> armours = new List<ArmourAnnotation>();

            foreach (string armour in Armour)
            {
                if (String.IsNullOrEmpty(armour))
                    continue;

                List<string> armourParams = armour
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToList();

                armours.Add(new ArmourAnnotation
                {
                    Name = armourParams[0],
                    Level = int.Parse(armourParams[1]),
                    Type = armourParams[2],
                });
            }

            List<ArmourAnnotation> MaxArmours = armours
                .GroupBy(x => x.Type, (y, z) => z.Aggregate((a, b) => (b.Level > a.Level) ? b : a))
                .ToList();

            foreach (ArmourAnnotation actualArmour in MaxArmours)
            {
                defence += actualArmour.Level;

                if (status)
                    defenceLine += $"{actualArmour.Name}, ";
                else
                    defenceLine += $" + {actualArmour.Name} {actualArmour.Level}";
            }

            armourDefence = defence;
            armourLine = defenceLine.TrimEnd(new char[] {' ', ','});
        }
    }
}

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

        public int Armour { get; set; } 
        public string Patron { get; set; }

        public int Health { get; set; }

        public void Init()
        {
            Name = String.Empty;
            Strength = 4;
            Defence = 10;
            Glory = 7;
            Shame = 0;
            Armour = 0;
            Patron = "нет";

            Weapons = new List<string>();
            Weapons.Add("дубинка, 1, 0");
        }

        public Character Clone()
        {
            return new Character() {

                Name = this.Name,
                Strength = this.Strength,
                Defence = this.Defence,
                Glory = this.Glory,
                Shame = this.Shame,
                Armour = this.Armour,
                Patron = this.Patron,

                Health = 3,
            };
        }

        public string Save()
        {
            string weapons = String.Join(":", Weapons);

            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                Strength, Defence, Glory, Shame, Armour, weapons, Patron
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Defence = int.Parse(save[1]);
            Glory = int.Parse(save[2]);
            Shame = int.Parse(save[3]);
            Armour = int.Parse(save[4]);
            Weapons = save[5].Split(':').ToList();
            Patron = save[6];
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
    }
}

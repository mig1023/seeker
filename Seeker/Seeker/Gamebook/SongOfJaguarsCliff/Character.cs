using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SongOfJaguarsCliff
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _wounds;
        public int Wounds
        {
            get => _wounds;
            set => _wounds = Game.Param.Setter(value, _wounds, this);
        }

        public int Hitpoints { get; set; }

        public int Priority { get; set; }

        public string PriorityComment { get; set; }

        public int Distance { get; set; }

        public List<Weapon> Weapons { get; set; } 

        public Weapon CurrentWeapon { get; set; }

        public override void Init()
        {
            base.Init();

            Name = "Главный герой";
            Wounds = 0;
            Hitpoints = 6;
            Priority = 1;
            Weapons = new List<Weapon> { new Weapon("Дерринджер,4,1,0-50,1") };
            CurrentWeapon = null;

            Game.Healing.Add(name: "Перебинтовать рану", healing: 1, portions: 3);
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Wounds = this.Wounds,
            Hitpoints = this.Hitpoints,
            Weapons = new List<Weapon>(this.Weapons),
            CurrentWeapon = this.CurrentWeapon,
        };

        public override string Save()
        {
            string weapons = String.Join(";", Weapons.Select(x => x.Save()));
            return String.Join("|", Wounds, Hitpoints, weapons);
        }

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Wounds = int.Parse(save[0]);
            Hitpoints = int.Parse(save[1]);

            Weapons = new List<Weapon>();
            
            foreach (string weaponLine in save[1].Split(';'))
                Weapons.Add(new Weapon(weaponLine));

            Name = "Главный герой";
            Priority = 1;
            IsProtagonist = true;
        }
    }
}

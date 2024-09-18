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

        public List<Weapon> Weapons { get; set; } 

        public override void Init()
        {
            base.Init();

            Wounds = 0;
            Hitpoints = 6;
            Weapons = new List<Weapon> { new Weapon("Дерринджер,4,1,0-50,1") };
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Wounds = this.Wounds,
            Hitpoints = this.Hitpoints,
            Weapons = new List<Weapon>(this.Weapons),
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

            IsProtagonist = true;
        }
    }
}

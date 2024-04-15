using System;

namespace Seeker.Gamebook.BangkokSky
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int MartialArts { get; set; }
        public int Physical { get; set; }
        public int Driving { get; set; }
        public int Firearms { get; set; }
        public int Diplomacy { get; set; }
        public int ConcreteJungle { get; set; }
        public int Respect { get; set; }

        private int _wounds;
        public int Wounds
        {
            get => _wounds;
            set => _wounds = Game.Param.Setter(value, _wounds, this);
        }

        public int StatBonuses { get; set; }

        public override void Init()
        {
            base.Init();

            MartialArts = 0;
            Physical = 0;
            Driving = 0;
            Firearms = 0;
            Diplomacy = 0;
            ConcreteJungle = 0;
            Wounds = 0;
            Respect = 0;
            StatBonuses = 8;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MartialArts = this.MartialArts,
            Physical = this.Physical,
            Driving = this.Driving,
            Firearms = this.Firearms,
            Diplomacy = this.Diplomacy,
            ConcreteJungle = this.ConcreteJungle,
            Wounds = this.Wounds,
            Respect = this.Respect,
            StatBonuses = this.StatBonuses,
        };

        public override string Save() => String.Join("|",
            Name, MartialArts, Physical, Driving, Firearms,
            Diplomacy, ConcreteJungle, Wounds, Respect, StatBonuses);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Name = save[0];
            MartialArts = int.Parse(save[1]);
            Physical = int.Parse(save[2]);
            Driving = int.Parse(save[3]);
            Firearms = int.Parse(save[4]);
            Diplomacy = int.Parse(save[5]);
            ConcreteJungle = int.Parse(save[6]);
            Wounds = int.Parse(save[7]);
            Respect = int.Parse(save[8]);
            StatBonuses = int.Parse(save[9]);

            IsProtagonist = true;
        }
    }
}

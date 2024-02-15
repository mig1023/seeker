using System;

namespace Seeker.Gamebook.OrcsDay
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        public int Muscle { get; set; }
        public int Wits { get; set; }
        public int Courage { get; set; }
        public int Luck { get; set; }
        public int Weapon { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }

        public int Orcishness { get; set; }
        
        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, max: 5, _hitpoints, this);
        }

        private int _money;
        public int Money
        {
            get => _money;
            set => _money = Game.Param.Setter(value);
        }

        public int Bet { get; set; } 

        public int StatBonuses { get; set; }

        public override void Init()
        {
            base.Init();

            Muscle = 0;
            Wits = 0;
            Courage = 0;
            Luck = 0;
            Weapon = 0;
            Hitpoints = 5;
            Money = 20;
            Orcishness = 6;
            StatBonuses = 10;
            WayBack = 0;
            Bet = 1;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Muscle = this.Muscle,
            Wits = this.Wits,
            Courage = this.Courage,
            Luck = this.Luck,
            Weapon = this.Weapon,
            Hitpoints = this.Hitpoints,
            Money = this.Money,
            Orcishness = this.Orcishness,
            Attack = this.Attack,
            Defense = this.Defense,
            StatBonuses = this.StatBonuses,
            Bet = this.Bet,
        };

        public override string Save() => String.Join("|",
            Name, Muscle, Wits, Courage, Luck, Hitpoints, Money, Orcishness,
            StatBonuses, WayBack, Bet, Weapon);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Name = save[0];
            Muscle = int.Parse(save[1]);
            Wits = int.Parse(save[2]);
            Courage = int.Parse(save[3]);
            Luck = int.Parse(save[4]);
            Hitpoints = int.Parse(save[5]);
            Money = int.Parse(save[6]);
            Orcishness = int.Parse(save[7]);
            StatBonuses = int.Parse(save[8]);
            WayBack = int.Parse(save[9]);
            Bet = int.Parse(save[10]);
            Weapon = int.Parse(save[11]);

            IsProtagonist = true;
        }
    }
}

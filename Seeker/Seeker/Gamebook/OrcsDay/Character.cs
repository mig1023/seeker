using System;

namespace Seeker.Gamebook.OrcsDay
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Muscle;
        public int Wits;
        public int Courage;
        public int Luck;

        public int Attack;
        public int Defense;
        public int Health;

        public int Orcishness;

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value);
        }

        private int _money;
        public int Money
        {
            get => _money;
            set => _money = Game.Param.Setter(value);
        }

        public override void Init()
        {
            Name = String.Empty;
            Muscle = 0;
            Wits = 0;
            Courage = 0;
            Luck = 0;
            Hitpoints = 5;
            Money = 20;
            Orcishness = 6;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            Muscle = this.Muscle,
            Wits = this.Wits,
            Courage = this.Courage,
            Luck = this.Luck,
            Hitpoints = this.Hitpoints,
            Money = this.Money,
            Orcishness = this.Orcishness,
            Attack = this.Attack,
            Defense = this.Defense,
            Health = this.Health,
        };

        public override string Save() => String.Join("|",
            Name, Muscle, Wits, Courage, Luck, Hitpoints, Money, Orcishness
        );

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
        }
    }
}

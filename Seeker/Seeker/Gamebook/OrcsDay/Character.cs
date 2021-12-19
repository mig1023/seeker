using System;

namespace Seeker.Gamebook.OrcsDay
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Muscle { get; set; }
        public int Wits { get; set; }
        public int Courage { get; set; }
        public int Luck { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }

        public int Orcishness { get; set; }
        
        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set
            {
                if (value < 0)
                    _hitpoints = 0;
                else if (value > 5)
                    _hitpoints = 5;
                else
                    _hitpoints = value;
            }
        }

        private int _money;
        public int Money
        {
            get => _money;
            set => _money = Game.Param.Setter(value);
        }

        public int StatBonuses { get; set; }

        public int WayBack { get; set; }

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
            StatBonuses = 10;
            WayBack = 0;
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
            StatBonuses = this.StatBonuses,
        };

        public override string Save() => String.Join("|",
            Name, Muscle, Wits, Courage, Luck, Hitpoints, Money, Orcishness, StatBonuses, WayBack
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
            StatBonuses = int.Parse(save[8]);
            WayBack = int.Parse(save[9]);
        }

        public override string Debug() => String.Format("Бонусов: {0}\nWayBack: {1}", StatBonuses, WayBack);
    }
}

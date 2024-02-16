using System;

namespace Seeker.Gamebook.VWeapons
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        private int _suspicions;
        public int Suspicions
        {
            get => _suspicions;
            set => _suspicions = Game.Param.Setter(value, max: 5);
        }

        private int _time;
        public int Time
        {
            get => _time;
            set => _time = Game.Param.Setter(value, max: 12);
        }

        private int _accuracy;
        public int Accuracy
        {
            get => _accuracy;
            set => _accuracy = Game.Param.Setter(value, max: 5, _accuracy, this);
        }

        private int _cartridges;
        public int Cartridges
        {
            get => _cartridges;
            set => _cartridges = Game.Param.Setter(value, max: 8, _cartridges, this);
        }

        private int _head;
        public int Head
        {
            get => _head;
            set => _head = Game.Param.Setter(value, max: 3, _head, this);
        }

        private int _shoulderGirdle;
        public int ShoulderGirdle
        {
            get => _shoulderGirdle;
            set => _shoulderGirdle = Game.Param.Setter(value, max: 4, _shoulderGirdle, this);
        }

        private int _body;
        public int Body
        {
            get => _body;
            set => _body = Game.Param.Setter(value, max: 4, _body, this);
        }

        private int _hands;
        public int Hands
        {
            get => _hands;
            set => _hands = Game.Param.Setter(value, max: 4, _hands, this);
        }

        private int _legs;
        public int Legs
        {
            get => _legs;
            set => _legs = Game.Param.Setter(value, max: 4, _legs, this);
        }

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value);
        }

        public bool First { get; set; }
        public bool Dead { get; set; }
        public bool WithoutCartridges { get; set; }
        public bool Animal { get; set; }

        public override void Init()
        {
            base.Init();

            Suspicions = 0;
            Time = 12;
            Accuracy = 3;
            Cartridges = 8;

            Head = 3;
            ShoulderGirdle = 4;
            Body = 4;
            Hands = 4;
            Legs = 4;

            First = false;
            Dead = false;
            WithoutCartridges = false;
            Animal = false;

        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,

            Suspicions = this.Suspicions,
            Time = this.Time,
            Accuracy = this.Accuracy,
            Cartridges = this.Cartridges,

            Head = this.Head,
            ShoulderGirdle = this.ShoulderGirdle,
            Body = this.Body,
            Hands = this.Hands,
            Legs = this.Legs,

            Hitpoints = this.Hitpoints,
            First = this.First,
            Dead = this.Dead,
            WithoutCartridges = this.WithoutCartridges,
            Animal = this.Animal,
        };

        public override string Save() => String.Join("|",
            Suspicions, Time, Accuracy, Cartridges, Head, ShoulderGirdle,
            Body, Hands, Legs, (Dead ? 1 : 0));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Suspicions = int.Parse(save[0]);
            Time = int.Parse(save[1]);
            Accuracy = int.Parse(save[2]);
            Cartridges = int.Parse(save[3]);

            Head = int.Parse(save[4]);
            ShoulderGirdle = int.Parse(save[5]);
            Body = int.Parse(save[6]);
            Hands = int.Parse(save[7]);
            Legs = int.Parse(save[8]);
            Dead = (int.Parse(save[9]) == 1);

            IsProtagonist = true;
        }
    }
}

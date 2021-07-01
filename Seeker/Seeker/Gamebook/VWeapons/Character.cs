using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.VWeapons
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.VWeapons.Character();

        private int _suspicions;
        public int Suspicions
        {
            get => _suspicions;
            set
            {
                if (value > 5)
                    _suspicions = 5;
                else if (value < 0)
                    _suspicions = 0;
                else
                    _suspicions = value;
            }
        }

        private int _time;
        public int Time
        {
            get => _time;
            set
            {
                if (value > 12)
                    _time = 12;
                else if (value < 0)
                    _time = 0;
                else
                    _time = value;
            }
        }

        private int _accuracy;
        public int Accuracy
        {
            get => _accuracy;
            set
            {
                if (value > 5)
                    _accuracy = 5;
                else if (value < 0)
                    _accuracy = 0;
                else
                    _accuracy = value;
            }
        }

        private int _cartridges;
        public int Cartridges
        {
            get => _cartridges;
            set
            {
                if (value > 8)
                    _cartridges = 8;
                else if (value < 0)
                    _cartridges = 0;
                else
                    _cartridges = value;
            }
        }

        private int _head;
        public int Head
        {
            get => _head;
            set
            {
                if (value > 3)
                    _head = 3;
                else if (value < 0)
                    _head = 0;
                else
                    _head = value;
            }
        }

        private int _shoulderGirdle;
        public int ShoulderGirdle 
        {
            get => _shoulderGirdle;
            set
            {
                if (value > 4)
                    _shoulderGirdle = 4;
                else if (value < 0)
                    _shoulderGirdle = 0;
                else
                    _shoulderGirdle = value;
            }
        }

        private int _body;
        public int Body
        {
            get => _body;
            set
            {
                if (value > 4)
                    _body = 4;
                else if (value < 0)
                    _body = 0;
                else
                    _body = value;
            }
        }

        private int _hands;
        public int Hands
        {
            get => _hands;
            set
            {
                if (value > 4)
                    _hands = 4;
                else if (value < 0)
                    _hands = 0;
                else
                    _hands = value;
            }
        }

        private int _legs;
        public int Legs
        {
            get => _legs;
            set
            {
                if (value > 4)
                    _legs = 4;
                else if (value < 0)
                    _legs = 0;
                else
                    _legs = value;
            }
        }

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set
            {
                if (value < 0)
                    _hitpoints = 0;
                else
                    _hitpoints = value;
            }
        }

        public bool First { get; set; }
        public bool Dead { get; set; }
        public bool WithoutCartridges { get; set; }
        public bool Animal { get; set; }

        public override void Init()
        {
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

        public override string Save() =>
            String.Join("|", Suspicions, Time, Accuracy, Cartridges, Head, ShoulderGirdle, Body, Hands, Legs, (Dead ? 1 : 0));

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
        }
    }
}

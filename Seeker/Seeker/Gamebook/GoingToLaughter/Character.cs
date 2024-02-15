using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        private int _heroism;
        public int Heroism
        {
            get => _heroism;
            set => _heroism = Game.Param.Setter(value, _heroism, this);
        }

        private int _villainy;
        public int Villainy
        {
            get => _villainy;
            set => _villainy = Game.Param.Setter(value);
        }

        private int _buffoonery;
        public int Buffoonery
        {
            get => _buffoonery;
            set => _buffoonery = Game.Param.Setter(value, _buffoonery, this);
        }

        private int _inspiration;
        public int Inspiration
        {
            get => _inspiration;
            set => _inspiration = Game.Param.Setter(value, _inspiration, this);
        }

        public List<string> Advantages { get; set; }
        public List<string> Disadvantages { get; set; }
        public int Balance { get; set; }
        public int AbubakarOffer { get; set; }

        public override void Init()
        {
            base.Init();

            Heroism = 0;
            Villainy = 0;
            Buffoonery = 5;
            Inspiration = 1;
            Balance = 0;
            AbubakarOffer = 0;

            Advantages = new List<string>();
            Disadvantages = new List<string>();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,

            Heroism = this.Heroism,
            Villainy = this.Villainy,
            Buffoonery = this.Buffoonery,
            Inspiration = this.Inspiration,

            Advantages = new List<string>(this.Advantages),
            Disadvantages = new List<string>(this.Disadvantages),
            Balance = this.Balance,
            AbubakarOffer = this.AbubakarOffer,
        };

        public override string Save() => String.Join("|",
            Heroism, Villainy, Buffoonery, Inspiration, Balance,
            String.Join(",", Advantages), String.Join(",", Disadvantages),
            AbubakarOffer);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Heroism = int.Parse(save[0]);
            Villainy = int.Parse(save[1]);
            Buffoonery = int.Parse(save[2]);
            Inspiration = int.Parse(save[3]);

            Balance = int.Parse(save[4]);
            Advantages = new List<string>(save[5].Split(','));
            Disadvantages = new List<string>(save[6].Split(','));
            AbubakarOffer = int.Parse(save[7]);

            IsProtagonist = true;
        }
    }
}

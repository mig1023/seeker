using System;

namespace Seeker.Gamebook.StrikeBack
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _attack;
        public int MaxAttack { get; set; }
        public int Attack
        {
            get => _attack;
            set => _attack = Game.Param.Setter(value, max: MaxAttack, _attack, this);
        }

        private int _defence;
        public int MaxDefence { get; set; }
        public int Defence
        {
            get => _defence;
            set => _defence = Game.Param.Setter(value, max: MaxDefence, _defence, this);
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, max: MaxEndurance, _endurance, this);
        }

        public string Creature { get; set; }

        public override void Init()
        {
            base.Init();

            Name = "Главный герой";
            Creature = "ГОБЛИН";

            MaxAttack = 2;
            Attack = MaxAttack;
            MaxDefence = 8;
            Defence = MaxDefence;
            MaxEndurance = 5;
            Endurance = MaxEndurance;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Creature = this.Creature,
            MaxAttack = this.MaxAttack,
            Attack = this.Attack,
            MaxDefence = this.MaxDefence,
            Defence = this.Defence,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
        };

        public override string Save() => String.Join("|",
            MaxAttack, Attack, MaxDefence, Defence, MaxEndurance, Endurance, Creature
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxAttack = int.Parse(save[0]);
            Attack = int.Parse(save[1]);
            MaxDefence = int.Parse(save[2]);
            Defence = int.Parse(save[3]);
            MaxEndurance = int.Parse(save[4]);
            Endurance = int.Parse(save[5]);

            Creature = save[6];

            IsProtagonist = true;
        }
    }
}

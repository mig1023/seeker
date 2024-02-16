using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.HeartOfIce
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        private int _life;
        public int MaxLife { get; set; }
        public int Life
        {
            get => _life;
            set => _life = Game.Param.Setter(value, max: MaxLife, _life, this);
        }

        private int _money;
        public int Money
        {
            get => _money;
            set => _money = Game.Param.Setter(value, _money, this);
        }

        private int _food;
        public int Food
        {
            get => _food;
            set => _food = Game.Param.Setter(value, _food, this);
        }

        private int _shots;
        public int Shots
        {
            get => _shots;
            set => _shots = Game.Param.Setter(value, _shots, this);
        }

        public List<string> Skills { get; set; }
        public int SkillsValue { get; set; }
        public bool Chosen { get; set; }
        public int Split { get; set; }

        public override void Init()
        {
            base.Init();

            MaxLife = 10;
            Life = MaxLife;
            Money = 30;
            Food = 0;
            Shots = 0;
            Chosen = false;
            SkillsValue = 4;
            Split = 0;

            Skills = new List<string>();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Money = this.Money,
            Food = this.Food,
            Shots = this.Shots,
            SkillsValue = this.SkillsValue,
            Split = this.Split,
        };

        public override string Save() => String.Join("|",
            MaxLife, Life, Money, Food, Shots, SkillsValue, Split,
            (Chosen ? 1 : 0), String.Join(":", Skills).TrimEnd(':'));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Money = int.Parse(save[2]);
            Food = int.Parse(save[3]);
            Shots = int.Parse(save[4]);
            SkillsValue = int.Parse(save[5]);
            Split = int.Parse(save[6]);
            Chosen = (int.Parse(save[7]) == 1);

            Skills = save[8].Split(':').ToList();

            IsProtagonist = true;
        }
    }
}

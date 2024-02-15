using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Ants
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        public List<bool> Dice { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = Game.Param.Setter(value);
        }

        private int _increase;
        public int Increase
        {
            get => _increase;
            set => _increase = Game.Param.Setter(value);
        }

        public int Time { get; set; }
        public int Defence { get; set; }
        public int Start { get; set; }

        public string EnemyName { get; set; }

        private int _enemyHitpoints;
        public int EnemyHitpoints
        {
            get => _enemyHitpoints;
            set => _enemyHitpoints = Game.Param.Setter(value);
        }

        public override void Init()
        {
            base.Init();

            Quantity = 0;
            Increase = 0;
            Time = 0;
            Defence = 0;
            Start = 0;
            EnemyName = String.Empty;
            EnemyHitpoints = 0;
            Dice = new List<bool> { false, false, false, false, false, false, false };
        }

        public Character Clone() => new Character()
        {
            Dice = this.Dice,
            Quantity = this.Quantity,
            Increase = this.Increase,
            Time = this.Time,
            Defence = this.Defence,
            Start = this.Start,
            EnemyName = this.EnemyName,
            EnemyHitpoints = this.EnemyHitpoints,
        };

        public override string Save() => String.Join("|",
            String.Join(",", Dice.Select(x => x ? "1" : "0")),
            Quantity, Increase, Time, Defence, Start, EnemyName, EnemyHitpoints
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Dice = save[0].Split(',').Select(x => x == "1").ToList();
            Quantity = int.Parse(save[1]);
            Increase = int.Parse(save[2]);
            Time = int.Parse(save[3]);
            Defence = int.Parse(save[4]);
            Start = int.Parse(save[5]);
            EnemyName = save[6];
            EnemyHitpoints = int.Parse(save[7]);

            IsProtagonist = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.AntSurvival
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public List<bool> Dice { get; set; }

        public int Quantity { get; set; }
        public int Increase { get; set; }
        public int Time { get; set; }
        public int Enemy { get; set; }

        public override void Init()
        {
            base.Init();

            Quantity = 0;
            Increase = 0;
            Time = 0;
            Enemy = 0;
            Dice = new List<bool> { false, false, false, false, false, false, false };
        }

        public Character Clone() => new Character()
        {
            Dice = this.Dice,
            Quantity = this.Quantity,
            Increase = this.Increase,
            Time = this.Time,
            Enemy = this.Enemy,
        };

        public override string Save() => String.Join("|",
            String.Join(",", Dice.Select(x => x ? "1" : "0")),
            Quantity, Increase, Time, Enemy
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Dice = save[0].Split(',').Select(x => x == "1").ToList();
            Quantity = int.Parse(save[1]);
            Increase = int.Parse(save[2]);
            Time = int.Parse(save[3]);
            Enemy = int.Parse(save[4]);

            IsProtagonist = true;
        }
    }
}

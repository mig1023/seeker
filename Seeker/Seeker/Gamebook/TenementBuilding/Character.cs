using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.TenementBuilding
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        public List<bool> Luck { get; set; }

        public override void Init()
        {
            base.Init();

            Luck = new List<bool> { false, true, true, true, true, true, true };
            Luck[Game.Dice.Roll()] = false;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
        };

        public override string Save() => String.Join("|",
            String.Join(",", Luck.Select(x => x ? "1" : "0")));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Luck = save[0].Split(',').Select(x => x == "1").ToList();
            IsProtagonist = true;
        }
    }
}

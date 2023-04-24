using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.AntSurvival
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public List<bool> Dice { get; set; }

        public override void Init()
        {
            base.Init();

            Dice = new List<bool> { false, false, false, false, false, false, false };
        }

        public Character Clone() => new Character()
        {
            Dice = this.Dice,
        };

        public override string Save() => String.Join("|",
            String.Join(",", Dice.Select(x => x ? "1" : "0"))
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Dice = save[0].Split(',').Select(x => x == "1").ToList();

            IsProtagonist = true;
        }
    }
}

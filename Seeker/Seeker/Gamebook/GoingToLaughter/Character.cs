using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public List<string> Advantages { get; set; }
        public List<string> Disadvantages { get; set; }
        public int Balance { get; set; }

        public override void Init()
        {
            Advantages = new List<string>();
            Disadvantages = new List<string>();
            Balance = 0;
        }

        public Character Clone() => new Character()
        {
            Advantages = new List<string>(this.Advantages),
            Disadvantages = new List<string>(this.Disadvantages),
            Balance = this.Balance,
        };

        public override string Save() => String.Join("|",
            Balance, String.Join(",", Advantages), String.Join(",", Disadvantages)
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Balance = int.Parse(save[0]);
            Advantages = new List<string>(save[1].Split(','));
            Disadvantages = new List<string>(save[2].Split(','));
        }
    }
}

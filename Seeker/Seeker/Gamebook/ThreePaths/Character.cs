using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ThreePaths
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        public int? Time { get; set; }
        public List<string> Spells { get; set; }
        public int SpellSlots { get; set; }

        public override void Init()
        {
            base.Init();

            Time = null;
            SpellSlots = 9;
            Spells = new List<string>();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Time = this.Time,
            Spells = new List<string>(),
        };

        public override string Save() => String.Join("|",
            Time, SpellSlots, String.Join(",", Spells).TrimEnd(','));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Time = Game.Continue.IntNullableParse(save[0]);
            SpellSlots = int.Parse(save[1]);
            Spells = save[2].Split(',').ToList();

            IsProtagonist = true;
        }
    }
}

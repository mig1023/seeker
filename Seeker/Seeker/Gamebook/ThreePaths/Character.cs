using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.ThreePaths
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.ThreePaths.Character();

        public string Name { get; set; }
        public int? Time { get; set; }
        public int? Spells { get; set; }

        public void Init()
        {
            Time = null;
            Spells = null;
        }

        public Character Clone()
        {
            return new Character()
            {
                Time = this.Time,
                Spells = this.Spells,
            };
        }

        public string Save()
        {
            return String.Format("{0}|{1}", Time, Spells);
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Time = Game.Continue.IntNullableParse(save[0]);
            Spells = Game.Continue.IntNullableParse(save[1]);
        }
    }
}

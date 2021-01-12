using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.CreatureOfHavoc.Character();

        public string Name { get; set; }
        public int Mastery { get; set; }
        public int Endurance { get; set; }
        public int Luck { get; set; }

        public void Init()
        {
            Name = String.Empty;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
            };
        }

        public string Save()
        {
            return String.Format("{0}", Mastery);
        }

        public void Load(string saveLine)
        {
            Mastery = int.Parse(saveLine);
        }
    }
}

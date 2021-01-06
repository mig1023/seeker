using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.OctopusIsland
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.OctopusIsland.Character();

        public string Name { get; set; }

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
            return String.Format("{0}", Name);
        }

        public void Load(string saveLine)
        {
            Name = saveLine;
        }
    }
}

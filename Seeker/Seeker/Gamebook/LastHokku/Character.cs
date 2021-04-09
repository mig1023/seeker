using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LastHokku
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LastHokku.Character();

        public string Name { get; set; }

        public List<string> Hokku { get; set; }

        public void Init()
        {
            Name = String.Empty;
            Hokku = new List<string>();
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
            };
        }

        public string Save() => String.Empty;

        public void Load(string saveLine) { }
    }
}

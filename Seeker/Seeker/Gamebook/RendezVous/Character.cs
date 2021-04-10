using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.RendezVous
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.RendezVous.Character();

        public string Name { get; set; }
        public int Awareness { get; set; }

        public void Init()
        {
            Name = String.Empty;
            Awareness = 0;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            Awareness = this.Awareness,
        };

        public string Save() => Awareness.ToString();

        public void Load(string saveLine)
        {
            Awareness = int.Parse(saveLine);
        }
    }
}

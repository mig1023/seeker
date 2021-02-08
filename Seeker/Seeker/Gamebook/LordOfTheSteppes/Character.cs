using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LordOfTheSteppes.Character();

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

        public string Save() => String.Empty;

        public void Load(string saveLine) { }
    }
}

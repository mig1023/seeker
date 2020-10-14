using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SwampFever
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.SwampFever.Character();

        public string Name { get; set; }

        public void Init()
        {

        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
            };
        }
    }
}

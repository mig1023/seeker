using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.RockOfTerror
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.RockOfTerror.Character();

        public string Name { get; set; }

        public double Time { get; set; }

        public void Init()
        {
            Time = 0;
        }

        public Character Clone()
        {
            return new Character() { Time = this.Time };
        }
    }
}

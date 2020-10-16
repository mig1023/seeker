using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SwampFever
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.SwampFever.Character();

        public string Name { get; set; }
        public int Fury { get; set; }
        public int Creds { get; set; }
        public int Rate { get; set; }
        public int Hitpoints { get; set; }


        public void Init()
        {
            Name = String.Empty;
            Fury = 0;
            Creds = 0;
            Rate = 100;
            Hitpoints = 1;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                Fury = this.Fury,
                Creds = this.Creds,
                Rate = this.Rate,
                Hitpoints = this.Hitpoints,
            };
        }
    }
}

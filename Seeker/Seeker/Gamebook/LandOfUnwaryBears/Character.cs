using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LandOfUnwaryBears
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LandOfUnwaryBears.Character();

        public string Name { get; set; }

        public void Init()
        {
            Name = String.Empty;
        }

        public string Save() => String.Empty;

        public void Load(string saveLine) { }
    }
}

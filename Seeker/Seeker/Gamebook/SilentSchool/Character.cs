using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.SilentSchool
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.SilentSchool.Character();

        public string Name { get; set; }
        public int Life { get; set; }

        public void Init()
        {
            Life = 30;
        }

        public Character Clone()
        {
            return new Character()
            {
                Life = this.Life,
            };
        }

        public string Save() => String.Format("{0}", Life);

        public void Load(string saveLine)
        {
            Life = int.Parse(saveLine);
        }
    }
}

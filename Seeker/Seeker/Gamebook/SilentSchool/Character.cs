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
        public int Grail { get; set; }
        public int ChangeDecision { get; set; }
        
        public void Init()
        {
            Life = 30;
            Grail = 0;
            ChangeDecision = 0;
        }

        public Character Clone()
        {
            return new Character()
            {
                Life = this.Life,
                Grail = this.Grail,
                ChangeDecision = this.ChangeDecision,
            };
        }

        public string Save() => String.Format("{0}|{1}|{2}", Life, Grail, ChangeDecision);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Life = int.Parse(save[0]);
            Grail = int.Parse(save[1]);
            ChangeDecision = int.Parse(save[2]);
        }
    }
}

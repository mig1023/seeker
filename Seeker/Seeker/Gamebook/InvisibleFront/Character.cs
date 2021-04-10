using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.InvisibleFront
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.InvisibleFront.Character();

        public string Name { get; set; }
        public int Dissatisfaction { get; set; }
        public int Recruitment { get; set; }


        public void Init()
        {
            Dissatisfaction = 0;
            Recruitment = 0;
        }

        public Character Clone() => new Character()
        {
            Dissatisfaction = this.Dissatisfaction,
            Recruitment = this.Recruitment,
        };

        public string Save() => String.Join("|", Dissatisfaction, Recruitment);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Dissatisfaction = int.Parse(save[0]);
            Recruitment = int.Parse(save[1]);
        }
    }
}

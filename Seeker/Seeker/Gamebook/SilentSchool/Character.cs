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

        private int _life;
        public int Life
    {
            get => _life;
            set
            {
                if (value < 0)
                    _life = 0;
                else
                    _life = value;
            }
        }

        public string Weapon { get; set; }
        public int Grail { get; set; }
        public int ChangeDecision { get; set; }
        public int HarmSelfAlready { get; set; }
        
        public void Init()
        {
            Life = 30;
            Weapon = String.Empty;
            Grail = 0;
            ChangeDecision = 0;
            HarmSelfAlready = 0;
        }

        public Character Clone() => new Character()
        {
            Life = this.Life,
            Weapon = this.Weapon,
            Grail = this.Grail,
            ChangeDecision = this.ChangeDecision,
            HarmSelfAlready = this.HarmSelfAlready,
        };

        public string Save() => String.Join("|", Life, Weapon,Grail, ChangeDecision, HarmSelfAlready);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Life = int.Parse(save[0]);
            Weapon = save[1];
            Grail = int.Parse(save[2]);
            ChangeDecision = int.Parse(save[3]);
            HarmSelfAlready = int.Parse(save[4]);
        }
    }
}

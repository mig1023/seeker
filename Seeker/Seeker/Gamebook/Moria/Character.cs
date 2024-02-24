using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moria
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public List<string> Fellowship { get; set; }

        public int MagicPause { get; set; }

        public override void Init()
        {
            base.Init();

            MagicPause = 0;
            Fellowship = new List<string>(Constants.Fellowship.Keys);
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MagicPause = this.MagicPause,
            Fellowship = new List<string>(this.Fellowship),
        };

        public override string Save() => String.Join("|",
            Name, MagicPause, String.Join(",", Fellowship));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Name = save[0];
            MagicPause = int.Parse(save[1]);
            Fellowship = save[2].Split(',').ToList();

            IsProtagonist = true;
        }
    }
}

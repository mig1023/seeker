using System;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Initiation { get; set; }

        public override void Init()
        {
            base.Init();

            Initiation = 3;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Initiation = this.Initiation,
        };

        public override string Save() => Initiation.ToString();

        public override void Load(string saveLine)
        {
            Initiation = int.Parse(saveLine);
            IsProtagonist = true;
        }
    }
}

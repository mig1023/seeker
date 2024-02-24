using System;

namespace Seeker.Gamebook.RendezVous
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Awareness { get; set; }

        public override void Init()
        {
            base.Init();

            Awareness = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Awareness = this.Awareness,
        };

        public override string Save() => Awareness.ToString();

        public override void Load(string saveLine)
        {
            Awareness = int.Parse(saveLine);
            IsProtagonist = true;
        }
    }
}

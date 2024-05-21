using System;

namespace Seeker.Gamebook.WrongWayGoBack
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Time { get; set; }

        public override void Init()
        {
            base.Init();
            Time = 3661;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Time = this.Time,
        };

        public override string Save() =>
            Time.ToString();

        public override void Load(string saveLine)
        {
            Time = Game.Xml.IntParse(saveLine);
            IsProtagonist = true;
        }
    }
}

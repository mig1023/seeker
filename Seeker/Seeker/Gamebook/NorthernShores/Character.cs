using System;

namespace Seeker.Gamebook.NorthernShores
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Heat { get; set; }

        public bool Gameover { get; set; }

        public override void Init()
        {
            base.Init();
            Heat = 0;
            Gameover = false;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Heat = this.Heat,
            Gameover = this.Gameover,
        };

        public override string Save() => String.Join("|",
            Heat, Gameover ? 1 : 0);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Heat = Game.Xml.IntParse(save[0]);
            Gameover = save[1] == "1";

            IsProtagonist = true;
        }
    }
}

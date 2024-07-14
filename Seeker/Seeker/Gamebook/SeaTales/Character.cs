using System;

namespace Seeker.Gamebook.SeaTales
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Heat { get; set; }

        private int _nonsense;
        public int Nonsense
        {
            get => _nonsense;
            set => _nonsense = Game.Param.Setter(value);
        }

        public bool Gameover { get; set; }

        public override void Init()
        {
            base.Init();
            Heat = 0;
            Nonsense = 0;
            Gameover = false;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Heat = this.Heat,
            Nonsense = this.Nonsense,
            Gameover = this.Gameover,
        };

        public override string Save() => String.Join("|",
            Heat, Nonsense, Gameover ? 1 : 0);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Heat = Game.Xml.IntParse(save[0]);
            Nonsense = Game.Xml.IntParse(save[1]);
            Gameover = save[2] == "1";

            IsProtagonist = true;
        }
    }
}

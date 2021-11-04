using System;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _sestertius;
        public int Sestertius
        {
            get => _sestertius;
            set => _sestertius = Game.Param.Setter(value);
        }

        private int _honor;
        public int Honor
        {
            get => _honor;
            set => _honor = Game.Param.Setter(value);
        }

        public override void Init()
        {
            Name = String.Empty;
            Sestertius = 100;
            Honor = 3;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            Sestertius = this.Sestertius,
            Honor = this.Honor,
        };

        public override string Save() => String.Join("|", Sestertius, Honor);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Sestertius = int.Parse(save[0]);
            Honor = int.Parse(save[1]);
        }
    }
}

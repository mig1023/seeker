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

        private int _legionaries;
        public int Legionaries
        {
            get => _legionaries;
            set => _legionaries = Game.Param.Setter(value);
        }

        private int _horsemen;
        public int Horsemen
        {
            get => _horsemen;
            set => _horsemen = Game.Param.Setter(value);
        }
        
        public int Discipline { get; set; }

        public override void Init()
        {
            Name = String.Empty;
            Sestertius = 100;
            Honor = 3;
            Legionaries = 0;
            Horsemen = 0;
            Discipline = 0;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            Sestertius = this.Sestertius,
            Honor = this.Honor,
            Legionaries = this.Legionaries,
            Horsemen = this.Horsemen,
            Discipline = this.Discipline,
        };

        public override string Save() => String.Join("|", Sestertius, Honor, Legionaries, Horsemen, Discipline);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Sestertius = int.Parse(save[0]);
            Honor = int.Parse(save[1]);
            Legionaries = int.Parse(save[2]);
            Horsemen = int.Parse(save[3]);
            Discipline = int.Parse(save[4]);
        }
    }
}

using System;

namespace Seeker.Gamebook.PrisonerOfMoritaiCastle
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, _hitpoints, this);
        }

        private int _shuriken;
        public int Shuriken
        {
            get => _shuriken;
            set => _shuriken = Game.Param.Setter(value, _shuriken, this);
        }

        private int _arrows;
        public int Arrows
        {
            get => _arrows;
            set => _arrows = Game.Param.Setter(value, _arrows, this);
        }

        public override void Init()
        {
            base.Init();

            Hitpoints = 5;
            Shuriken = 5;
            Arrows = 10;

            Game.Healing.Add(name: "Тонизирующее", healing: 1, portions: 1);
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Hitpoints = this.Hitpoints,
            Shuriken = this.Shuriken,
            Arrows = this.Arrows,
        };

        public override string Save() => String.Join("|",
            Hitpoints, Shuriken, Arrows);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Hitpoints = int.Parse(save[0]);
            Shuriken = int.Parse(save[1]);
            Arrows = int.Parse(save[2]);

            IsProtagonist = true;
        }
    }
}

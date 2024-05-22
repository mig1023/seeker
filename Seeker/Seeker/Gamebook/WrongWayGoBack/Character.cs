using System;

namespace Seeker.Gamebook.WrongWayGoBack
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _skill;
        public int Skill
        {
            get => _skill;
            set => _skill = Game.Param.Setter(value, _skill, this);
        }

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, _hitpoints, this);
        }

        private int _luck;
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, _luck, this);
        }

        public int Time { get; set; }

        public override void Init()
        {
            base.Init();

            Skill = Game.Dice.Roll();
            Hitpoints = 12 + Game.Dice.Roll(dices: 2);
            Luck = 6 + Game.Dice.Roll();
            Time = 3661;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Skill = this.Skill,
            Hitpoints = this.Hitpoints,
            Luck = this.Luck,
            Time = this.Time,
        };

        public override string Save() => String.Join("|",
            Skill, Hitpoints, Luck, Time);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Skill = int.Parse(save[0]);
            Hitpoints = int.Parse(save[1]);
            Luck = int.Parse(save[2]);
            Time = int.Parse(save[3]);

            IsProtagonist = true;
        }
    }
}

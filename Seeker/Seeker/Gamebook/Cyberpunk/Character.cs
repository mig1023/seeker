using System;

namespace Seeker.Gamebook.Cyberpunk
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _planning;
        public int Planning
        {
            get => _planning;
            set => _planning = Game.Param.Setter(value, _planning, this);
        }

        private int _preparation;
        public int Preparation
        {
            get => _preparation;
            set => _preparation = Game.Param.Setter(value, _preparation, this);
        }

        private int _luck;
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, _luck, this);
        }

        private int _cybernetics;
        public int Cybernetics
        {
            get => _cybernetics;
            set => _cybernetics = Game.Param.Setter(value, max: 99, _cybernetics, this);
        }

        public override void Init()
        {
            base.Init();

            Planning = 0;
            Preparation = 0;
            Luck = 0;
            Cybernetics = 1;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Planning = this.Planning,
            Preparation = this.Preparation,
            Luck = this.Luck,
            Cybernetics = this.Cybernetics,
        };

        public override string Save() => String.Join("|", Planning, Preparation, Luck, Cybernetics);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Planning = int.Parse(save[0]);
            Preparation = int.Parse(save[1]);
            Luck = int.Parse(save[2]);
            Cybernetics = int.Parse(save[3]);

            IsProtagonist = true;
        }
    }
}

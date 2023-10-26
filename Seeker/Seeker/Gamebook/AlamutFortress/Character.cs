using System;

namespace Seeker.Gamebook.AlamutFortress
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value);
        }

        private int _hitpoints;
        public int MaxHitpoints { get; set; }
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, max: MaxHitpoints, _hitpoints, this);
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set => _gold = Game.Param.Setter(value, _gold, this);
        }

        public override void Init()
        {
            base.Init();

            Strength = Game.Dice.Roll(dices: 2) + 6;
            MaxHitpoints = Game.Dice.Roll(dices: 2) + 18;
            Hitpoints = MaxHitpoints;
            Gold = Game.Dice.Roll(dices: 4);
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Strength = this.Strength,
            MaxHitpoints = this.MaxHitpoints,
            Hitpoints = this.Hitpoints,
            Gold = this.Gold,
        };

        public override string Save() => String.Join("|",
            Strength, MaxHitpoints, Hitpoints, Gold
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            MaxHitpoints = int.Parse(save[1]);
            Hitpoints = int.Parse(save[2]);
            Gold = int.Parse(save[3]);

            IsProtagonist = true;
        }
    }
}

using System;

namespace Seeker.Gamebook.Moonrunner
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _mastery;
        public int MaxMastery { get; set; }
        public int Mastery
        {
            get => _mastery;
            set => _mastery = Game.Param.Setter(value, max: MaxMastery, _mastery, this);
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, max: MaxEndurance, _endurance, this);
        }

        private int _luck;
        public int MaxLuck { get; set; }
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, max: MaxLuck, _luck, this);
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set => _gold = Game.Param.Setter(value, _gold, this);
        }

        public int SkillSlots { get; set; }

        private int _enemySpells;
        public int EnemySpells
        {
            get => _enemySpells;
            set => _enemySpells = Game.Param.Setter(value, _enemySpells, this);
        }

        public int Offer { get; set; }

        public override void Init()
        {
            base.Init();

            MaxMastery = Game.Dice.Roll() + 6;
            Mastery = MaxMastery;
            MaxEndurance = Game.Dice.Roll(dices: 2) + 12;
            Endurance = MaxEndurance;
            MaxLuck = Game.Dice.Roll() + 6;
            Luck = MaxLuck;
            Gold = 15;
            SkillSlots = 4;
            EnemySpells = 10;
            Offer = 0;

            Game.Healing.Add(name: "Поесть", healing: 4, portions: 3);
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MaxMastery = this.MaxMastery,
            Mastery = this.Mastery,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
            MaxLuck = this.MaxLuck,
            Luck = this.Luck,
            Gold = this.Gold,
            SkillSlots = this.SkillSlots,
            EnemySpells = this.EnemySpells,
            Offer = this.Offer,
        };

        public override string Save() => String.Join("|",
            MaxMastery, Mastery, MaxEndurance, Endurance, MaxLuck,
            Luck, Gold, SkillSlots, EnemySpells, Offer);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxMastery = int.Parse(save[0]);
            Mastery = int.Parse(save[1]);
            MaxEndurance = int.Parse(save[2]);
            Endurance = int.Parse(save[3]);
            MaxLuck = int.Parse(save[4]);
            Luck = int.Parse(save[5]);
            Gold = int.Parse(save[6]);
            SkillSlots = int.Parse(save[7]);
            EnemySpells = int.Parse(save[8]);
            Offer = int.Parse(save[9]);

            IsProtagonist = true;
        }
    }
}

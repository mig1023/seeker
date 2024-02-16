using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

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

        public List<string> Spells { get; set; }
        public int SpellSlots { get; set; }

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
            SpellSlots = 10;
            Spells = new List<string>();

            Game.Healing.Add(name: "Попить", healing: 2, portions: 2);
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
            SpellSlots = this.SpellSlots,
            Spells = new List<string>(),
        };

        public override string Save() => String.Join("|",
            MaxMastery, Mastery, MaxEndurance, Endurance, MaxLuck, Luck, Gold, SpellSlots,
            String.Join(",", Spells).TrimEnd(',')
        );

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
            SpellSlots = int.Parse(save[7]);
            Spells = save[8].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            IsProtagonist = true;
        }
    }
}

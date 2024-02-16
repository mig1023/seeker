using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        public enum SpecialTechniques
        {
            TwoBlades, TotalProtection, FirstStrike, PowerfulStrike, Reaction, IgnoreFirstStrike,
            IgnorePowerfulStrike, IgnoreReaction, ExtendedDamage, PoisonBlade, Nope
        };

        public enum FightStyles { Fullback, Defensive, Counterattacking, Aggressive }

        private int _attack;
        public int MaxAttack { get; set; }
        public int Attack
        {
            get => _attack;
            set => _attack = Game.Param.Setter(value, max: MaxAttack, _attack, this);
        }

        private int _defence;
        public int MaxDefence { get; set; }
        public int Defence
        {
            get => _defence;
            set => _defence = Game.Param.Setter(value, max: MaxDefence, _defence, this);
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, max: MaxEndurance, _endurance, this);
        }

        private int _initiative;
        public int MaxInitiative { get; set; }
        public int Initiative
        {
            get => _initiative;
            set => _initiative = Game.Param.Setter(value, max: MaxInitiative, _initiative, this);
        }

        private int _coins;
        public int Coins
        {
            get => _coins;
            set => _coins = Game.Param.Setter(value, _coins, this);
        }

        public FightStyles FightStyle { get; set; }
        public List<SpecialTechniques> SpecialTechnique { get; set; }
        public int Bonuses { get; set; }

        public override void Init()
        {
            base.Init();

            Name = "Главный герой";

            MaxAttack = 8;
            Attack = MaxAttack;
            MaxDefence = 15;
            Defence = MaxDefence;
            MaxEndurance = 14;
            Endurance = MaxEndurance;
            MaxInitiative = 10;
            Initiative = MaxInitiative;
            Coins = 40;

            FightStyle = FightStyles.Counterattacking;
            SpecialTechnique = new List<SpecialTechniques>();
            Bonuses = 2;

            Game.Healing.Add(name: "Воспользоваться лечебной мазью", healing: 1, portions: 4);
            Game.Healing.Add(name: "Поесть", healing: 3, portions: 4);
            Game.Healing.Add(name: "Выпить напиток знахаря", healing: 4, portions: 2);
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MaxAttack = this.MaxAttack,
            Attack = this.Attack,
            MaxDefence = this.MaxDefence,
            Defence = this.Defence,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
            MaxInitiative = this.MaxInitiative,
            Initiative = this.Initiative,
            Coins = this.Coins,
            FightStyle = FightStyles.Counterattacking,
            SpecialTechnique = new List<SpecialTechniques>(this.SpecialTechnique),
            Bonuses = this.Bonuses,
        };

        public override string Save() => String.Join("|",
            MaxAttack, Attack, MaxDefence, Defence, MaxEndurance, Endurance, MaxInitiative, Initiative, Bonuses,
            String.Join(":", SpecialTechnique.ConvertAll(e => e.ToString())).TrimEnd(':'), Coins);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxAttack = int.Parse(save[0]);
            Attack = int.Parse(save[1]);
            MaxDefence = int.Parse(save[2]);
            Defence = int.Parse(save[3]);
            MaxEndurance = int.Parse(save[4]);
            Endurance = int.Parse(save[5]);
            MaxInitiative = int.Parse(save[6]);
            Initiative = int.Parse(save[7]);
            Bonuses = int.Parse(save[8]);
            Coins = int.Parse(save[10]);

            string[] specialTechniques = save[9].Split(':');

            foreach(string specialTechnique in specialTechniques)
            {
                bool success = Enum.TryParse(specialTechnique, out SpecialTechniques value);

                if (success)
                    SpecialTechnique.Add(value);
            }

            IsProtagonist = true;
        }

        public string GetSpecialTechniques()
        {
            if (SpecialTechnique.Count == 0)
                return String.Empty;

            if ((SpecialTechnique.Count == 1) && (SpecialTechnique[0] == SpecialTechniques.Nope))
                return String.Empty;

            List<string> techniques = SpecialTechnique.ConvertAll(x => Constants.TechniquesNames()[x]);
            return $"\n{String.Join(", ", techniques)}";
        }
    }
}

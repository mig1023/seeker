using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LordOfTheSteppes.Character();

        public enum SpecialTechniques { TwoBlades, TotalProtection, FirstStrike, PowerfulStrike, Reaction,
            IgnoreFirstStrike, IgnorePowerfulStrike, IgnoreReaction, ExtendedDamage, PoisonBlade, Nope };

        public enum FightStyles { Fullback, Defensive, Counterattacking, Aggressive }

        private int _attack;
        public int MaxAttack { get; set; }
        public int Attack
        {
            get => _attack;
            set
            {
                if (value > MaxAttack)
                    _attack = MaxAttack;
                else if (value < 0)
                    _attack = 0;
                else
                    _attack = value;
            }
        }

        private int _defence;
        public int MaxDefence { get; set; }
        public int Defence
        {
            get => _defence;
            set
            {
                if (value > MaxDefence)
                    _defence = MaxDefence;
                else if (value < 0)
                    _defence = 0;
                else
                    _defence = value;
            }
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get => _endurance;
            set
            {
                if (value > MaxEndurance)
                    _endurance = MaxEndurance;
                else if (value < 0)
                    _endurance = 0;
                else
                    _endurance = value;
            }
        }

        private int _initiative;
        public int MaxInitiative { get; set; }
        public int Initiative
        {
            get => _initiative;
            set
            {
                if (value > MaxInitiative)
                    _initiative = MaxInitiative;
                else if (value < 0)
                    _initiative = 0;
                else
                    _initiative = value;
            }
        }

        private int _coins;
        public int Coins
        {
            get => _coins;
            set
            {
                if (value < 0)
                    _coins = 0;
                else
                    _coins = value;
            }
        }

        public FightStyles FightStyle { get; set; }
        public List<SpecialTechniques> SpecialTechnique { get; set; }
        public int Bonuses { get; set; }

        public override void Init()
        {
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

        public override string Save()
        {
            string specialTechniques = String.Join(":", SpecialTechnique.ConvertAll(e => e.ToString()));

            return String.Join("|", MaxAttack, Attack, MaxDefence, Defence, MaxEndurance, Endurance,
                MaxInitiative, Initiative, Bonuses, specialTechniques, Coins);
        }

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
        }

        public string GetSpecialTechniques()
        {
            if (SpecialTechnique.Count == 0)
                return String.Empty;

            if ((SpecialTechnique.Count == 1) && (SpecialTechnique[0] == SpecialTechniques.Nope))
                return String.Empty;

            return String.Format("\n{0}", String.Join(", ", SpecialTechnique.ConvertAll(e => Constants.TechniquesNames()[e])));
        }
    }
}

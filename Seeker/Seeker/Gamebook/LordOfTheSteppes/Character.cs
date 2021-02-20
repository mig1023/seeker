using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LordOfTheSteppes.Character();

        public enum SpecialTechniques { TwoBlades, TotalProtection, FirstStrike, PowerfulStrike, Reaction, Nope };


        public string Name { get; set; }

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

        public SpecialTechniques SpecialTechnique { get; set; }
        public int Bonuses { get; set; }
        public int ExtendedDamage { get; set; }

        public void Init()
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

            SpecialTechnique = SpecialTechniques.Nope;
            Bonuses = 2;
            ExtendedDamage = 0;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                MaxAttack = this.MaxAttack,
                Attack = this.Attack,
                MaxDefence = this.MaxDefence,
                Defence = this.Defence,
                MaxEndurance = this.MaxEndurance,
                Endurance = this.Endurance,
                MaxInitiative = this.MaxInitiative,
                Initiative = this.Initiative,
                SpecialTechnique = this.SpecialTechnique,
                Bonuses = this.Bonuses,
                ExtendedDamage = this.ExtendedDamage,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                MaxAttack, Attack, MaxDefence, Defence, MaxEndurance, Endurance,
                MaxInitiative, Initiative, Bonuses, SpecialTechnique, ExtendedDamage
            );
        }

        public void Load(string saveLine)
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
            ExtendedDamage = int.Parse(save[10]);

            bool success = Enum.TryParse(save[9], out SpecialTechniques value);
            SpecialTechnique = (success ? value : SpecialTechniques.Nope);
        }
    }
}

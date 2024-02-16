using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StrikeBack
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        public enum SpecialTechniques
        {
            WithoutProtagonist,
            WithoutGameover,
            ToFirstDeathOnly,
            Werewolf,
            RumbleKnife,
            Nope
        };

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
            set
            {
                _endurance = Game.Param.Setter(value, max: MaxEndurance, _endurance, this);

                if (EnduranceSave)
                    EnduranceLoss[this.Name] = _endurance;
            }
        }
        private bool EnduranceSave { get; set; }

        private static Dictionary<string, int> EnduranceLoss = new Dictionary<string, int>();

        public List<SpecialTechniques> SpecialTechnique { get; set; }

        public string Creature { get; set; }

        public override void Init()
        {
            base.Init();

            Name = "Главный герой";
            Creature = "ГОБЛИН";
            MaxAttack = 2;
            Attack = MaxAttack;
            MaxDefence = 8;
            Defence = MaxDefence;
            MaxEndurance = 5;
            Endurance = MaxEndurance;
            EnduranceLoss.Clear();
            EnduranceSave = false;
            SpecialTechnique = new List<SpecialTechniques>();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Creature = this.Creature,
            MaxAttack = this.MaxAttack,
            Attack = this.Attack,
            MaxDefence = this.MaxDefence,
            Defence = this.Defence,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
            EnduranceSave = true,
            SpecialTechnique = new List<SpecialTechniques>(this.SpecialTechnique),
        };

        public Character SetEndurance()
        {
            if (EnduranceLoss.ContainsKey(this.Name))
                this.Endurance = EnduranceLoss[this.Name];

            return this;
        }

        public int GetEndurance() =>
            EnduranceLoss.ContainsKey(this.Name) ? EnduranceLoss[this.Name] : this.Endurance;

        public override string Save() => String.Join("|",
            MaxAttack, Attack, MaxDefence, Defence, MaxEndurance, Endurance, Creature,
            String.Join(",", EnduranceLoss.Select(x => x.Key + "=" + x.Value).ToArray()),
            String.Join(":", SpecialTechnique.ConvertAll(e => e.ToString())).TrimEnd(':'));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxAttack = int.Parse(save[0]);
            Attack = int.Parse(save[1]);
            MaxDefence = int.Parse(save[2]);
            Defence = int.Parse(save[3]);
            MaxEndurance = int.Parse(save[4]);
            Endurance = int.Parse(save[5]);
            Creature = save[6];

            EnduranceLoss.Clear();

            string[] endurances = save[7].Split(',');

            foreach (string enduranceLine in endurances)
            {
                string[] endurance = enduranceLine.Split('=');
                EnduranceLoss.Add(endurance[0], int.Parse(endurance[1]));
            }

            string[] specialTechniques = save[8].Split(':');

            foreach (string specialTechnique in specialTechniques)
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

            List<string> techniques = SpecialTechnique
                .ConvertAll(x => Constants.TechniquesNames()[x]);

            return $"\n{String.Join(", ", techniques)}";
        }
    }
}

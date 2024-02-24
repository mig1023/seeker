using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.CaptainSheltonsSecret
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
            set
            {
                _endurance = Game.Param.Setter(value, max: MaxEndurance, _endurance, this);

                if (EnduranceAutosave)
                    EnduranceLoss[this.Name] = _endurance;
            }
        }
        private bool EnduranceAutosave { get; set; }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set => _gold = Game.Param.Setter(value, _gold, this);
        }

        public int ExtendedDamage { get; set; }
        public int MasteryDamage { get; set; }
        public bool SeaArmour { get; set; }
        public List<bool> Luck { get; set; }

        private static Dictionary<string, int> EnduranceLoss = new Dictionary<string, int>();

        public override void Init()
        {
            base.Init();

            int dice = Game.Dice.Roll(dices: 2);

            Name = "Главный герой";
            MaxMastery = Constants.Mastery[dice];
            Mastery = MaxMastery;
            MaxEndurance = Constants.Endurances[dice];
            Endurance = MaxEndurance;
            Gold = 15;
            Luck = new List<bool> { false, true, true, true, true, true, true };

            for (int i = 0; i < 2; i++)
                Luck[Game.Dice.Roll()] = false;

            Game.Healing.Add(name: "Поесть", healing: 4, portions: 3);

            EnduranceLoss.Clear();
            EnduranceAutosave = false;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MaxMastery = this.MaxMastery,
            Mastery = this.Mastery,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
            Gold = this.Gold,
            ExtendedDamage = this.ExtendedDamage,
            MasteryDamage = this.MasteryDamage,
            SeaArmour = this.SeaArmour,
            EnduranceAutosave = true,
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
            MaxMastery, Mastery, MaxEndurance, Endurance, Gold, ExtendedDamage, MasteryDamage,
            (SeaArmour ? 1 : 0), String.Join(",", Luck.Select(x => x ? "1" : "0")),
            String.Join(",", EnduranceLoss.Select(x => x.Key + "=" + x.Value).ToArray())
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxMastery = int.Parse(save[0]);
            Mastery = int.Parse(save[1]);
            MaxEndurance = int.Parse(save[2]);
            Endurance = int.Parse(save[3]);
            Gold = int.Parse(save[4]);
            ExtendedDamage = int.Parse(save[5]);
            MasteryDamage = int.Parse(save[6]);
            SeaArmour = (save[7] == "1");

            Luck = save[8].Split(',').Select(x => x == "1").ToList();

            EnduranceLoss.Clear();

            string[] endurances = save[9].Split(',');

            foreach (string enduranceLine in endurances)
            {
                string[] endurance = enduranceLine.Split('=');
                EnduranceLoss.Add(endurance[0], int.Parse(endurance[1]));
            }

            IsProtagonist = true;
        }
    }
}

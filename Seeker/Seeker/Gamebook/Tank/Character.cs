using System;

namespace Seeker.Gamebook.Tank
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Driver { get; set; }
        public int Shooter { get; set; }
        public int Gunner { get; set; }

        public int StatBonuses { get; set; }

        public int VictoryPoints { get; set; }
        public int Immobilized { get; set; }
        public int Dead { get; set; }

        public override void Init()
        {
            base.Init();

            Driver = 0;
            Shooter = 0;
            Gunner = 0;
            StatBonuses = 6;
            VictoryPoints = 0;
            Immobilized = 0;
            Dead = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Driver = this.Driver,
            Shooter = this.Shooter,
            Gunner = this.Gunner,
            StatBonuses = this.StatBonuses,
            VictoryPoints = this.VictoryPoints,
            Immobilized = this.Immobilized,
            Dead = this.Dead,
        };

        public override string Save() => String.Join("|",
            Driver, Shooter, Gunner, StatBonuses, VictoryPoints, Immobilized, Dead);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Driver = int.Parse(save[0]);
            Shooter = int.Parse(save[1]);
            Gunner = int.Parse(save[2]);
            StatBonuses = int.Parse(save[3]);
            VictoryPoints = int.Parse(save[4]);
            Immobilized = int.Parse(save[5]);
            Dead = int.Parse(save[6]);

            IsProtagonist = true;
        }
    }
}

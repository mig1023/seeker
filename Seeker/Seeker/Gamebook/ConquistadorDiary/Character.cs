using System;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        public int Points { get; set; }
        public int DiegoPoints { get; set; }
        public int? Score { get; set; }
        public int CurrentBet { get; set; }
        public int LastBet { get; set; }
        public int Round { get; set; }

        public override void Init()
        {
            base.Init();

            Name = "Главный герой";
            Points = 0;
            DiegoPoints = 4;
            Score = 0;
            CurrentBet = 0;
            LastBet = 0;
            Round = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Points = this.Points,
            DiegoPoints = this.DiegoPoints,
            Score = this.Score,
            CurrentBet = this.CurrentBet,
            LastBet = this.LastBet,
            Round = this.Round,
        };

        public override string Save() =>
            String.Join("|", Points, DiegoPoints, Score, CurrentBet, Round);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Points = int.Parse(save[0]);
            DiegoPoints = int.Parse(save[1]); 
            Score = int.Parse(save[2]);
            CurrentBet = int.Parse(save[3]);
            LastBet = int.Parse(save[4]);
            Round = int.Parse(save[5]);
            IsProtagonist = true;
        }
    }
}

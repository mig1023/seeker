using System;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Points { get; set; }
        public int? Score { get; set; }
        public int CurrentBet { get; set; }

        public override void Init()
        {
            base.Init();

            Name = "Главный герой";
            Points = 0;
            Score = 0;
            CurrentBet = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Points = this.Points,
            Score = this.Score,
            CurrentBet = this.CurrentBet,
        };

        public override string Save() =>
            String.Join("|", Points, Score, CurrentBet);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Points = int.Parse(save[0]); 
            Score = int.Parse(save[1]);
            CurrentBet = int.Parse(save[2]); 
            IsProtagonist = true;
        }
    }
}

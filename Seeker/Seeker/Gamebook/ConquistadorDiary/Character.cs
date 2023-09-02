using System;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int? Score { get; set; }

        public override void Init()
        {
            base.Init();

            Name = "Главный герой";
            Score = null;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Score = this.Score,
        };

        public override string Save() => String.Join("|", Score);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Score = int.Parse(save[0]); 
            IsProtagonist = true;
        }
    }
}

using System;

namespace Seeker.Gamebook.RockOfTerror
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        public int Time { get; set; }

        public int Injury { get; set; }

        public int? MonksHeart { get; set; }

        public override void Init()
        {
            base.Init();

            Time = 0;
            Injury = 0;
            MonksHeart = null;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Time = this.Time,
            Injury = this.Injury,
            MonksHeart = this.MonksHeart,
        };

        public override string Save() => String.Join("|",
            Time, Injury, (MonksHeart == null ? -1 : MonksHeart));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Time = int.Parse(save[0]);
            Injury = int.Parse(save[1]);
            MonksHeart = int.Parse(save[2]);
            MonksHeart = (MonksHeart < 0 ? null : MonksHeart);

            IsProtagonist = true;
        }
    }
}

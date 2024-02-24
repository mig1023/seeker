using System;

namespace Seeker.Gamebook.InvisibleFront
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Dissatisfaction { get; set; }
        public int Recruitment { get; set; }


        public override void Init()
        {
            base.Init();

            Dissatisfaction = 0;
            Recruitment = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Dissatisfaction = this.Dissatisfaction,
            Recruitment = this.Recruitment,
        };

        public override string Save() =>
            String.Join("|", Dissatisfaction, Recruitment);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Dissatisfaction = int.Parse(save[0]);
            Recruitment = int.Parse(save[1]);

            IsProtagonist = true;
        }
    }
}

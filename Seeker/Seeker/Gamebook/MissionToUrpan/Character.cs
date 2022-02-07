using System;

namespace Seeker.Gamebook.MissionToUrpan
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Reputation { get; set; }

        public override void Init()
        {
            base.Init();

            Reputation = 3;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Reputation = this.Reputation,
        };

        public override string Save() => Reputation.ToString();

        public override void Load(string saveLine)
        {
            Reputation = int.Parse(saveLine);
            IsProtagonist = true;
        }
    }
}

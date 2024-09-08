using System;

namespace Seeker.Gamebook.UndergroundRoad
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Wounds { get; set; }

        public override void Init()
        {
            base.Init();

            Wounds = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Wounds = this.Wounds,
        };

        public override string Save() => String.Join("|", Wounds);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Wounds = int.Parse(save[0]);

            IsProtagonist = true;
        }
    }
}

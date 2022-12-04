using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SilverAgeSilhouette
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Rating { get; set; }
        public List<string> Verse { get; set; }

        public override void Init()
        {
            base.Init();

            Rating = 0;
            Verse = new List<string>();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Rating = this.Rating,
            Verse = this.Verse,
        };

        public override string Save() =>
            String.Join("|", Rating, String.Join("%", Verse));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Rating = int.Parse(save[0]);
            Verse = save[1].Split('%').ToList();

            IsProtagonist = true;
        }
    }
}

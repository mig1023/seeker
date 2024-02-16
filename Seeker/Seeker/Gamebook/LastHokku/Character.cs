using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.LastHokku
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        public List<string> Hokku { get; set; }

        public override void Init() =>
            Hokku = new List<string>();

        public override string Save() =>
            String.Join("|", Hokku);

        public override void Load(string saveLine) =>
            Hokku = saveLine.Split('|').ToList();
    }
}

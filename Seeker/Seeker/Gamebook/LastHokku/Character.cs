using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.LastHokku
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public List<string> Hokku { get; set; }

        public override void Init() =>
            Hokku = new List<string>();

        public override string Save() =>
            String.Join("|", Hokku);

        public override void Load(string saveLine) =>
            Hokku = saveLine.Split('|').ToList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Keperlace
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public Dictionary<string, int> MPaths { get; set; }

        public override void Init()
        {
            base.Init();

            MPaths = new Dictionary<string, int>();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            MPaths = this.MPaths,
        };

        public override string Save()
        {
            string mPath = String.Join("-", MPaths.Select(x => x.Key + "=" + x.Value));
            return String.Join("|", mPath);
        }

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            var mPaths = new Dictionary<string, int>();

            foreach (string path in save[0].Split('-'))
            {
                var pathData = path.Split('=');
                mPaths.Add(pathData[0], int.Parse(pathData[1]));
            }

            MPaths = mPaths;

            IsProtagonist = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.LastHokku
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LastHokku.Character();

        public List<string> Hokku { get; set; }
        public int Created { get; set; }

        public void Init()
        {
            Hokku = new List<string>();
            Created = 0;
        }

        public string Save() => String.Join("|", String.Join(":", Hokku), Created);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Hokku = save[0].Split('|').ToList();
            Created = int.Parse(save[1]);
        }
    }
}

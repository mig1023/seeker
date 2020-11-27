using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.RockOfTerror
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.RockOfTerror.Character();

        public string Name { get; set; }

        public int Time { get; set; }

        public int Injury { get; set; }

        public int? MonksHeart { get; set; }

        public void Init()
        {
            Time = 0;
            Injury = 0;
            MonksHeart = null;
        }

        public Character Clone()
        {
            return new Character() {
                Time = this.Time,
                Injury = this.Injury,
                MonksHeart = this.MonksHeart,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}",
                Time, Injury, (MonksHeart == null ? -1 : MonksHeart)
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Time = int.Parse(save[0]);
            Injury = int.Parse(save[1]);
            MonksHeart = int.Parse(save[2]);
            MonksHeart = (MonksHeart < 0 ? null : MonksHeart);
        }
    }
}

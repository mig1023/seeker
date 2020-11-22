using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.AdventuresOfABeardlessDeceiver.Character();

        public string Name { get; set; }

        public int Strength { get; set; }
        public int Skill { get; set; }
        public int Wisdom { get; set; }
        public int Cunning { get; set; }
        public int Oratory { get; set; }
        public int Popularity { get; set; }
        public int Tanga { get; set; }
        public int? AkynGlory { get; set; }
        public int? UnitOfTime { get; set; }

        public int StatBonuses { get; set; }

        public void Init()
        {
            Strength = 1;
            Skill = 1;
            Wisdom = 1;
            Cunning = 1;
            Oratory = 1;
            Popularity = 3;
            Tanga = 15;
            StatBonuses = 4;
            AkynGlory = null;
            UnitOfTime = null;
        }

        public Character Clone()
        {
            return new Character()
            {
                Strength = this.Strength,
                Skill = this.Skill,
                Wisdom = this.Wisdom,
                Cunning = this.Cunning,
                Oratory = this.Oratory,
                Popularity = this.Popularity,
                Tanga = this.Tanga,
                StatBonuses = this.StatBonuses,
                AkynGlory = this.AkynGlory,
                UnitOfTime = this.UnitOfTime,
            };
        }


        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}",
                Strength, Skill, Wisdom, Cunning, Oratory, Popularity, Tanga, StatBonuses,
                (AkynGlory == null ? -1 : AkynGlory), (UnitOfTime == null ? -1 : UnitOfTime)
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            Wisdom = int.Parse(save[2]);
            Cunning = int.Parse(save[3]);
            Oratory = int.Parse(save[4]);
            Popularity = int.Parse(save[5]);
            Tanga = int.Parse(save[6]);
            StatBonuses = int.Parse(save[7]);

            AkynGlory = int.Parse(save[8]);
            AkynGlory = (AkynGlory < 0 ? null : AkynGlory);

            UnitOfTime = int.Parse(save[9]);
            UnitOfTime = (UnitOfTime < 0 ? null : UnitOfTime);
        }
    }
}

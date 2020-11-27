using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.DzungarWar
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.DzungarWar.Character();

        public string Name { get; set; }

        public int Strength { get; set; }
        public int Skill { get; set; }
        public int Wisdom { get; set; }
        public int Cunning { get; set; }
        public int Oratory { get; set; }
        public int? Danger { get; set; }
        public int Tanga { get; set; }
        public int? Favour { get; set; }
        public int Tincture { get; set; }
        public int Ginseng { get; set; }

        public int StatBonuses { get; set; }
        public int MaxBonus { get; set; }
        public int Brother { get; set; }

        public void Init()
        {
            Strength = 1;
            Skill = 1;
            Wisdom = 1;
            Cunning = 1;
            Oratory = 1;
            Danger = 0;
            Tanga = 150;
            Favour = null;
            Tincture = 3;
            Ginseng = 0;
            StatBonuses = 4;
            MaxBonus = 1;
            Brother = 0;
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
                Danger = this.Danger,
                Favour = this.Favour,
                Tincture = this.Tincture,
                Ginseng = this.Ginseng,
                Tanga = this.Tanga,
                StatBonuses = this.StatBonuses,
                MaxBonus = this.MaxBonus,
                Brother = this.Brother,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
                Strength, Skill, Wisdom, Cunning, Oratory, Danger, Favour,
                Tanga, StatBonuses, MaxBonus, Brother, Tincture, Ginseng
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
            Danger = Game.Continue.IntNullableParse(save[5]);
            Favour = Game.Continue.IntNullableParse(save[6]);
            Tanga = int.Parse(save[7]);
            StatBonuses = int.Parse(save[8]);
            MaxBonus = int.Parse(save[9]);
            Brother = int.Parse(save[10]);
            Tincture = int.Parse(save[11]);
            Ginseng = int.Parse(save[12]);
        }
    }
}

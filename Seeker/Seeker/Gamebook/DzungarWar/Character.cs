using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.DzungarWar
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.DzungarWar.Character();

        private int _strength;
        public int Strength
        {
            get => _strength;
            set
            {
                if (value > 12)
                    _strength = 12;
                else if (value < 0)
                    _strength = 0;
                else
                    _strength = value;
            }
        }

        private int _skill;
        public int Skill
        {
            get => _skill;
            set
            {
                if (value > 12)
                    _skill = 12;
                else if (value < 0)
                    _skill = 0;
                else
                    _skill = value;
            }
        }

        private int _wisdom;
        public int Wisdom
        {
            get => _wisdom;
            set
            {
                if (value > 12)
                    _wisdom = 12;
                else if (value < 0)
                    _wisdom = 0;
                else
                    _wisdom = value;
            }
        }

        private int _cunning;
        public int Cunning
        {
            get => _cunning;
            set
            {
                if (value > 12)
                    _cunning = 12;
                else if (value < 0)
                    _cunning = 0;
                else
                    _cunning = value;
            }
        }

        private int _oratory;
        public int Oratory
        {
            get => _oratory;
            set
            {
                if (value > 12)
                    _oratory = 12;
                else if (value < 0)
                    _oratory = 0;
                else
                    _oratory = value;
            }
        }

        private int? _danger;
        public int? Danger
        {
            get => _danger;
            set
            {
                if (value > 12)
                    _danger = 12;
                else if (value < 0)
                    _danger = 0;
                else
                    _danger = value;
            }
        }

        public int Tanga { get; set; }
        public int? Favour { get; set; }
        public int Tincture { get; set; }
        public int Ginseng { get; set; }

        public int StatBonuses { get; set; }
        public int MaxBonus { get; set; }
        public int Brother { get; set; }

        public override void Init()
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

        public Character Clone() => new Character()
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

        public override string Save() => String.Join("|", Strength, Skill, Wisdom, Cunning, Oratory,
            Danger, Favour, Tanga, StatBonuses, MaxBonus, Brother, Tincture, Ginseng);

        public override void Load(string saveLine)
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

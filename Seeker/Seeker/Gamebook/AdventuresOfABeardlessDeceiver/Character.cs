using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.AdventuresOfABeardlessDeceiver.Character();

        public string Name { get; set; }

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

        private int _popularity;
        public int Popularity
        {
            get => _popularity;
            set
            {
                if (value > 12)
                    _popularity = 12;
                else if (value < 0)
                    _popularity = 0;
                else
                    _popularity = value;
            }
        }

        public int Kumis { get; set; }
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
            Kumis = 1;
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
                Kumis = this.Kumis,
                Tanga = this.Tanga,
                StatBonuses = this.StatBonuses,
                AkynGlory = this.AkynGlory,
                UnitOfTime = this.UnitOfTime,
            };
        }


        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                Strength, Skill, Wisdom, Cunning, Oratory, Popularity, Kumis, Tanga, StatBonuses,
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
            Kumis = int.Parse(save[6]);
            Tanga = int.Parse(save[7]);
            StatBonuses = int.Parse(save[8]);
            AkynGlory = Game.Continue.IntNullableParse(save[9]);
            UnitOfTime = Game.Continue.IntNullableParse(save[10]);
        }
    }
}

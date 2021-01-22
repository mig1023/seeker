using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.BlackCastleDungeon.Character();

        public string Name { get; set; }

        private int _mastery;
        public int MaxMastery { get; set; }
        public int Mastery
        {
            get
            {
                return _mastery;
            }
            set
            {
                if (value > MaxMastery)
                    _mastery = MaxMastery;
                else if (value < 0)
                    _mastery = 0;
                else
                    _mastery = value;
            }
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get
            {
                return _endurance;
            }
            set
            {
                if (value > MaxEndurance)
                    _endurance = MaxEndurance;
                else if (value < 0)
                    _endurance = 0;
                else
                    _endurance = value;
            }
        }

        private int _luck;
        public int MaxLuck { get; set; }
        public int Luck
        {
            get
            {
                return _luck;
            }
            set
            {
                if (value > MaxLuck)
                    _luck = MaxLuck;
                else if (value < 0)
                    _luck = 0;
                else
                    _luck = value;
            }
        }

        private int _gold;
        public int Gold
        {
            get
            {
                return _gold;
            }
            set
            {
                if (value < 0)
                    _gold = 0;
                else
                    _gold = value;
            }
        }

        public List<string> Spells { get; set; }
        public int SpellSlots { get; set; }

        public void Init()
        {
            MaxMastery = Game.Dice.Roll() + 6;
            Mastery = MaxMastery;
            MaxEndurance = Game.Dice.Roll(dices: 2) + 12;
            Endurance = MaxEndurance;
            MaxLuck = Game.Dice.Roll() + 6;
            Luck = MaxLuck;
            Gold = 15;
            SpellSlots = 10;
            Spells = new List<string>();
        }

        public Character Clone()
        {
            return new Character()
            {
                Name = this.Name,
                MaxMastery = this.MaxMastery,
                Mastery = this.Mastery,
                MaxEndurance = this.MaxEndurance,
                Endurance = this.Endurance,
                MaxLuck = this.MaxLuck,
                Luck = this.Luck,
                Gold = this.Gold,
                SpellSlots = this.SpellSlots,
                Spells = new List<string>(),
            };
        }

        public string Save()
        {
            string spells = String.Join(",", Spells);

            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                MaxMastery, Mastery, MaxEndurance, Endurance, MaxLuck, Luck, Gold, SpellSlots, spells
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxMastery = int.Parse(save[0]);
            Mastery = int.Parse(save[1]);
            MaxEndurance = int.Parse(save[2]);
            Endurance = int.Parse(save[3]);
            MaxLuck = int.Parse(save[4]);
            Luck = int.Parse(save[5]);
            Gold = int.Parse(save[6]);
            SpellSlots = int.Parse(save[7]);
            Spells = save[8].Split(',').ToList();
        }
    }
}

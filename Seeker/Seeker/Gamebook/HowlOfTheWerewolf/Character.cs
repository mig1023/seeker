using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.HowlOfTheWerewolf.Character();

        public string Name { get; set; }

        private int _mastery;
        public int MaxMastery { get; set; }
        public int Mastery
        {
            get => _mastery;
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
            get => _endurance;
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
            get => _luck;
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

        private int _change;
        public int Change
        {
            get => _change;
            set
            {
                if ((_change > 0) && (value < 1))
                    _change = 1;
                else if (value < 0)
                    _change = 0;
                else
                    _change = value;
            }
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set
            {
                if (value < 0)
                    _gold = 0;
                else
                    _gold = value;
            }
        }

        public int WayBack { get; set; }

        public void Init()
        {
            Name = String.Empty;

            MaxMastery = (int)Math.Ceiling(Game.Dice.Roll() / 2.0) + 7;
            Mastery = MaxMastery;
            MaxEndurance = Game.Dice.Roll(dices: 2) + 10;
            Endurance = MaxEndurance;
            MaxLuck = Game.Dice.Roll() + 6;
            Luck = MaxLuck;
            Change = 0;
            Gold = 15;
            WayBack = 0;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                MaxMastery = this.MaxMastery,
                Mastery = this.Mastery,
                MaxEndurance = this.MaxEndurance,
                Endurance = this.Endurance,
                MaxLuck = this.MaxLuck,
                Luck = this.Luck,
                Change = this.Change,
                Gold = this.Gold,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                MaxMastery, Mastery, MaxEndurance, Endurance, MaxLuck, Luck, Change, Gold, WayBack
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
            Change = int.Parse(save[6]);
            Gold = int.Parse(save[7]);
            WayBack = int.Parse(save[8]);
        }
    }
}

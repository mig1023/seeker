using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xamarin.Forms;

namespace Seeker.Gamebook.SeasOfBlood
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _teamStrength;
        public int TeamStrength
        {
            get => _teamStrength;
            set => _teamStrength = Game.Param.Setter(value, _teamStrength, this);
        }

        private int _teamSize;
        public int MaxTeamSize { get; set; }
        public int TeamSize
        {
            get => _teamSize;
            set => _teamSize = Game.Param.Setter(value, max: MaxTeamSize, _teamSize, this);
        }

        private int _mastery;
        public int MaxMastery { get; set; }
        public int Mastery
        {
            get => _mastery;
            set => _mastery = Game.Param.Setter(value, max: MaxMastery, _mastery, this);
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, max: MaxEndurance, _endurance, this);
        }

        private int _luck;
        public int MaxLuck { get; set; }
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, max: MaxLuck, _luck, this);
        }

        private int _logbook;
        public int Logbook
        {
            get => _logbook;
            set => _logbook = Game.Param.Setter(value, max: 50, _logbook, this);
        }

        private int _coins;
        public int Coins
        {
            get => _coins;
            set => _coins = Game.Param.Setter(value, _coins, this);
        }
        
        private int _spoils;
        public int Spoils
        {
            get => _spoils;
            set => _spoils = Game.Param.Setter(value, _spoils, this);
        }

        public override void Init()
        {
            base.Init();

            TeamStrength = Game.Dice.Roll() + 6;
            MaxTeamSize = Game.Dice.Roll(dices: 2) + 6;
            TeamSize = MaxTeamSize;

            MaxMastery = Game.Dice.Roll() + 6;
            Mastery = MaxMastery;
            MaxEndurance = Game.Dice.Roll(dices: 2) + 12;
            Endurance = MaxEndurance;
            MaxLuck = Game.Dice.Roll() + 6;
            Luck = MaxLuck;
            
            Logbook = 0;
            Coins = 20;
            Spoils = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            TeamStrength = this.TeamStrength,
            MaxTeamSize = this.MaxTeamSize,
            TeamSize = this.TeamSize,
            MaxMastery = this.MaxMastery,
            Mastery = this.Mastery,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
            MaxLuck = this.MaxLuck,
            Luck = this.Luck,
        };

        public override string Save() => String.Join("|",
            TeamStrength, MaxTeamSize, TeamSize, MaxMastery,
            Mastery, MaxEndurance, Endurance, MaxLuck, Luck
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            TeamStrength = int.Parse(save[0]);
            MaxTeamSize = int.Parse(save[1]);
            TeamSize = int.Parse(save[2]);
            MaxMastery = int.Parse(save[3]);
            Mastery = int.Parse(save[4]);
            MaxEndurance = int.Parse(save[5]);
            Endurance = int.Parse(save[6]);
            MaxLuck = int.Parse(save[7]);
            Luck = int.Parse(save[8]);

            IsProtagonist = true;
        }
    }
}

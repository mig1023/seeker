using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.YounglingTournament
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public enum ForcesTypes
        {
            Speed,
            Push,
            Attraction,
            Jump,
            Foresight,
            Conceal,
            Sight,
            StrengtheningTheBody,
            Healing,
            Suffocation
        }

        public enum SwordTypes
        {
            Decisiveness,
            Elasticity,
            Rivalry,
            Perseverance,
            Aggressiveness,
            Confidence,
            Vaapad,
            JarKai
        }

        private int _lightSide;
        public int LightSide
        {
            get => _lightSide;
            set => _lightSide = Game.Param.Setter(value);
        }

        private int _darkSide;
        public int DarkSide
        {
            get => _darkSide;
            set => _darkSide = Game.Param.Setter(value);
        }

        private int _hitpoints;
        public int MaxHitpoints { get; set; }
        public int Hitpoints
        {
            get => _hitpoints;
            set
            {
                _hitpoints = Game.Param.Setter(value, max: MaxHitpoints, _hitpoints, this);

                if (HitpointAutosave)
                    HitpointsLoss[this.Name] = _hitpoints;
            }
        }

        private int _accuracy;
        public int Accuracy
        {
            get => _accuracy;
            set => _accuracy = Game.Param.Setter(value);
        }

        private int _pilot;
        public int Pilot
        {
            get => _pilot;
            set => _pilot = Game.Param.Setter(value);
        }

        private int _stealth;
        public int Stealth
        {
            get => _stealth;
            set => _stealth = Game.Param.Setter(value);
        }

        private int _hacking;
        public int Hacking
        {
            get => _hacking;
            set => _hacking = Game.Param.Setter(value);
        }

        private int _shield;
        public int Shield
        {
            get => _shield;
            set => _shield = Game.Param.Setter(value);
        }

        private int _firepower;
        public int Firepower
        {
            get => _firepower;
            set => _firepower = Game.Param.Setter(value);
        }

        private int _skill;
        public int Skill
        {
            get => _skill;
            set => _skill = Game.Param.Setter(value);
        }

        private int _rang;
        public int Rang
        {
            get => _rang;
            set => _rang = Game.Param.Setter(value);
        }

        private int _speed;
        public int Speed
        {
            get => _speed;
            set => _speed = Game.Param.Setter(value);
        }

        public int Reading { get; set; }
        public int SecondPart { get; set; }
        public string SwordTechnique { get; set; }
        private bool HitpointAutosave { get; set; } 

        public int Thrust { get; set; }
        public int EnemyThrust { get; set; }

        public SortedDictionary<ForcesTypes, int> ForceTechniques { get; set; }
        public SortedDictionary<SwordTypes, int> SwordTechniques { get; set; }
        public List<int> ForceTechniquesOrder { get; set; }

        private static Dictionary<string, int> HitpointsLoss = new Dictionary<string, int>();
        

        public override void Init()
        {
            base.Init();

            LightSide = 100;
            DarkSide = 0;
            MaxHitpoints = 30;
            Hitpoints = MaxHitpoints;
            Accuracy = 10;
            Pilot = 1;
            Stealth = 1;
            Hacking = 1;
            Shield = 0;
            Firepower = 5;
            WayBack = 0;
            Thrust = 0;
            EnemyThrust = 0;
            Reading = 0;
            SecondPart = 0;

            ForceTechniques = new SortedDictionary<ForcesTypes, int>
            {
                [ForcesTypes.Speed] = 1,
                [ForcesTypes.Push] = 1,
                [ForcesTypes.Attraction] = 1,
                [ForcesTypes.Jump] = 1,
                [ForcesTypes.Foresight] = 1,
                [ForcesTypes.Conceal] = 1,
                [ForcesTypes.Sight] = 1,
                [ForcesTypes.StrengtheningTheBody] = 0,
                [ForcesTypes.Healing] = 0,
                [ForcesTypes.Suffocation] = 0,
            };

            SwordTechniques = new SortedDictionary<SwordTypes, int>
            {
                [SwordTypes.Decisiveness] = 4,
                [SwordTypes.Elasticity] = 1,
                [SwordTypes.Rivalry] = 1,
                [SwordTypes.Perseverance] = 0,
                [SwordTypes.Aggressiveness] = 0,
                [SwordTypes.Confidence] = 0,
                [SwordTypes.Vaapad] = 0,
                [SwordTypes.JarKai] = 0,
            };

            HitpointsLoss.Clear();
            HitpointAutosave = false;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            LightSide = this.LightSide,
            DarkSide = this.DarkSide,
            MaxHitpoints = this.MaxHitpoints,
            Hitpoints = this.Hitpoints,
            Accuracy = this.Accuracy,
            Pilot = this.Pilot,
            Stealth = this.Stealth,
            Hacking = this.Hacking,
            Shield = this.Shield,
            Firepower = this.Firepower,
            Skill = this.Skill,
            Rang = this.Rang,
            Speed = this.Speed,
            SwordTechnique = this.SwordTechnique,
            HitpointAutosave = true,
            Reading = this.Reading,
            SecondPart = this.SecondPart,
        };

        public Character SetHitpoints(int hitpointsPenalty = 0)
        {
            if (HitpointsLoss.ContainsKey(this.Name))
            {
                if (hitpointsPenalty > 0)
                    HitpointsLoss[this.Name] -= hitpointsPenalty;

                this.Hitpoints = HitpointsLoss[this.Name];
            }

            return this;
        }

        public static void SetHitpoints(string name, int value, int defaultInitValue)
        {
            if (HitpointsLoss.ContainsKey(name))
                HitpointsLoss[name] -= value;
            else
                HitpointsLoss.Add(name, (defaultInitValue - value));
        }

        public int GetHitpoints(int hitpointsPenalty = 0) =>
            (HitpointsLoss.ContainsKey(this.Name) ? HitpointsLoss[this.Name] : this.Hitpoints) - hitpointsPenalty;

        public override string Save() => String.Join("|",
            LightSide, DarkSide, MaxHitpoints, Hitpoints, Accuracy, Pilot, Stealth, Hacking, Firepower, WayBack,
            String.Join(",", ForceTechniques.Values), String.Join(",", SwordTechniques.Values),
            String.Join(",", HitpointsLoss.Select(x => x.Key + "=" + x.Value).ToArray()),
            Thrust, EnemyThrust, Reading, SecondPart);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            LightSide = int.Parse(save[0]);
            DarkSide = int.Parse(save[1]);
            MaxHitpoints = int.Parse(save[2]);
            Hitpoints = int.Parse(save[3]);
            Accuracy = int.Parse(save[4]);
            Pilot = int.Parse(save[5]);
            Stealth = int.Parse(save[6]);
            Hacking = int.Parse(save[7]);
            Firepower = int.Parse(save[8]);
            WayBack = int.Parse(save[9]);

            string [] forces = save[10].Split(',');

            ForceTechniques = new SortedDictionary<ForcesTypes, int>
            {
                [ForcesTypes.Speed] = int.Parse(forces[0]),
                [ForcesTypes.Push] = int.Parse(forces[1]),
                [ForcesTypes.Attraction] = int.Parse(forces[2]),
                [ForcesTypes.Jump] = int.Parse(forces[3]),
                [ForcesTypes.Foresight] = int.Parse(forces[4]),
                [ForcesTypes.Conceal] = int.Parse(forces[5]),
                [ForcesTypes.Sight] = int.Parse(forces[6]),
                [ForcesTypes.StrengtheningTheBody] = int.Parse(forces[7]),
                [ForcesTypes.Healing] = int.Parse(forces[8]),
                [ForcesTypes.Suffocation] = int.Parse(forces[9]),
            };

            string[] sword = save[11].Split(',');

            SwordTechniques = new SortedDictionary<SwordTypes, int>
            {
                [SwordTypes.Decisiveness] = int.Parse(sword[0]),
                [SwordTypes.Elasticity] = int.Parse(sword[1]),
                [SwordTypes.Rivalry] = int.Parse(sword[2]),
                [SwordTypes.Perseverance] = int.Parse(sword[3]),
                [SwordTypes.Aggressiveness] = int.Parse(sword[4]),
                [SwordTypes.Confidence] = int.Parse(sword[5]),
                [SwordTypes.Vaapad] = int.Parse(sword[6]),
                [SwordTypes.JarKai] = int.Parse(sword[7]),
            };

            HitpointsLoss.Clear();

            string[] hitpoints = save[12].Split(',');

            if (hitpoints.Length > 0)
            {
                foreach (string hitpointLine in hitpoints)
                {
                    if (String.IsNullOrEmpty(hitpointLine))
                        continue;

                    string[] hitpoint = hitpointLine.Split('=');
                    HitpointsLoss.Add(hitpoint[0], int.Parse(hitpoint[1]));
                }
            }

            Thrust = int.Parse(save[13]);
            EnemyThrust = int.Parse(save[14]);
            Reading = int.Parse(save[15]);
            SecondPart = int.Parse(save[16]);

            IsProtagonist = true;
        }
    }
}

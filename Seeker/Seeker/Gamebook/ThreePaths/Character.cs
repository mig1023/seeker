﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.ThreePaths
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.ThreePaths.Character();

        public string Name { get; set; }
        public int? Time { get; set; }
        public List<string> Spells { get; set; }
        public int SpellSlots { get; set; }

        public void Init()
        {
            Time = null;
            SpellSlots = 9;
            Spells = new List<string>();
        }

        public Character Clone() => new Character()
        {
            Time = this.Time,
            Spells = new List<string>(),
        };

        public string Save() => String.Join("|", Time, SpellSlots, String.Join(",", Spells));

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Time = Game.Continue.IntNullableParse(save[0]);
            SpellSlots = int.Parse(save[1]);
            Spells = save[2].Split(',').ToList();
        }
    }
}

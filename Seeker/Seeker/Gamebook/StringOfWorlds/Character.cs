using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.StringOfWorlds.Character();

        public string Name { get; set; }

        public int Mastery { get; set; }
        public int Endurance { get; set; }
        public int Luck { get; set; }
        public int Gold { get; set; }
        public List<string> Spells { get; set; }
        public int SpellSlots { get; set; }

        public void Init()
        {
            Mastery = Game.Dice.Roll() + 6;
            Endurance = Game.Dice.Roll(dices: 2) + 12;
            Luck = Game.Dice.Roll() + 6;
            Gold = 15;
            SpellSlots = 10;
            Spells = new List<string>();
        }

        public Character Clone()
        {
            return new Character()
            {
                Name = this.Name,
                Mastery = this.Mastery,
                Endurance = this.Endurance,
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
                "{0}|{1}|{2}|{3}|{4}|{5}",
                Mastery, Endurance, Luck, Gold, SpellSlots, spells
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Mastery = int.Parse(save[0]);
            Endurance = int.Parse(save[1]);
            Luck = int.Parse(save[2]);
            Gold = int.Parse(save[3]);
            SpellSlots = int.Parse(save[4]);
            Spells = save[5].Split(',').ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public List<Character> Enemies { get; set; }
        public bool ThisIsSpell { get; set; }

        public Character.SpecializationType? Specialization { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Жизни: {0}", Character.Protagonist.Hitpoints),
            String.Format("Обращений: {0}", Character.Protagonist.Transformation),
            String.Format("Золото: {0}", Character.Protagonist.Gold),
        };

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (Character.Protagonist.Spells == null)
                return null;

            foreach (string spell in Character.Protagonist.Spells)
            {
                if (currentSpells.ContainsKey(spell.ToLower()))
                    currentSpells[spell.ToLower()] += 1;
                else
                    currentSpells.Add(spell.ToLower(), 1);
            }

            if (currentSpells.Count <= 0)
                return null;

            List<string> statusLines = new List<string>();

            foreach (string spell in currentSpells.Keys.ToList().OrderBy(x => x))
                statusLines.Add(String.Format("{0}: {1}", spell.ToUpper(), currentSpells[spell]));

            return statusLines;
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Other.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { String.Format("{0}, {1} {2}", Text, Price, gold) };
            }
            else if ((Name == "Get") && ThisIsSpell)
            {
                int count = Character.Protagonist.Spells.Where(x => x == Text).Count();
                return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" ({0} шт)", count) : String.Empty)) };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nсила {1}  жизни {2}", enemy.Name, enemy.Strength, enemy.Hitpoints));

            return enemies;
        }

        public override bool IsButtonEnabled()
        {
            bool bySpell = ThisIsSpell && (Character.Protagonist.Magicpoints <= 0);
            bool bySpecButton = (Specialization != null) && (Character.Protagonist.Specialization != Character.SpecializationType.Nope);
            bool byPrice = (Price > 0) && (Character.Protagonist.Gold < Price);

            return !(bySpell || bySpecButton || byPrice);
        }

        public List<string> Get()
        {
            if (ThisIsSpell && (Character.Protagonist.Magicpoints >= 1))
            {
                Character.Protagonist.Spells.Add(Text);
                Character.Protagonist.Magicpoints -= 1;
            }
            else if ((Specialization != null) && (Character.Protagonist.Specialization == Character.SpecializationType.Nope))
            {
                Character.Protagonist.Specialization = Specialization ?? Character.SpecializationType.Nope;

                if (Specialization == Character.SpecializationType.Warrior)
                    Character.Protagonist.Strength += 1;

                else if (Specialization == Character.SpecializationType.Thrower)
                    Character.Protagonist.Magicpoints += 1;

                else
                {
                    Character.Protagonist.Magicpoints += 2;
                    Character.Protagonist.Transformation += 2;
                }
            }

            else if ((Price > 0) && (Character.Protagonist.Gold >= Price))
                Character.Protagonist.Gold -= Price;

            return new List<string> { "RELOAD" };
        }
    }
}

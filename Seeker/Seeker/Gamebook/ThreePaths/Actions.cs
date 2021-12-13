using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ThreePaths
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public bool ThisIsSpell { get; set; }

        public override List<string> Status()
        {
            if (protagonist.Time == null)
                return null;
                
            return new List<string> { String.Format("Время: {0:d2}:00", protagonist.Time) };
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool bySpellAdd = ThisIsSpell && (protagonist.SpellSlots <= 0) && !secondButton;
            bool bySpellRemove = ThisIsSpell && !protagonist.Spells.Contains(Text) && secondButton;

            return !(bySpellAdd || bySpellRemove);
        }

        public List<string> Get()
        {
            protagonist.Spells.Add(Text);
            protagonist.SpellSlots -= 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease()
        {
            protagonist.Spells.Remove(Text);
            protagonist.SpellSlots += 1;

            return new List<string> { "RELOAD" };
        }

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(option);

                        if (oneOption.Contains("ВРЕМЯ <") && (level <= protagonist.Time))
                            return false;

                        if (oneOption.Contains("ВРЕМЯ >=") && (level > protagonist.Time))
                            return false;
                    }
                    else if (oneOption.Contains("ЗАКЛЯТИЕ"))
                    {
                        return protagonist.Spells.Contains(oneOption.Trim());
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public override List<string> Representer()
        {
            int count = protagonist.Spells.Where(x => x == Text).Count();

            return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" ({0} шт)", count) : String.Empty)) };
        }
    }
}

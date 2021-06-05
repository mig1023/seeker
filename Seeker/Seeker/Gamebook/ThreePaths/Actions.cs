using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.ThreePaths
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public bool ThisIsSpell { get; set; }

        public override List<string> Status()
        {
            if (Character.Protagonist.Time == null)
                return null;
                
            return new List<string> { String.Format("Время: {0:d2}:00", Character.Protagonist.Time) };
        }

        public override bool IsButtonEnabled() => !(ThisIsSpell && (Character.Protagonist.SpellSlots <= 0));

        public List<string> Get()
        {
            Character.Protagonist.Spells.Add(Text);
            Character.Protagonist.SpellSlots -= 1;

            return new List<string> { "RELOAD" };
        }

        public override bool CheckOnlyIf(string option)
        {
            foreach (string oneOption in option.Split(','))
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    int level = Game.Other.LevelParse(option);

                    if (oneOption.Contains("ВРЕМЯ <") && (level <= Character.Protagonist.Time))
                        return false;

                    if (oneOption.Contains("ВРЕМЯ >=") && (level > Character.Protagonist.Time))
                        return false;
                }
                else if (oneOption.Contains("ЗАКЛЯТИЕ"))
                    return Character.Protagonist.Spells.Contains(oneOption.Trim());

                else if (oneOption.Contains("!"))
                {
                    if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                        return false;
                }
                else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                    return false;
            }

            return true;
        }

        public override List<string> Representer()
        {
            int count = Character.Protagonist.Spells.Where(x => x == Text).Count();

            return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" (x{0})", count) : String.Empty)) };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ThreePaths
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public bool ThisIsSpell { get; set; }

        public override List<string> Status()
        {
            if (protogonist.Time == null)
                return null;
                
            return new List<string> { String.Format("Время: {0:d2}:00", protogonist.Time) };
        }

        public override bool IsButtonEnabled() => !(ThisIsSpell && (protogonist.SpellSlots <= 0));

        public List<string> Get()
        {
            protogonist.Spells.Add(Text);
            protogonist.SpellSlots -= 1;

            return new List<string> { "RELOAD" };
        }

        public override bool CheckOnlyIf(string option)
        {
            foreach (string oneOption in option.Split(','))
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    int level = Game.Other.LevelParse(option);

                    if (oneOption.Contains("ВРЕМЯ <") && (level <= protogonist.Time))
                        return false;

                    if (oneOption.Contains("ВРЕМЯ >=") && (level > protogonist.Time))
                        return false;
                }
                else if (oneOption.Contains("ЗАКЛЯТИЕ"))
                    return protogonist.Spells.Contains(oneOption.Trim());

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
            int count = protogonist.Spells.Where(x => x == Text).Count();

            return new List<string> { String.Format("{0}{1}", Text, (count > 0 ? String.Format(" (x{0})", count) : String.Empty)) };
        }
    }
}

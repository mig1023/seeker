using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.ThreePaths
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }
        public bool Init { get; set; }

        public void Do()
        {
            if (Name == "Trigger")
                Game.Option.Trigger(Value.ToString());

            else if (Name == "Time")
            {
                if (Init)
                    Character.Protagonist.Time = 0;
                else
                    Character.Protagonist.Time += Value;
            }

            else if (Name == "RemoveSpell")
                Character.Protagonist.Spells.RemoveAt(Character.Protagonist.Spells.IndexOf(ValueString));
        }
    }
}

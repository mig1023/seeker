using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "RemoveSpell")
                Character.Protagonist.Spells.RemoveAt(Character.Protagonist.Spells.IndexOf(ValueString));
            else
                InnerDo(Character.Protagonist);
        }
    }
}

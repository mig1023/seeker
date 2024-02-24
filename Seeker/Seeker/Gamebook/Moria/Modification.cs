using System;

namespace Seeker.Gamebook.Moria
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "GandalfCastSpell")
            {
                Character.Protagonist.MagicPause = 4;
            }
            else if (Name == "Death")
            {
                Character.Protagonist.Fellowship.Remove(ValueString);
            }
            else if (Name == "WayBack")
            {
                Character.Protagonist.WayBack = Value;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

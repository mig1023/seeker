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
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

using System;

namespace Seeker.Gamebook.Moria
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "GandalfCastSpell")
            {
                protagonist.MagicPause = 4;
            }
            else if (Name == "Death")
            {
                protagonist.Fellowship.Remove(ValueString);
            }
            else if (Name == "WayBack")
            {
                protagonist.WayBack = Value;
            }
            else
            {
                base.Do(protagonist);
            }
        }
    }
}

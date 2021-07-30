using System;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthRestore")
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;

            else
                InnerDo(Character.Protagonist);
        }
    }
}

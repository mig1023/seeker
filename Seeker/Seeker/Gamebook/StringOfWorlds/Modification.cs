using System;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthRestore")
            {
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;
            }
            else if (Name == "CapeProtect")
            {
                Character.Protagonist.Strength += (Game.Option.IsTriggered("Плащ") ? 1 : -1);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

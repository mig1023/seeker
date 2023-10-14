using System;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Cleaning")
            {
                Game.Data.Clean(reStart: true);
            }
            else if (Name == "RemoveSpell")
            {
                Character.Protagonist.Spells.RemoveAt(Character.Protagonist.Spells.IndexOf(ValueString));
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

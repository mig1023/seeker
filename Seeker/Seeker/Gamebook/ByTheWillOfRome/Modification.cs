using System;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "RemoveTrigger")
                Game.Option.Trigger(ValueString, remove: true);
            else
                base.Do(Character.Protagonist);
        }
    }
}

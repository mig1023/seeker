using System;

namespace Seeker.Gamebook.PrisonerOfMoritaiCastle
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Signal")
            {
                if (Game.Option.IsTriggered("сигнал"))
                    Game.Option.Trigger("пожар");
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

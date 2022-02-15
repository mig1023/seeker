using System;

namespace Seeker.Gamebook.Sheriff
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Level")
            {
                Character.Protagonist.Whoosh += Constants.Levels()[ValueString];
            }
            else if (Name == "CleanNotebook")
            {
                foreach (string clean in Constants.CleaningNotebookList())
                    Game.Option.Trigger(clean, remove: true);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

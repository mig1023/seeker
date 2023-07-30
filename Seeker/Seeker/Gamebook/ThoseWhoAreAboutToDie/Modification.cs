using System;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "ReactionByReaction")
            {
                if (Character.Protagonist.Reaction <= Value)
                    Character.Protagonist.Reaction -= 2;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

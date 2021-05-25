using System;
using System.Collections.Generic;
using System.Text;

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
                InnerDo(Character.Protagonist);
        }
    }
}

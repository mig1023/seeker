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
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}

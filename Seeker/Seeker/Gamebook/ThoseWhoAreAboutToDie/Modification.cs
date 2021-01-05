using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public void Do()
        {
            if (Name == "ReactionByReaction")
            {
                if (Character.Protagonist.Reaction <= Value)
                    Character.Protagonist.Reaction -= 2;

                if (Character.Protagonist.Reaction < 0)
                    Character.Protagonist.Reaction = 0;
            }
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                if (currentValue > 12)
                    currentValue = 12;
                else if (currentValue < 0)
                    currentValue = 0;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}

using System.Collections.Generic;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protogonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "Spell")
            {
                protogonist.Spells.Remove(ValueString);
                protogonist.Hitpoints -= 2;
            }

            else if (Name == "RestoreSpells")
                protogonist.Spells = new List<string>(protogonist.SpellsReplica);
            
            else if (Name == "HealingByVessel")
                Game.Healing.Add("Выпить отвар из бурдюка,8");

            else if (Name == "Transformation")
            {
                protogonist.Transformation -= 1;
                protogonist.Hitpoints -= (Game.Data.Triggers.Contains("PainfulTransformation") ? 3 : 2);
            }

            else if (Name == "NoMoreMagic")
            {
                protogonist.Spells.Clear();
                protogonist.Transformation = 0;
            }

            else
                base.Do(protogonist);
        }
    }
}

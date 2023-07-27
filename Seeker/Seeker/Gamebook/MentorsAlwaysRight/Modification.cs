using System.Collections.Generic;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "Spell")
            {
                protagonist.Spells.Remove(ValueString);
                protagonist.Hitpoints -= 2;
            }
            else if (Name == "RestoreSpells")
            {
                protagonist.Spells = new List<string>(protagonist.SpellsReplica);
            }
            else if (Name == "HealingByVessel")
            {
                Game.Healing.Add("Выпить отвар из бурдюка,8");
            }
            else if (Name == "AddTransformation")
            {
                protagonist.Transformation += Value;
            }
            else if (Name == "Transformation")
            {
                protagonist.Transformation -= 1;
                protagonist.Hitpoints -= (Game.Option.IsTriggered("PainfulTransformation") ? 3 : 2);
            }
            else if (Name == "NoMoreMagic")
            {
                protagonist.Spells.Clear();
                protagonist.Transformation = 0;
            }
            else
            {
                base.Do(protagonist);
            }
        }
    }
}

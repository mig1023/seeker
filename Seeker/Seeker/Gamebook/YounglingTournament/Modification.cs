using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.YounglingTournament
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Back")
            {
                Character.Protagonist.WayBack = Value;
            }
            else if (Name == "NewForceTechniquesOrder")
            {
                Character.Protagonist.ForceTechniquesOrder = NewForceTechniquesOrder();
            }    
            else if (Enum.IsDefined(typeof(Character.ForcesTypes), Name))
            {
                Enum.TryParse(Name, out Character.ForcesTypes technique);
                Character.Protagonist.ForceTechniques[technique] =
                    AddTechnique(Character.Protagonist.ForceTechniques[technique], Value);
            }
            else if (Enum.IsDefined(typeof(Character.SwordTypes), Name))
            {
                Enum.TryParse(Name, out Character.SwordTypes technique);
                Character.Protagonist.SwordTechniques[technique] =
                    AddTechnique(Character.Protagonist.SwordTechniques[technique], Value);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }

        private int AddTechnique(int technique, int value)
        {
            technique += value;

            if (technique > Constants.GetMaxTechniqueValue())
                technique = Constants.GetMaxTechniqueValue();

            return technique;
        }

        private List<int> NewForceTechniquesOrder()
        {
            List<int> forceTechniquesOrder = new List<int> { 1, 2 };

            if (Character.Protagonist.ForceTechniques[Character.ForcesTypes.Suffocation] > 0)
                forceTechniquesOrder.Add(3);

            for (int i = 0; i < forceTechniquesOrder.Count; i++)
            {
                int newPosition = Game.Dice.Roll(size: forceTechniquesOrder.Count) - 1;

                int replace = forceTechniquesOrder[newPosition];
                forceTechniquesOrder[newPosition] = forceTechniquesOrder[i];
                forceTechniquesOrder[i] = replace;
            }

            return forceTechniquesOrder;
        }
    }
}

using System;

namespace Seeker.Gamebook.YounglingTournament
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Enum.IsDefined(typeof(Character.ForcesTypes), Name))
            {
                Enum.TryParse(Name, out Character.ForcesTypes technique);
                Character.Protagonist.ForceTechniques[technique] += Value;
            }

            else if (Enum.IsDefined(typeof(Character.SwordTypes), Name))
            {
                Enum.TryParse(Name, out Character.SwordTypes technique);
                Character.Protagonist.SwordTechniques[technique] += Value;
            }

            else
                base.Do(Character.Protagonist);
        }
    }
}

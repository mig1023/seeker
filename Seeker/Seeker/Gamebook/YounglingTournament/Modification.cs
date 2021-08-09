using System;

namespace Seeker.Gamebook.YounglingTournament
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Enum.IsDefined(typeof(Character.Techniques), Name))
            {
                Enum.TryParse(Name, out Character.Techniques technique);
                Character.Protagonist.ForceTechniques[technique] += Value;
            }
            else
                base.Do(Character.Protagonist);
        }
    }
}

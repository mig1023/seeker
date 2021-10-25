using System;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (!ModBySpecificName(Name))
                base.Do(Character.Protagonist);
        }

        private bool ModBySpecificName(string name)
        {
            Character protagonist = Character.Protagonist;

            if (name == "CrazyDance")
            {
                protagonist.Heroism += 6;
                protagonist.Inspiration += 6;
            }
            else
                return false;

            return true;
        }
    }
}

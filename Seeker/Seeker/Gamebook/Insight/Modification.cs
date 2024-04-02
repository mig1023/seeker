using System;

namespace Seeker.Gamebook.Insight
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public int? BodyArmour { get; set; }

        public override void Do()
        {
            if ((Name == "Life") && (BodyArmour != null) && Game.Option.IsTriggered("бронежилет"))
                Value = BodyArmour.Value;

            base.Do(Character.Protagonist);
        }
    }
}

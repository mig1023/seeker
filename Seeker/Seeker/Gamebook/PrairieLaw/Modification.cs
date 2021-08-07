using System;

namespace Seeker.Gamebook.PrairieLaw
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthByPoison")
                DoByTrigger("Противоядие", () => Character.Protagonist.Strength -= 10, not: true);

            else if (Name == "StrengthByBlanket")
                Character.Protagonist.Strength += (Game.Data.Triggers.Contains("Одеяло") ? 1 : -1);

            else if (Name == "StrengthByBlanketOrMatches")
            {
                bool blanketOrMatches = Game.Data.Triggers.Contains("Одеяло") || Game.Data.Triggers.Contains("Спички");
                Character.Protagonist.Strength += (blanketOrMatches ? 1 : -1);
            }

            else if (Name == "Skin")
                DoByTrigger("Нож", () => Character.Protagonist.AnimalSkins.Add(ValueString));

            else
                base.Do(Character.Protagonist);
        }
    }
}

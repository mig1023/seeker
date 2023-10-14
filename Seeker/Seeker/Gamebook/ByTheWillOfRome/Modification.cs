using System;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Untrigger")
            {
                Game.Option.Trigger(ValueString, remove: true);
            }
            else if (Name == "FishFood")
            {
                if (Character.Protagonist.Sestertius >= 100)
                {
                    Character.Protagonist.Sestertius -= 100;
                }
                else
                {
                    Game.Option.Trigger("Рабы", remove: true);
                }

                Game.Option.Trigger("Еда №2", remove: true);
            }
            else if (Name == "DisciplineFood")
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (Game.Option.IsTriggered("Еда №{i}"))
                        return;
                }

                Character.Protagonist.Discipline -= 1;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

using System;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "RemoveTrigger")
            {
                Game.Option.Trigger(ValueString, remove: true);
            }
            else if (Name == "FishFood")
            {
                if (Character.Protagonist.Sestertius >= 100)
                    Character.Protagonist.Sestertius -= 100;
                else
                    Game.Option.Trigger("Рабы", remove: true);

                Game.Option.Trigger("Еда №2", remove: true);
            }
            else if (Name == "DisciplineFood")
            {
                if (!Game.Option.IsTriggered("Еда №1") && !Game.Option.IsTriggered("Еда №2") && !Game.Option.IsTriggered("Еда №3"))
                    Character.Protagonist.Discipline -= 1;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

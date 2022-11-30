using System;

namespace Seeker.Gamebook.SilverAgeSilhouette
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private void TriggerReplace(string remove, string add)
        {
            Game.Option.Trigger(remove, true);
            Game.Option.Trigger(add);
        }

        public override void Do()
        {
            if (Name == "FighterOrBrawler")
            {
                if (Game.Option.IsTriggered("Боец"))
                    TriggerReplace("Боец", "Дебошир");
                else
                    Game.Option.Trigger("Боец");
            }
            else
                base.Do(Character.Protagonist);
        }
    }
}

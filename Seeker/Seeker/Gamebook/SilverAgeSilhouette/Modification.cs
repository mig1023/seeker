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
            else if (Name == "RevolutionaryOrRebel")
            {
                if (!Game.Option.IsTriggered("Революционер"))
                    Game.Option.Trigger("Революционер");
                else
                    Game.Option.Trigger("Бунтарь");
            }
            else if (Name == "LoserOrKissOfDeath")
            {
                if (!Game.Option.IsTriggered("Неудачник"))
                    Game.Option.Trigger("Неудачник");
                else
                    Game.Option.Trigger("Поцелуй смерти");
            }
            else if (Name == "Monarchist")
            {
                if (!Game.Option.IsTriggered("Сын Отечества"))
                    Game.Option.Trigger("Сын Отечества");
                else
                    Game.Option.Trigger("Монархист");
            }
            else
                base.Do(Character.Protagonist);
        }
    }
}

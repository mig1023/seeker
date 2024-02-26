using System;

namespace Seeker.Gamebook.Hunt
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "RandomAction")
            {
                int action = Game.Dice.Roll(size: 10);
                Game.Option.Trigger($"{action}действие");
            }
            else if (Name == "RandomResponse")
            {
                int response = Game.Dice.Roll(size: 10);
                Game.Option.Trigger($"{response}ответ");
            }
            else if (Name == "RandomBit")
            {
                int bit = Game.Dice.Roll(size: 2);

                if (bit == 1)
                    Game.Option.Trigger("Укусил");
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

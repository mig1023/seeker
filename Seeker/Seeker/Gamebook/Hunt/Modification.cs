using System;

namespace Seeker.Gamebook.Hunt
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private void RandomTrigger(string trigger)
        {
            int random = Game.Dice.Roll(size: 10);
            Game.Option.Trigger($"{random}{trigger}");
        }

        private void ProbabilityTrigger(int probability, string trigger)
        {
            if (Game.Dice.Roll(size: probability) == 1)
                Game.Option.Trigger(trigger);
        }

        private void Clean(string trigger)
        {
            for (int i = 1; i <= 10; i++)
                Game.Option.Trigger($"{i}{trigger}", remove: true);
        }

        public override void Do()
        {
            if (Name == "RandomAction")
            {
                RandomTrigger("действие");
            }
            else if (Name == "CleanAction")
            {
                Clean("действие");
            }
            else if (Name == "RandomResponse")
            {
                RandomTrigger("ответ");
            }
            else if (Name == "CleanResponse")
            {
                Clean("ответ");
            }
            else if (Name == "RandomActionAndResponse")
            {
                RandomTrigger("действие");
                RandomTrigger("ответ");
            }
            else if (Name == "RandomBit")
            {
                ProbabilityTrigger(probability: 2, "Укусил");
            }
            else if (Name == "RandomMiss")
            {
                ProbabilityTrigger(probability: 4, "Мимо");
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}

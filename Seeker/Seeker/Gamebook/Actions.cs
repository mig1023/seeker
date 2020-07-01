using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Actions : Seeker.Interfaces.IActions
    {
        public List<Character> Enemies { get; set; }
        public string ActionName { get; set; }
        public string ButtonName { get; set; }


        public List<string> Do()
        {
            var method = typeof(Actions).GetMethod(ActionName);
            List<string> tmp = method.Invoke(this, new object[] { }) as List<string>;

            return tmp;
        }

        public List<string> GoodLuckCheck()
        {
            bool goodLuck = Game.Dice.Roll(dices: 2) < Game.Data.Protagonist.Luck;

            Game.Data.Protagonist.Luck -= 1;

            return new List<string> { (goodLuck ? "УСПЕХ :)" : "НЕУДАЧА :(") };
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character enemy in Enemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    fight.Add(String.Format("{0} (выносливость {1})", enemy.Name, enemy.Endurance));

                    int protagonistHitStrength = Game.Dice.Roll(dices: 2) + Game.Data.Protagonist.Mastery;
                    fight.Add(String.Format("Сила вашего удара: {0}", protagonistHitStrength));

                    int enemyHitStrength = Game.Dice.Roll(dices: 2) + enemy.Mastery;
                    fight.Add(String.Format("Сила его удара: {0}", enemyHitStrength));

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("GOOD|Вы ранили противника"));
                        enemy.Endurance -= 2;

                        bool enemyLost = true;

                        foreach (Character e in Enemies)
                            if (e.Endurance > 0)
                                enemyLost = false;

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|Противник ранил вас"));
                        Game.Data.Protagonist.Endurance -= 2;

                        if (Game.Data.Protagonist.Endurance <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}

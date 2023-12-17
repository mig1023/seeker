using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.TenementBuilding
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        //public override List<string> Representer()
        //{
        //    if (Enemies == null)
        //        return new List<string>();

        //    string name = Enemies[0];
        //    int strength = Constants.Enemies[name];
        //    int count = Enemies.Count;
        //    string line = Game.Services.CoinsNoun(count, "штук", "штуки", "штук");

        //    return new List<string> { $"{name}\n{strength} сила каждого, всего {count} {line}" };
        //}

        public static string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
            {
                string luck = Constants.LuckList[Character.Protagonist.Luck[i] ? i : i + 10];
                luckListShow += $"{luck} ";
            }

            return luckListShow;
        }

        public List<string> Goodluck()
        {
            List<string> luckCheck = new List<string>
            {
                "Числа удачи:",
                "BIG|" + LuckNumbers()
            };

            int goodLuck = Game.Dice.Roll();

            string luckLine = protagonist.Luck[goodLuck] ? "не " : String.Empty;
            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {luckLine}зачёркунтый");

            luckCheck.Add(Result(protagonist.Luck[goodLuck], "УСПЕХ|НЕУДАЧА"));

            protagonist.Luck[goodLuck] = false;

            return luckCheck;
        }
    }
}

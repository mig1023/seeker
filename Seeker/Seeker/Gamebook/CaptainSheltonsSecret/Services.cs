using System;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Services
    {
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

        public static bool IsProtagonist(string name) =>
            name == Character.Protagonist.Name;
    }
}

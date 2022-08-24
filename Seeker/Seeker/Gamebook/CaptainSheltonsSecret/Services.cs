using System;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Services
    {
        public static string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += String.Format("{0} ", Constants.LuckList[Character.Protagonist.Luck[i] ? i : i + 10]);

            return luckListShow;
        }

        public static bool IsProtagonist(string name) =>
            name == Character.Protagonist.Name;
    }
}

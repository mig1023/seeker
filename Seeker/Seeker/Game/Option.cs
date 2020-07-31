using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Option
    {
        public int Destination { get; set; }
        public string Text { get; set; }
        public string OnlyIf { get; set; }

        public static void OpenOption(string optionLine)
        {
            if (String.IsNullOrEmpty(optionLine))
                return;

            string[] allOption = optionLine.Split(',');

            foreach (string option in allOption)
                Game.Data.OpenedOption.Add(option.Trim());
        }
    }
}

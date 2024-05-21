using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.WrongWayGoBack
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status()
        {
            if (Character.Protagonist.Time > 0)
            {
                TimeSpan time = TimeSpan.FromSeconds(Character.Protagonist.Time);
                return new List<string> { "Оставшееся время:" + time.ToString(@"hh\:mm\:ss") };
            }
            else
            {
                return new List<string> { "Оставшееся время: 00:00:00" };
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 33;
            toEndText = String.Empty;

            if (Game.Data.CurrentParagraphID == 33)
            {
                return false;
            }
            else if (Character.Protagonist.Time > 0)
            {
                return false;
            }
            else
            {
                toEndText = "Время истекло...";
                return true;
            }
        }
    }
}

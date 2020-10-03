using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Interfaces
{
    interface IConstants
    {
        string GetButtonsColor(Game.Buttons.ButtonTypes type);

        string GetFontColor();

        string GetBackgroundColor();

        string GetStatusBarColor();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Interfaces
{
    interface IConstants
    {
        string GetButtonsColor(Game.Buttons.ButtonTypes type);

        string GetColor(Game.Data.ColorTypes type);
    }
}

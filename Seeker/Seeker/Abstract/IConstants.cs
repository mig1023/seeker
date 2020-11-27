﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Abstract
{
    interface IConstants
    {
        string GetButtonsColor(Output.Buttons.ButtonTypes type);

        string GetColor(Game.Data.ColorTypes type);

        string GetFont();

        bool GetLtlFont();

        double? GetLineHeight();

    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SeaTales
{
    interface IParts
    {
        List<string> Status();

        List<string> AdditionalStatus();

        List<string> Representer(Actions action);

        bool GameOver(out int toEndParagraph, out string toEndText);

        List<string> Test(Actions action);
    }
}

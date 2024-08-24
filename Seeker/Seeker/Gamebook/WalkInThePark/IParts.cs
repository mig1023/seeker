using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.WalkInThePark
{
    interface IParts
    {
        List<string> Status();

        List<string> AdditionalStatus();

        bool GameOver(Actions action, out int toEndParagraph, out string toEndText);

        List<string> Representer(Actions action);
    }
}

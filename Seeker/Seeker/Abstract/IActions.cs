using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Abstract
{
    interface IActions
    {
        string ActionName { get; set; }

        string ButtonName { get; set; }

        string Aftertext { get; set; }

        List<string> Do(out bool reload, string action = "", bool Trigger = false);

        List<string> Status();

        List<string> StaticButtons();

        bool StaticAction(string action);

        bool GameOver(out int toEndParagraph, out string toEndText);

        bool IsButtonEnabled();
    }
}

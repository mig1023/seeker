using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Interfaces
{
    interface IActions
    {
        string ActionName { get; set; }

        string ButtonName { get; set; }

        string Aftertext { get; set; }

        List<string> Do(out bool reload, string action = "", bool openOption = false);

        List<string> Status();

        bool GameOver();

        bool IsButtonEnabled();
    }
}

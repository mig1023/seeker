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

        List<string> Do(string action = "");

        List<Label> Status();

        bool GameOver();
    }
}

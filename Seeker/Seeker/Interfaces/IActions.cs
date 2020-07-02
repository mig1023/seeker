using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Interfaces
{
    interface IActions
    {
        string ActionName { get; set; }

        string ButtonName { get; set; }

        List<string> Do(string action = "");

        string Status();
    }
}

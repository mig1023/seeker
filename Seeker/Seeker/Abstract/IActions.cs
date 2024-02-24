using System.Collections.Generic;
using System.Xml;

namespace Seeker.Abstract
{
    interface IActions
    {
        string Type { get; set; }
        string Button { get; set; }
        List<Output.Text> Texts { get; set; }
        string Trigger { get; set; }
        string Head { get; set; }

        int Price { get; set; }

        bool Used { get; set; } 
        bool Multiple { get; set; }

        Abstract.IModification Benefit { get; set; }

        List<string> Do(out bool reload, string action = "", bool Trigger = false);

        List<string> Status();

        List<string> AdditionalStatus();

        List<string> StaticButtons();

        string ButtonText();

        bool StaticAction(string action);

        bool GameOver(out int toEndParagraph, out string toEndText);

        bool IsButtonEnabled(bool secondButton = false);

        bool IsHealingEnabled();

        void UseHealing(int healingLevel);

        List<string> TextByProperties(XmlNode text);

        Game.Option Option { get; set; }

        bool Availability(string option);
    }
}

using System.Collections.Generic;

namespace Seeker.Abstract
{
    interface IActions
    {
        string Name { get; set; }
        string Button { get; set; }
        string Aftertext { get; set; }
        List<Output.Text> Aftertexts { get; set; }
        string Trigger { get; set; }
        string Text { get; set; }

        int Price { get; set; }

        bool Used { get; set; } 
        bool Multiple { get; set; }

        Abstract.IModification Benefit { get; set; }

        List<string> Do(out bool reload, string action = "", bool Trigger = false);

        List<string> Status();

        List<string> AdditionalStatus();

        List<string> StaticButtons();

        bool StaticAction(string action);

        bool GameOver(out int toEndParagraph, out string toEndText);

        bool IsButtonEnabled(bool secondButton = false);

        bool IsHealingEnabled();

        void UseHealing(int healingLevel);

        string TextByOptions(string option);

        Game.Option Option { get; set; }
    }
}

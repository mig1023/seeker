using System.Collections.Generic;
using System.Xml;

namespace Seeker.Abstract
{
    interface IConstants
    {
        void Clean();

        void LoadColor(string type, string value);

        string GetColor(Output.Buttons.ButtonTypes type);

        string GetColor(Game.Data.ColorTypes type);

        string GetFont();

        Output.Interface.TextFontSize GetFontSize();

        List<int> GetParagraphsWithoutStatuses();

        List<int> GetParagraphsWithoutStaticsButtons();

        void LoadParagraphsWithoutSomething(XmlDocument xmlFile, string type);

        int? GetParagraphsStatusesLimit();

        void LoadEnabledDisabledOption(string option);

        bool ShowDisabledOption();

        Dictionary<string, string> ButtonText();

        void LoadButtonText(string button, string text);
    }
}

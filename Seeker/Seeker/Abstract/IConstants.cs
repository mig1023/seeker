using System.Collections.Generic;
using System.Xml;

namespace Seeker.Abstract
{
    interface IConstants
    {
        void Clean();

        void LoadColor(string type, string value);

        void LoadColor(string type, string value, bool button);

        string GetColor(Output.Buttons.ButtonTypes type);

        string GetColor(Game.Data.ColorTypes type);

        string GetFont();

        Output.Interface.TextFontSize GetFontSize();

        List<int> GetParagraphsWithoutStatuses();

        void LoadParagraphsWithoutStatuses(XmlNode paragraphs);

        int? GetParagraphsStatusesLimit();

        bool ShowDisabledOption();

        string ButtonText();
    }
}

using System.Collections.Generic;
using System.Xml;

namespace Seeker.Abstract
{
    interface IConstants
    {
        void Load(string name, string value);

        bool GetBool(string name);

        string GetString(string name);

        void Clean();

        void LoadColor(string type, string value);

        string GetColor(Output.Buttons.ButtonTypes type);

        string GetColor(Game.Data.ColorTypes type);

        string GetFont();

        Output.Interface.TextFontSize GetFontSize();

        List<int> GetParagraphsWithoutStatuses();

        List<int> GetParagraphsWithoutStaticsButtons();

        void LoadList(string name, List<string> list);

        void LoadDictionary(string name, Dictionary<string, string> dictionary);

        bool GetParagraphsStatusesLimit(out int limitStart, out int limitEnd);

        void LoadAdditionalStatusesEqualParts(string option);

        bool ShowAdditionalStatusesEqualParts();

        void LoadDefaultFontSize(string option);

        void LoadWalkingInCirclesAcceptable(string option);

        bool GetWalkingInCirclesAcceptable();

        Dictionary<string, string> ButtonText();

        void LoadButtonText(string button, string text);
    }
}

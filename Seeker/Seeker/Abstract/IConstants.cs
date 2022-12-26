﻿using System.Collections.Generic;
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

        void LoadList(string name, List<string> list);

        void LoadDictionary(string name, Dictionary<string, string> dictionary);

        bool GetParagraphsStatusesLimit(out int limitStart, out int limitEnd);

        void LoadEnabledDisabledOption(bool option);

        bool ShowDisabledOption();

        void LoadStartParagraphOption(string option);

        void LoadDefaultFontSize(string option);

        void LoadDynamicOption(bool option);

        bool GetDynamicOption();

        int GetStartParagraph();

        Dictionary<string, string> ButtonText();

        void LoadButtonText(string button, string text);
    }
}

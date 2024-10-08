﻿using System.Collections.Generic;

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

        List<int> GetParagraphsWithoutStatuses();

        List<int> GetParagraphsWithoutStaticsButtons();

        void LoadList(string name, List<string> list);

        void LoadDictionary(string name, Dictionary<string, string> dictionary);

        Dictionary<string, string> ButtonText();

        void LoadButtonText(string button, string text);
    }
}

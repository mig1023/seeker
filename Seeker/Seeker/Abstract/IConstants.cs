using System.Collections.Generic;

namespace Seeker.Abstract
{
    interface IConstants
    {
        void LoadButtonsColor(string type, string value);

        void LoadColor(string type, string value);

        string GetButtonsColor(Output.Buttons.ButtonTypes type);

        string GetColor(Game.Data.ColorTypes type);

        string GetFont();

        Output.Interface.TextFontSize GetFontSize();

        List<int> GetParagraphsWithoutStatuses();

        int? GetParagraphsStatusesLimit();

        bool ShowDisabledOption();

        string ButtonText();
    }
}

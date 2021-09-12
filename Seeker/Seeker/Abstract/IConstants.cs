using System.Collections.Generic;

namespace Seeker.Abstract
{
    interface IConstants
    {
        string GetButtonsColor(Output.Buttons.ButtonTypes type);

        string GetColor(Game.Data.ColorTypes type);

        string GetFont();

        Output.Interface.TextFontSize GetFontSize();

        double? GetLineHeight();

        List<int> GetParagraphsWithoutStatuses();

        bool ShowDisabledOption();
    }
}

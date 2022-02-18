using System;
using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Prototypes
{
    class Constants
    {
        public virtual Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>();

        public virtual Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>();

        public virtual string GetButtonsColor(ButtonTypes type) => (ButtonsColors().ContainsKey(type) ? ButtonsColors()[type] : String.Empty);

        public virtual string GetColor(Game.Data.ColorTypes type) => (Colors().ContainsKey(type) ? Colors()[type] : String.Empty);

        public virtual string GetFont() => String.Empty;

        public virtual Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public virtual List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };

        public virtual int? GetParagraphsStatusesLimit() => null;

        public virtual bool ShowDisabledOption() => false;
    }
}

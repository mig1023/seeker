using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.DzungarWar
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#93684f",
            [ButtonTypes.Action] = "#8c7254",
            [ButtonTypes.Option] = "#a88672",
            [ButtonTypes.Continue] = "#b99e8e",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#f1e6d1",
            [ColorTypes.ActionBox] = "#af8f69",
            [ColorTypes.StatusBar] = "#dcc18c",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.AdditionalStatus] = "#e3cda3",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 660 };

        public static Dictionary<string, string> StatNames() => new Dictionary<string, string>
        {
            ["Strength"] = "силы",
            ["Skill"] = "ловкости",
            ["Wisdom"] = "мудрости",
            ["Cunning"] = "хитрости",
            ["Oratory"] = "красноречия",
            ["Danger"] = "опасности",
        };

        public override bool ShowDisabledOption() => true;
    }
}

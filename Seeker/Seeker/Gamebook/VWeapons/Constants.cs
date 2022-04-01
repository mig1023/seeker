using System.Collections.Generic;

namespace Seeker.Gamebook.VWeapons
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<string, string> healingParts = new Dictionary<string, string>
        {
            ["головы"] = "Head",
            ["плеч"] = "ShoulderGirdle",
            ["корпуса"] = "Body",
            ["рук"] = "Hands",
            ["ног"] = "Legs",
        };

        public override Dictionary<string, string> ButtonText() => new Dictionary<string, string>
        {
            ["Fight"] = "Перестрелка",
        };

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}

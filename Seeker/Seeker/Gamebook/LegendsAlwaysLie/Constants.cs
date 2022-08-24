using System.Collections.Generic;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<string, Character.SpecializationType> GetSpecializationType() =>
            new Dictionary<string, Character.SpecializationType>
        {
            ["ВОИН"] = Character.SpecializationType.Warrior,
            ["МАГ"] = Character.SpecializationType.Wizard,
            ["МЕТАТЕЛЬ"] = Character.SpecializationType.Thrower,
        };

        public override Output.Interface.TextFontSize GetFontSize() =>
            Output.Interface.TextFontSize.little;

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            Availability = Actions.StaticInstance.Availability,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}

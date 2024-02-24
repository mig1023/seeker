using System.Collections.Generic;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, Character.SpecializationType> GetSpecializationType() =>
            new Dictionary<string, Character.SpecializationType>
        {
            ["ВОИН"] = Character.SpecializationType.Warrior,
            ["МАГ"] = Character.SpecializationType.Wizard,
            ["МЕТАТЕЛЬ"] = Character.SpecializationType.Thrower,
        };
    }
}

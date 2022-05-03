using System.Collections.Generic;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<int, string> GetCountName() => new Dictionary<int, string>
        {
            [1] = "Первый",
            [2] = "Второй",
            [3] = "Третий",
            [4] = "Четвёртый",
            [5] = "Пятый",
            [6] = "Шестой",
        };

        public static Dictionary<int, string> GetPassageName() => new Dictionary<int, string>
        {
            [1] = "дверь",
            [2] = "первое окно",
            [3] = "второе окно",
        };

        public static int GetUlrichMastery() => 8;

        public static int GetVanRichtenMastery() => 10;

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

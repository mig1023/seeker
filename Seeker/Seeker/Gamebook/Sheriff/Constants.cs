using System.Collections.Generic;

namespace Seeker.Gamebook.Sheriff
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static List<string> CleaningNotebookList() => new List<string>
        {
            "Билли убит в амбаре",
            "Все любили Билли",
            "Убит ночью",
            "Не успел защититься",
            "У убийцы есть рука",
            "Убийца женщина",
            "Надо проверить салон мадам Жу-жу",
            "Женская перчатка",
            "Дробовик",
            "Патроны",
            "Портсигар",
        };

        public static Dictionary<string, int> Levels() => new Dictionary<string, int>
        {
            ["Easy"] = 100,
            ["Medium"] = 1,
            ["Hard"] = 0,
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

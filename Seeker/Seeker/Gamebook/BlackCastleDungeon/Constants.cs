using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#151515",
            [ButtonTypes.Action] = "#3f3f3f",
            [ButtonTypes.Option] = "#696969",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#2a2a2a",
            [ColorTypes.BookColor] = "#151515",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 619 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 619 };

        public static List<string> StaticSpells() => new List<string>
        {
            "ЗАКЛЯТИЕ КОПИИ", "ЗАКЛЯТИЕ СИЛЫ", "ЗАКЛЯТИЕ СЛАБОСТИ"
        };

        public static List<string> GetActionParams() => new List<string>
        {
            "RoundsToWin", "WoundsToWin", "StrengthPenlty", "ThisIsSpell"
        };

        public static List<string> GetEnemyParams() => new List<string>
        {
            "Name", "MaxMastery", "MaxEndurance"
        };

        public override string GetDescription() => "В сказочное королевство приходит б" +
            "еда. Злой волшебник Барлад Дэрт, поселившийся в таинственном и мрачном Чё" +
            "рном замке в самом сердце Зачарованного леса, хочет взять в жёны единстве" +
            "нную дочь Короля. Получив отказ, он похищает Принцессу и с помощью чёрной" +
            " магии погружает её в волшебный сон. Много смельчаков, вызвавшись освобод" +
            "ить Принцессу, отправились на поиски Чёрного замка, но ни один не вернулс" +
            "я назад.\n\nЕсли вы хотите попытаться избежать бесчисленных ловушек Зачар" +
            "ованного леса, сразиться с коварным и бесстрашным воинством чародея, отыс" +
            "кать Чёрный замок и, пробравшись в него, победить мага и освободить Принц" +
            "ессу, то всё, что вам для этого надо, - два игральных кубика, карандаш и " +
            "ластик. И не беда, если не удалось сделать это с первого раза, ведь у вас" +
            " всегда будет возможность пойти по другому пути, начав всё сначала.";

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
        };
    }
}

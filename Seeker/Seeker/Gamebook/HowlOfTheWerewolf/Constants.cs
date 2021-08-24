using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#383e3b",
            [ButtonTypes.Action] = "#516f72",
            [ButtonTypes.Option] = "#738b8e",
            [ButtonTypes.Continue] = "#738b8e",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#c2cccd",
            [ColorTypes.StatusBar] = "#253130",
            [ColorTypes.AdditionalStatus] = "#a8b7b8",
            [ColorTypes.ActionBox] = "#a8b7b8",
        };

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

        public static List<string> GetActionParams() => new List<string>
        {
            "Value", "RoundsToWin", "RoundsWinToWin", "RoundsFailToFail", "RoundsToFight", "WoundsToWin",
            "WoundsToFail", "WoundsForTransformation", "WoundsLimit", "HitStrengthBonus", "ExtendedDamage"
        };

        public static List<string> GetEnemyParams() => new List<string>
        {
            "Name", "MaxMastery", "MaxEndurance"
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
        };
    }
}

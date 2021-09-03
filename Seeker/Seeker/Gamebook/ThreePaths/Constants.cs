using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ThreePaths
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#009999",
            [ButtonTypes.Option] = "#009999",
            [ButtonTypes.Action] = "#005b5b",
            [ButtonTypes.Continue] = "#6fc5c5",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#007a7a",
            [ColorTypes.BookColor] = "#009999",
        };

        public static List<string> GetModsParams() => new List<string> 
        {
            "Name", "Value", "ValueString"
        };

        public override string GetDescription() => "В этой книге есть и опасные приключения, и дальние странствия по неведомым землям. Но от обычных книг она отличается тем, что её герой - это сам читатель. Вы сможете стать одним из главных действующих лиц захватывающего действия, выбрать по карте маршруты своих путешествий, и только вам решать, когда надо пускать в дело меч, а когда стоит обойти опасность стороной. По количеству возможных вариантов развития сюжета книги-игры значительно превосходят компьютерные игрушки. Итак, приключения начинаются. Желаем успеха!";

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

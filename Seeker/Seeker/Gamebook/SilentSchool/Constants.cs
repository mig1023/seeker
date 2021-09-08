using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.SilentSchool
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#434343",
            [ButtonTypes.Action] = "#555555",
            [ButtonTypes.Option] = "#686868",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#2a2a2a",
            [ColorTypes.BookColor] = "#151515",
        };

        public override string GetDescription() => "В тексте содержатся сцены насилия " +
            "(в том числе и над детьми), множество околоцензурных выражений и прочая ч" +
            "ернуха. Если вы поклонник ювенальной юстиции, радетель за чистоту русског" +
            "о языка или просто считаете себя «человеком высокой культуры среди всего " +
            "этого быдла» – лучше не читайте. Я предупредил.";

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

using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.RockOfTerror
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#191919",
            [ButtonTypes.Action] = "#2a2a2a",
            [ButtonTypes.Option] = "#494949",
            [ButtonTypes.Continue] = "#2f2f2f",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#000000",
            [ColorTypes.Font] = "#FFFFFF",
            [ColorTypes.StatusBar] = "#151515",
            [ColorTypes.BookColor] = "#000000",
        };

        public override string GetDescription() => "В 15-16 веках Европа была объята священным пламенем инквизиции. Десятки тысяч невинных мужчин, женщин и детей были казнены по обвинению в колдовстве и сговоре с дьяволом. Однако, что бы не говорили историки, как бы не отрицали наличие колдовства и злых духов, не все эти обвинения были вымышленными...\n\nЭта история произошла в 1536 году, на юго-востоке Германии, в местечке под названием Догиндорф. История столь ужасная и трагичная, полная мистики и колдовства, что странствующие монахи Раймунд и Палуданус посчитали своим долгом изложить её на бумаге. К сожалению, до наших дней дошла лишь часть рукописи и многие эпизоды, а так же подробности окончания истории, доподлинно неизвестны. Зато известно с чего она начиналась...";

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

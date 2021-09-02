using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.OctopusIsland
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#c93c20",
            [ButtonTypes.Action] = "#c93c20",
            [ButtonTypes.Option] = "#c93c20",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#ce4f36",
        };

        public override string GetDescription() => "Вместе с Покорителями Невозможного, ты исследуешь остров, остров Осьминогов, на котором томится в заключении маленький мальчик. Ты попытаешься освободить мальчика, и потратишь на это два дня, а может, и больше, если дела пойдут совсем не важно. Но это время ты проведёшь вместе с Ксолотлом, Тибо и Суи. Они будут разговаривать с тобой, словно ты всегда был их другом, потому что в этом путешествии тебе придётся сыграть роль Сержа. Это именно ты подскажешь товарищам, по какой тропинке идти - налево или направо, вступать в бой с роботом-чудовищем или осторожно, на цыпочках, удалиться.";

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

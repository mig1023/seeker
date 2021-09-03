using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#fcdd76",
            [ButtonTypes.Action] = "#fcdd76",
            [ButtonTypes.Option] = "#fcdd76",
            [ButtonTypes.Font] = "#000000",
            [ButtonTypes.Continue] = "#fdeeba",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#fce391",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.BookColor] = "#fcdd76",
            [ColorTypes.BookFontColor] = "#000000",
        };

        public override bool ShowDisabledOption() => true;

        public override string GetDescription() => "Тяжела жизнь гладиатора. Кровопролитные бои на песке арены, коварство противников, непредсказуемая толпа алчущих крови зрителей. Да, тяжела и опасна, но ещё опасней становиться поперёк дороги великих мира сего! Покушение на императора, интриги сенаторов, предательство и алчность – всё это лишь малая толика античного мира, в который тебе предстоит погрузиться. Но твою руку привычно греет рукоять верного клинка, а сердце жаждет приключений – что ещё требуется для такого героя как ты? Начни чтение обычным гладиатором и пусть Фортуна решит твою судьбу: спаситель империи или бесславное забвение! И да пребудут с тобой Боги!";

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

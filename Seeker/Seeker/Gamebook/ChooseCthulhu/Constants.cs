using System.Collections.Generic;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static List<int> BackgroudColor = null;
        public static List<int> MainButtonColor = null;

        public static bool InitColor()
        {
            if (BackgroudColor == null)
                BackgroudColor = new List<int> { 129, 147, 147 };
            else
                Services.ModColors(ref BackgroudColor);

            if (MainButtonColor == null)
                MainButtonColor = new List<int> { 0, 47, 62 };
            else
                Services.ModColors(ref MainButtonColor);

            return true;
        }

        public override string GetColor(ButtonTypes type)
        {
            Services.CheckColorInit();

            bool mainDuttons = (type == ButtonTypes.Main) || (type == ButtonTypes.Option);
            bool supplButtons = (type == ButtonTypes.System) || (type == ButtonTypes.Continue);

            if (Game.Settings.IsEnabled("WithoutStyles"))
                return base.GetColor(type);

            else if (mainDuttons || supplButtons)
                return Services.HexColor(MainButtonColor[0], MainButtonColor[1], MainButtonColor[2]);

            else if (type == ButtonTypes.Border)
                return Services.СontrastBorder(BackgroudColor, MainButtonColor);

            else
                return base.GetColor(type);
        }

        public override string GetColor(ColorTypes type)
        {
            Services.CheckColorInit();

            if (Game.Settings.IsEnabled("WithoutStyles"))
                return base.GetColor(type);

            else if (type == ColorTypes.Background)
                return Services.HexColor(BackgroudColor[0], BackgroudColor[1], BackgroudColor[2]);

            else if (type == ColorTypes.Font)
                return Services.СontrastText(BackgroudColor);

            else
                return base.GetColor(type);
        }


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

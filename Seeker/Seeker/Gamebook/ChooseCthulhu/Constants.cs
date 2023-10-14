using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        private static Character protagonist = Character.Protagonist;

        public static void ChangeBackground()
        {
            protagonist.BackColor = Services.ModColors(protagonist.BackColor);
            protagonist.BtnColor = Services.ModColors(protagonist.BtnColor);
        }

        public override string GetColor(ButtonTypes type)
        {
            bool mainDuttons = (type == ButtonTypes.Main) || (type == ButtonTypes.Option);
            bool supplButtons = (type == ButtonTypes.System) || (type == ButtonTypes.Continue);

            if (Game.Settings.IsEnabled("WithoutStyles"))
            {
                return base.GetColor(type);
            }
            else if (mainDuttons || supplButtons)
            {
                return Services.HexColor(protagonist.BtnColor[0], protagonist.BtnColor[1], protagonist.BtnColor[2]);
            }
            else if (type == ButtonTypes.Border)
            {
                return Services.СontrastBorder(protagonist.BackColor, protagonist.BtnColor);
            }
            else
            {
                return base.GetColor(type);
            }
        }

        public override string GetColor(ColorTypes type)
        {
            if (Game.Settings.IsEnabled("WithoutStyles"))
            {
                return base.GetColor(type);
            }
            else if (type == ColorTypes.Background)
            {
                return Services.HexColor(protagonist.BackColor[0], protagonist.BackColor[1], protagonist.BackColor[2]);
            }
            else if (type == ColorTypes.Font)
            {
                return Services.СontrastText(protagonist.BackColor);
            }
            else
            {
                return base.GetColor(type);
            }
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

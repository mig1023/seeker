using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static void ChangeBackground()
        {
            Character.Protagonist.BackColor = Colors.Mod(Character.Protagonist.BackColor);
            Character.Protagonist.BtnColor = Colors.Mod(Character.Protagonist.BtnColor);
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
                return Colors.Hex(Character.Protagonist.BtnColor[0], Character.Protagonist.BtnColor[1], Character.Protagonist.BtnColor[2]);
            }
            else if (type == ButtonTypes.Border)
            {
                return Colors.СontrastBorder(Character.Protagonist.BackColor, Character.Protagonist.BtnColor);
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
                return Colors.Hex(Character.Protagonist.BackColor[0], Character.Protagonist.BackColor[1], Character.Protagonist.BackColor[2]);
            }
            else if (type == ColorTypes.Font)
            {
                return Colors.СontrastText(Character.Protagonist.BackColor);
            }
            else
            {
                return base.GetColor(type);
            }
        }
    }
}

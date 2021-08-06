using System;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Buttons
    {
        public enum ButtonTypes { Main, Action, Option, Font, Border, Continue }

        public static Button Action(string actionName, bool enabled = true)
        {
            string color = Game.Data.Constants.GetButtonsColor(ButtonTypes.Action);

            Button actionButton = new Button
            {
                Text = actionName,
                TextColor = Color.White,
                IsEnabled = enabled,
                BackgroundColor = (enabled ? Color.FromHex(color) : Color.Gray),
                FontFamily = Interface.TextFontFamily(standart: true),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
            };

            return Interface.SetBorderAndTextColor(actionButton);
        }

        public static Button Option(Game.Option option)
        {
            bool optionColor = !String.IsNullOrEmpty(option.OnlyIf) && !option.OnlyIf.Contains(">") && !option.OnlyIf.Contains("<");

            if (Game.Data.Constants.ShowDisabledOption() || !String.IsNullOrEmpty(option.Aftertext))
                optionColor = !String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf);

            string color = Game.Data.Constants.GetButtonsColor(optionColor ?
                Buttons.ButtonTypes.Option : Buttons.ButtonTypes.Main);

            bool isEnabled = !(!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf));

            if (String.IsNullOrEmpty(option.Text))
                option.Text = (option.Destination == 0 ? "Начать сначала" : "Далее");

            Button optionButton = new Button
            {
                Text = option.Text,
                BackgroundColor = Color.FromHex(color),
                IsEnabled = isEnabled,
                FontFamily = Interface.TextFontFamily(standart: true),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                IsVisible = String.IsNullOrEmpty(option.Input),
            };

            return Interface.SetBorderAndTextColor(optionButton);
        }

        public static Button Additional(string text)
        {
            string color = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);

            Button additionButton = new Button
            {
                Text = text,
                BackgroundColor = (String.IsNullOrEmpty(color) ? Color.LightGray : Color.FromHex(color)),
                FontFamily = Interface.TextFontFamily(standart: true),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            return Interface.SetBorderAndTextColor(additionButton);
        }

        public static Button System(string text)
        {
            string color = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);

            Button systemButton = new Button
            {
                Text = text,
                BackgroundColor = (String.IsNullOrEmpty(color) ? Color.LightGray : Color.FromHex(color)),
                FontFamily = Interface.TextFontFamily(standart: true),
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            return Interface.SetBorderAndTextColor(systemButton);
        }
    }
}

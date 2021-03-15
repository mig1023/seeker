using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Buttons
    {
        public enum ButtonTypes { Main, Action, Option, Font, Border, Continue }


        public static Button Action(string actionName, bool enabled = true)
        {
            string color = Game.Data.Constants.GetButtonsColor(ButtonTypes.Action);

            Button actionButton = new Button()
            {
                Text = actionName,
                TextColor = Xamarin.Forms.Color.White,
                IsEnabled = enabled,
                BackgroundColor = (enabled ? Color.FromHex(color) : Color.Gray),
                FontFamily = Output.Interface.TextFontFamily(),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
            };

            return Output.Interface.SetBorderAndTextColor(actionButton);
        }

        public static Button Option(Game.Option option)
        {
            bool optionColor = !String.IsNullOrEmpty(option.OnlyIf) && !option.OnlyIf.Contains(">") && !option.OnlyIf.Contains("<");

            if (Game.Data.ShowDisabledOption || !String.IsNullOrEmpty(option.Aftertext))
                optionColor = !String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf);

            string color = Game.Data.Constants.GetButtonsColor(
                optionColor ? Output.Buttons.ButtonTypes.Option : Output.Buttons.ButtonTypes.Main
            );

            bool isEnabled = ((!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.CheckOnlyIf(option.OnlyIf)) ? false : true);

            Button optionButton = new Button()
            {
                Text = option.Text,
                BackgroundColor = Color.FromHex(color),
                IsEnabled = isEnabled,
                FontFamily = Output.Interface.TextFontFamily(),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            return Output.Interface.SetBorderAndTextColor(optionButton);
        }

        public static Button Additional(string text)
        {
            string color = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);

            Button additionButton = new Button()
            {
                Text = text,
                BackgroundColor = (String.IsNullOrEmpty(color) ? Color.LightGray : Color.FromHex(color)),
                FontFamily = Output.Interface.TextFontFamily(),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            return Output.Interface.SetBorderAndTextColor(additionButton);
        }
    }
}

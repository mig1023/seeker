using System;
using Xamarin.Forms;
using Seeker.Gamebook;

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

            return SetBorderAndTextColor(actionButton);
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

            return SetBorderAndTextColor(optionButton);
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

            return SetBorderAndTextColor(additionButton);
        }

        public static Button GamebookButton(Description gamebook)
        {
            Button gamebookButton = new Button
            {
                Text = gamebook.Title,
                BackgroundColor = Color.FromHex(gamebook.BookColor),
                FontFamily = Interface.TextFontFamily(),
                FontSize = Interface.FontSize(Interface.TextFontSize.normal),
            };

            if (!String.IsNullOrEmpty(gamebook.BorderColor))
            {
                gamebookButton.BorderColor = Color.FromHex(gamebook.BorderColor);
                gamebookButton.BorderWidth = Constants.BORDER_WIDTH;
            }

            if (!String.IsNullOrEmpty(gamebook.FontColor))
                gamebookButton.TextColor = Color.FromHex(gamebook.FontColor);
            else
                gamebookButton.TextColor = Color.White;

            return gamebookButton;
        }

        public static Button CloseSettings() => new Button
        {
            Text = Constants.BACK_LINK,
            BackgroundColor = Color.Gainsboro,
            FontFamily = Interface.TextFontFamily(),
            FontSize = Interface.FontSize(Interface.TextFontSize.normal),
            Margin = new Thickness(0, 15),
        };

        public static Button GameOver(string text)
        {
            string colorLine = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);

            Color color = Color.Gray;

            if (!String.IsNullOrEmpty(colorLine))
                color = Color.FromHex(colorLine);

            Button gameoverButton = new Button
            {
                Text = text,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = color,
                FontFamily = Interface.TextFontFamily(),
                FontSize = Interface.FontSize(Interface.TextFontSize.normal),
            };

            return SetBorderAndTextColor(gameoverButton);
        }

        public static Button SetBorderAndTextColor(Button button)
        {
            if (!String.IsNullOrEmpty(Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Border)))
            {
                button.BorderColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Border));
                button.BorderWidth = Constants.BORDER_WIDTH;
            }
            else
                button.BorderWidth = 0;

            string font = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Font);
            button.TextColor = (String.IsNullOrEmpty(font) ? Color.White : Color.FromHex(font));

            return button;
        }
    }
}

using System;
using Xamarin.Forms;
using Seeker.Gamebook;

namespace Seeker.Output
{
    class Buttons
    {
        public enum ButtonTypes
        {
            Main,
            Action,
            Option,
            ButtonFont,
            Border,
            Continue,
            System,
        };

        public static Button Action(string actionName, EventHandler onClick, bool enabled = true)
        {
            string color = Game.Data.Constants.GetColor(ButtonTypes.Action);

            Button actionButton = new Button
            {
                Text = actionName,
                TextColor = Color.White,
                IsEnabled = enabled,
                BackgroundColor = (enabled ? Color.FromHex(color) : Color.Gray),
                FontFamily = Interface.TextFontFamily(standart: true),
                FontSize = Interface.Font(NamedSize.Default),
            };

            actionButton.Clicked += onClick;

            return SetBorderAndTextColor(actionButton);
        }

        public static void EmptyOptionTextFuse(Game.Option option)
        {
            if (String.IsNullOrEmpty(option.Text))
                option.Text = (option.Goto == 0 ? "Начать сначала" : "Далее");
        }

        public static Button Option(Game.Option option, EventHandler onClick)
        {
            bool optionColor = !String.IsNullOrEmpty(option.Availability) &&
                !option.Availability.Contains(">") && !option.Availability.Contains("<");

            int aftertextsCount = option.Aftertexts?.Count ?? 0;

            bool availability = true;

            if (!String.IsNullOrEmpty(option.Availability))
                availability = Game.Data.Actions.Availability(option.Availability);

            //if (Game.Data.Constants.ShowDisabledOption(out bool _) || (aftertextsCount > 0))
            if (!Game.Data.Constants.GetBool("HideDisabledOption") || (aftertextsCount > 0))
                optionColor = !availability;
                
            string color = Game.Data.Constants.GetColor(optionColor ?
                Buttons.ButtonTypes.Option : Buttons.ButtonTypes.Main);

            if (!String.IsNullOrEmpty(option.Style))
                color = option.Style;

            bool isEnabled = !(!String.IsNullOrEmpty(option.Availability) && !availability);

            Button optionButton = new Button
            {
                Text = option.Text,
                IsEnabled = isEnabled,
                FontFamily = Interface.TextFontFamily(standart: true),
                FontSize = Interface.Font(NamedSize.Default),
                IsVisible = String.IsNullOrEmpty(option.Input),
            };

            if (optionButton.IsEnabled)
                optionButton.BackgroundColor = Color.FromHex(color);

            optionButton.Clicked += onClick;

            return SetBorderAndTextColor(optionButton);
        }

        public static Button Additional(string text, EventHandler onClick, bool anywayLarge = false)
        {
            string color = Game.Data.Constants.GetColor(Buttons.ButtonTypes.Continue);

            Button additionButton = new Button
            {
                Text = text,
                BackgroundColor = (String.IsNullOrEmpty(color) ? Color.LightGray : Color.FromHex(color)),
                FontFamily = Interface.TextFontFamily(standart: true),
            };

            if (anywayLarge || Game.Settings.IsEnabled("LargeAddButtons"))
            {
                additionButton.FontSize = Interface.Font(NamedSize.Default);
            }
            else
            {
                additionButton.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                additionButton.HeightRequest = Constants.SYS_MENU_HIGHT;
                additionButton.Padding = 0;
            }

            additionButton.Clicked += onClick;

            return SetBorderAndTextColor(additionButton);
        }

        public static Button System(string text, EventHandler onClick)
        {
            string defaultColor = Game.Data.Constants.GetColor(Buttons.ButtonTypes.Continue);
            string systemColor = Game.Data.Constants.GetColor(Buttons.ButtonTypes.System);
            string color = (String.IsNullOrEmpty(systemColor) ? defaultColor : systemColor);

            Button systemButton = new Button
            {
                Text = text,
                BackgroundColor = (String.IsNullOrEmpty(color) ? Color.LightGray : Color.FromHex(color)),
                FontFamily = Interface.TextFontFamily(standart: true),
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            systemButton.Clicked += onClick;

            return SetBorderAndTextColor(systemButton, system: true);
        }

        public static Button Gamebook(Description gamebook, EventHandler onClick)
        {
            Button gamebookButton = new Button
            {
                Text = gamebook.Title,
                BackgroundColor = Color.FromHex(gamebook.BookColor),
                FontFamily = Interface.TextFontFamily(),
                FontSize = Interface.Font(NamedSize.Default),
            };

            gamebookButton.Clicked += onClick;

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

        public static Button ClosePage(EventHandler onClick, bool bookmark = false)
        {
            Color buttonColor = Color.Gainsboro;

            if (bookmark)
            {
                string color = Game.Data.Constants.GetColor(Buttons.ButtonTypes.Main);
                buttonColor = Color.FromHex(color);
            }

            Button button = new Button
            {
                Text = Constants.BACK_LINK,
                BackgroundColor = buttonColor,
                FontFamily = Interface.TextFontFamily(),
                FontSize = Interface.Font(NamedSize.Default),
                Margin = new Thickness(0, 30),
            };

            button.Clicked += onClick;

            return button;
        }

        public static Button Bookmark(EventHandler onClick, string text,
            bool bookmark = false, bool topMargin = false, bool bottomMargin = false)
        {
            string color = Game.Data.Constants.GetColor(bookmark ?
                Buttons.ButtonTypes.Option : Buttons.ButtonTypes.Main);

            Button button = new Button
            {
                Text = text,
                BackgroundColor = Color.FromHex(color),
                FontFamily = Interface.TextFontFamily(),
                FontSize = Interface.Font(NamedSize.Default),
            };

            button.Margin = new Thickness(0, topMargin ? 30 : 0, 0, bottomMargin ? 30 : 0);
            button.Clicked += onClick;

            return SetBorderAndTextColor(button);
        }

        public static Button GameOver(string text, EventHandler onClick)
        {
            string colorLine = Game.Data.Constants.GetColor(Buttons.ButtonTypes.Continue);

            Color color = Color.Gray;

            if (!String.IsNullOrEmpty(colorLine))
                color = Color.FromHex(colorLine);

            Button gameoverButton = new Button
            {
                Text = text,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = color,
                FontFamily = Interface.TextFontFamily(),
                FontSize = Interface.Font(NamedSize.Default),
            };

            gameoverButton.Clicked += onClick;

            return SetBorderAndTextColor(gameoverButton);
        }

        public static Button SetBorderAndTextColor(Button button, bool system = false)
        {
            if (!system)
            {
                if (!String.IsNullOrEmpty(Game.Data.Constants.GetColor(Buttons.ButtonTypes.Border)))
                {
                    button.BorderColor = Color.FromHex(Game.Data.Constants.GetColor(Buttons.ButtonTypes.Border));
                    button.BorderWidth = Constants.BORDER_WIDTH;
                }
                else
                {
                    button.BorderWidth = 0;
                }
            }
          
            if (system)
            {
                string systemFont = Game.Data.Constants.GetColor(Game.Data.ColorTypes.SystemFont);
                button.TextColor = (String.IsNullOrEmpty(systemFont) ? Color.Black : Color.FromHex(systemFont));
            }
            else
            {
                string font = Game.Data.Constants.GetColor(Buttons.ButtonTypes.ButtonFont);
                button.TextColor = (String.IsNullOrEmpty(font) ? Color.White : Color.FromHex(font));
            }

            return button;
        }
    }
}

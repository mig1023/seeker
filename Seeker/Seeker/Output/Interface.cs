using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Seeker.Gamebook;
using System.Linq;

namespace Seeker.Output
{
    class Interface
    {
        public static List<Label> StatusBar(List<string> statusLines)
        {
            List<Label> statusLabels = new List<Label>();

            string textColor = Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusFont);

            foreach (string status in statusLines)
            {
                Label label = new Label();

                label.Text = status + System.Convert.ToChar(160);
                label.FontSize = 12;
                label.TextColor = (String.IsNullOrEmpty(textColor) ? Color.White : Color.FromHex(textColor));
                label.BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBar));

                label.HorizontalTextAlignment = TextAlignment.Center;
                label.VerticalTextAlignment = TextAlignment.Center;

                label.HorizontalOptions = LayoutOptions.FillAndExpand;
                label.VerticalOptions = LayoutOptions.FillAndExpand;

                statusLabels.Add(label);
            }

            return statusLabels;
        }

        public static Button GamebookButton(string gamebook)
        {
            Description description = Gamebook.List.GetDescription(gamebook);

            Button gamebookButton = new Button()
            {
                Text = gamebook,
                BackgroundColor = Color.FromHex(description.BookColor),
                FontFamily = TextFontFamily(),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
            };

            if (!String.IsNullOrEmpty(description.BorderColor))
            {
                gamebookButton.BorderColor = Color.FromHex(description.BorderColor);
                gamebookButton.BorderWidth = 1;
            }

            if (!String.IsNullOrEmpty(description.FontColor))
                gamebookButton.TextColor = Color.FromHex(description.FontColor);
            else
                gamebookButton.TextColor = Xamarin.Forms.Color.White;

            return gamebookButton;
        }

        public static Label GamebookDisclaimer(string gamebook)
        {
            return new Label()
            {
                Text = String.Format("© {0}", Gamebook.List.GetDescription(gamebook).Disclaimer),
                HorizontalTextAlignment = TextAlignment.Start,
                Margin = new Thickness(0, 0, 0, 8),
            };
        }

        public static List<View> Represent(List<string> enemiesLines)
        {
            List<View> enemies = new List<View>();

            foreach (string enemyLine in enemiesLines)
            {
                if (enemyLine.Contains("SPLITTER|"))
                {
                    string[] param = enemyLine.Split('|');

                    Label splitter = new Label()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = param[1],
                    };

                    StackLayout splitterForm = new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 25,
                        BackgroundColor = Color.FromHex("#bdbdbd"),
                    };

                    splitterForm.Children.Add(splitter);

                    enemies.Add(splitterForm);
                }
                else
                {
                    string[] enemyParam = enemyLine.Split('\n');

                    Label enemy = new Label()
                    {
                        FontAttributes = FontAttributes.Bold,
                        Text = enemyParam[0],
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = TextFontFamily(),
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    };

                    enemies.Add(enemy);

                    if (enemyParam.Length > 1)
                    {
                        Label param = new Label()
                        {
                            Text = enemyParam[1],
                            HorizontalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(0, -10, 0, 0),
                            FontFamily = TextFontFamily(),
                        };

                        enemies.Add(param);
                    }
                }
            }

            return enemies;
        }

        public static Button GameOverButton(string text)
        {
            string color = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);

            Button gameoverButton = new Button()
            {
                Text = text,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Option)),
                FontFamily = TextFontFamily(),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            return SetBorderAndTextColor(gameoverButton);
        }

        public static Button SetBorderAndTextColor(Button button)
        {
            if (!String.IsNullOrEmpty(Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Border)))
            {
                button.BorderColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Border));
                button.BorderWidth = 1;
            }

            if (!String.IsNullOrEmpty(Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Font)))
                button.TextColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Font));
            else
                button.TextColor = Xamarin.Forms.Color.White;

            return button;
        }

        public static string TextFontFamily()
        {
            string defaultFont = "YanoneFont";

            string font = String.Empty;

            if (Game.Data.Constants == null)
                font = defaultFont;
            else
                font = (String.IsNullOrEmpty(Game.Data.Constants.GetFont()) ? defaultFont : Game.Data.Constants.GetFont());

            var OnPlatformDic = (OnPlatform<string>)App.Current.Resources[font];
            var fontFamily = OnPlatformDic.Platforms.FirstOrDefault((arg) => arg.Platform.FirstOrDefault() == Device.RuntimePlatform).Value;

            return fontFamily.ToString();
        } 

        public static Label Text(string text, bool defaultParams = false)
        {
            Label label = new Label
            {
                Margin = 5,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = System.Text.RegularExpressions.Regex.Unescape(text),
                FontFamily = TextFontFamily(),
            };

            if (defaultParams)
                return label;

            if (Game.Data.Constants != null)
            {
                if (Game.Data.Constants.GetLineHeight() != null)
                    label.LineHeight = Game.Data.Constants.GetLineHeight() ?? -1;
                else
                    label.LineHeight = 1.20;

                if (!Game.Data.Constants.GetLtlFont())
                    label.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            }

            if ((Game.Data.Constants != null) && !String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font)))
                label.TextColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font));

            return label;
        }

        public static StackLayout ActionPlace()
        {
            StackLayout stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 20,
                BackgroundColor = Color.LightGray
            };

            if ((Game.Data.Constants != null) && !String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.ActionBox)))
                stackLayout.BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.ActionBox));

            return stackLayout;
        }

        public static List<Label> Actions(List<string> actionsLines)
        {
            List<Label> actionLabels = new List<Label>();

            foreach (string actionLine in actionsLines)
            {
                Label actions = new Label();

                string text = actionLine;

                if (text.Contains("BIG|"))
                    actions.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                else
                    actions.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));

                if (text.Contains("BAD|"))
                    actions.TextColor = Color.Red;

                if (text.Contains("GOOD|"))
                    actions.TextColor = Color.Green;

                if (text.Contains("BOLD|"))
                    actions.FontAttributes = FontAttributes.Bold;

                if (text.Contains("HEAD|"))
                {
                    actions.HorizontalTextAlignment = TextAlignment.Center;
                    actions.FontAttributes = FontAttributes.Bold;
                }
                else
                    actions.HorizontalTextAlignment = TextAlignment.Start;

                foreach (string r in new List<string> { "BIG", "GOOD", "BAD", "HEAD", "BOLD" })
                    text = text.Replace(String.Format("{0}|", r), String.Empty);

                actions.Text = text;
                actions.FontFamily = TextFontFamily();

                actionLabels.Add(actions);
            }

            return actionLabels;
        }
    }
}

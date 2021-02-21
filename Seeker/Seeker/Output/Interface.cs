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
        public enum TextFontSize { little, normal, big };

        private static double BIG_FONT = 25;

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

        public static List<VerticalText> AdditionalStatusBar(List<string> statusLines)
        {
            List<VerticalText> statusLabels = new List<VerticalText>();

            foreach (string status in statusLines)
                statusLabels.Add(new Output.VerticalText { Value = status });

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
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
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

                    string background = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);

                    if (String.IsNullOrEmpty(background))
                        background = "#bdbdbd";

                    StackLayout splitterForm = new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 25,
                        BackgroundColor = Color.FromHex(background),
                    };

                    splitterForm.Children.Add(splitter);

                    enemies.Add(splitterForm);
                }
                else
                {
                    string[] enemyParam = enemyLine.Split('\n');

                    int index = 0;

                    foreach(string line in enemyParam)
                    {
                        Label enemy = new Label()
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                        };

                        if (index > 0)
                        {
                            enemy.Text = line;
                            enemy.Margin = new Thickness(0, -10, 0, 0);
                            enemy.FontFamily = TextFontFamily();
                        }
                        else
                        {
                            enemy.Text = line.ToUpper();
                            enemy.FontFamily = TextFontFamily(bold: true);
                            enemy.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                        }

                        enemies.Add(enemy);

                        index += 1;
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

        public static string TextFontFamily(bool bold = false)
        {
            string defaultFont = String.Format("{0}{1}", "YanoneFont", (bold ? "Bold" : String.Empty));

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
                FontSize = Device.GetNamedSize((defaultParams ? NamedSize.Large : NamedSize.Medium), typeof(Label)),
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

                if (Game.Data.Constants.GetFontSize() == TextFontSize.normal)
                    label.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                else if (Game.Data.Constants.GetFontSize() == TextFontSize.big)
                    label.FontSize = BIG_FONT;
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
                bool bold = false;

                Dictionary<string, Color?> textTypes = new Dictionary<string, Color?>
                {
                    ["RED|"] = Color.Red,
                    ["BLUE|"] = Color.Blue,
                    ["YELLOW|"] = Color.Yellow,
                    ["GREEN|"] = Color.Green,
                    ["GRAY|"] = Color.Gray,
                    ["BAD|"] = Color.Red,
                    ["GOOD|"] = Color.Green,
                    ["BIG|"] = null,
                    ["BOLD|"] = null,
                    ["HEAD|"] = null,
                };

                foreach(string color in textTypes.Keys)
                    if (text.Contains(color))
                        actions.TextColor = textTypes[color] ?? actions.TextColor;

                actions.FontSize = Device.GetNamedSize(text.Contains("BIG|") ? NamedSize.Large : NamedSize.Medium, typeof(Label));

                if (text.Contains("BOLD|"))
                    bold = true;

                if (text.Contains("HEAD|"))
                {
                    actions.HorizontalTextAlignment = TextAlignment.Center;
                    actions.FontAttributes = FontAttributes.Bold;
                }
                else
                    actions.HorizontalTextAlignment = TextAlignment.Start;

                foreach (string key in textTypes.Keys)
                    text = text.Replace(key, String.Empty);

                actions.Text = text;
                actions.FontFamily = TextFontFamily(bold: bold);

                actionLabels.Add(actions);
            }

            return actionLabels;
        }
    }
}

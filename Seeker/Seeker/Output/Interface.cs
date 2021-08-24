using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Seeker.Gamebook;
using System.Linq;

namespace Seeker.Output
{
    class Interface
    {
        public enum TextFontSize { micro, small, little, normal, big, nope };

        public static Image GamebookImage(Description gamebookDescr) => new Image
        {
            Source = gamebookDescr.Illustration,
            Aspect = Aspect.AspectFill,
        };

        public static Image IllustrationImage(string image) => new Image
        {
            Source = image,
            Aspect = Aspect.AspectFit,
        };

        public static Entry Field(object binding) => new Entry
        {
            Placeholder = "Введите свой ответ",
            BindingContext = binding,
            FontFamily = Output.Interface.TextFontFamily(),
        };

        public static List<Label> StatusBar(List<string> statusLines)
        {
            List<Label> statusLabels = new List<Label>();

            string textColor = Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusFont);

            foreach (string status in statusLines)
            {
                Label label = new Label
                {
                    Text = status + System.Convert.ToChar(160),
                    FontSize = Constants.STATUSBAR_FONT,
                    TextColor = (String.IsNullOrEmpty(textColor) ? Color.White : Color.FromHex(textColor)),
                    BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBar)),

                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                statusLabels.Add(label);
            }

            return statusLabels;
        }

        public static List<VerticalText> AdditionalStatusBar(List<string> statusLines)
        {
            List<VerticalText> statusLabels = new List<VerticalText>();

            bool whiteColor = !String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.AdditionalFont));

            foreach (string status in statusLines)
                statusLabels.Add(new VerticalText { Value = status, WhiteColor = whiteColor });

            return statusLabels;
        }

        public static Button GamebookButton(string gamebook)
        {
            Description description = Gamebook.List.GetDescription(gamebook);

            Button gamebookButton = new Button
            {
                Text = gamebook,
                BackgroundColor = Color.FromHex(description.BookColor),
                FontFamily = TextFontFamily(),
                FontSize = FontSize(TextFontSize.normal),
            };

            if (!String.IsNullOrEmpty(description.BorderColor))
            {
                gamebookButton.BorderColor = Color.FromHex(description.BorderColor);
                gamebookButton.BorderWidth = Constants.BORDER_WIDTH;
            }

            if (!String.IsNullOrEmpty(description.FontColor))
                gamebookButton.TextColor = Color.FromHex(description.FontColor);
            else
                gamebookButton.TextColor = Color.White;

            return gamebookButton;
        }

        public static Label GamebookDisclaimer(string gamebook, bool withOut = true)
        {
            Label disclaimer = new Label
            {
                Text = String.Format("© {0}", Gamebook.List.GetDescription(gamebook).SmallDisclaimer),
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };

            if (withOut)
                disclaimer.Margin = new Thickness(0, 0, 0, Constants.DISCLAIMER_BORDER);

            return disclaimer;
        }

        public static Label LinkDisclaimer(string color) => new Label()
        {
            Text = "➝ подробнее",
            HorizontalTextAlignment = TextAlignment.Start,
            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            TextColor = Color.FromHex(color),
            FontAttributes = FontAttributes.Bold,
        };

        public static void GamebookDisclaimerAdd(string gamebook, ref StackLayout options)
        {
            Description description = Gamebook.List.GetDescription(gamebook);

            if (!String.IsNullOrEmpty(description.FullDisclaimer))
            {
                Frame disclaimerBorder = new Frame
                {
                    BorderColor = Color.FromHex(description.BookColor),
                    Margin = new Thickness(0, 0, 0, Constants.DISCLAIMER_BORDER),
                    IsVisible = false,
                };

                TapGestureRecognizer close = new TapGestureRecognizer();
                close.Tapped += (s, e) => disclaimerBorder.IsVisible = false;

                Label discliamerText = new Label
                {
                    Text = description.FullDisclaimer,
                    Margin = new Thickness(5, 5, 5, 5),
                };

                discliamerText.GestureRecognizers.Add(close);
                disclaimerBorder.GestureRecognizers.Add(close);

                TapGestureRecognizer open = new TapGestureRecognizer();
                open.Tapped += (s, e) =>
                {
                    disclaimerBorder.IsVisible = !disclaimerBorder.IsVisible;
                    disclaimerBorder.ForceLayout();
                };

                StackLayout textLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 0),
                };

                Label smallText = GamebookDisclaimer(gamebook);
                smallText.GestureRecognizers.Add(open);
                textLayout.Children.Add(smallText);

                Label linkText = LinkDisclaimer(description.BookColor);
                linkText.GestureRecognizers.Add(open);

                textLayout.Children.Add(linkText);
                options.Children.Add(textLayout);

                disclaimerBorder.Content = discliamerText;
                options.Children.Add(disclaimerBorder);
            }
            else
                options.Children.Add(GamebookDisclaimer(gamebook, withOut: true));
        }

        public static List<View> Represent(List<string> enemiesLines)
        {
            List<View> enemies = new List<View>();

            foreach (string enemyLine in enemiesLines)
            {
                if (enemyLine.Contains("SPLITTER|"))
                {
                    string[] param = enemyLine.Split('|');

                    Label splitter = new Label
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = param[1],
                    };

                    string background = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);

                    if (String.IsNullOrEmpty(background))
                        background = "#bdbdbd";

                    StackLayout splitterForm = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = Constants.SPLITTER_HIGHT,
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
                        Label enemy = new Label { HorizontalTextAlignment = TextAlignment.Center };

                        if (index > 0)
                        {
                            enemy.Text = line;
                            enemy.Margin = new Thickness(0, Constants.REPRESENT_PADDING, 0, 0);
                            enemy.FontFamily = TextFontFamily();
                        }
                        else
                        {
                            enemy.Text = line.ToUpper();
                            enemy.FontFamily = TextFontFamily(bold: true);
                            enemy.FontSize = FontSize(TextFontSize.normal);
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
            string colorLine = Game.Data.Constants.GetButtonsColor(Buttons.ButtonTypes.Continue);
            
            Color color = Color.Gray;

            if (!String.IsNullOrEmpty(colorLine))
                color = Color.FromHex(colorLine);

            Button gameoverButton = new Button
            {
                Text = text,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = color,
                FontFamily = TextFontFamily(),
                FontSize = FontSize(TextFontSize.normal),
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

        public static string TextFontFamily(bool bold = false, bool italic = false, bool standart = false)
        {
            string font = String.Empty;

            if (standart || (Game.Data.Constants == null) || String.IsNullOrEmpty(Game.Data.Constants.GetFont()))
                font = (bold ? "YanoneFontBold" : "YanoneFont");
            else
                font = String.Format("{0}{1}", Game.Data.Constants.GetFont(), (bold ? "Bold" : String.Empty));

            if (italic)
                font = "RobotoFontItalic";

            OnPlatform<string> OnPlatformDic = (OnPlatform<string>)App.Current.Resources[font];
            var fontFamily = OnPlatformDic.Platforms.FirstOrDefault((arg) => arg.Platform.FirstOrDefault() == Device.RuntimePlatform).Value;

            return fontFamily.ToString();
        }

        private static double FontSize(TextFontSize size)
        {
            switch (size)
            {
                case TextFontSize.normal:
                    return Device.GetNamedSize(NamedSize.Large, typeof(Label));

                case TextFontSize.small:
                    return Device.GetNamedSize(NamedSize.Small, typeof(Label));

                case TextFontSize.micro:
                    return Device.GetNamedSize(NamedSize.Micro, typeof(Label));

                case TextFontSize.big:
                    return Constants.BIG_FONT;

                case TextFontSize.little:
                default:
                    return Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            }
        }

        public static ExtendedLabel Text(Text text)
        {
            ExtendedLabel label = Text(text.Content);

            if (text.Bold)
                label.FontFamily = TextFontFamily(bold: true);
            else if (text.Italic)
            {
                label.FontFamily = TextFontFamily(italic: true);
                label.FontSize = FontSize(TextFontSize.little);
            }

            if (text.Alignment == "Center")
                label.HorizontalTextAlignment = TextAlignment.Center;

            if (text.Size != TextFontSize.nope)
                label.FontSize = FontSize(text.Size);

            return label;
        }

        public static ExtendedLabel Text(string text, bool defaultParams = false)
        {
            bool justyfy = (defaultParams ? false : Game.Data.Constants.JustifyText());

            ExtendedLabel label = new ExtendedLabel
            {
                Margin = Constants.TEXT_LABEL_MARGIN,
                FontSize = FontSize(defaultParams ? TextFontSize.normal : TextFontSize.little),
                Text = System.Text.RegularExpressions.Regex.Unescape(text),
                FontFamily = TextFontFamily(),
                JustifyText = justyfy,
            };

            if (defaultParams)
                return label;

            if (Game.Data.Constants != null)
            {
                if (Game.Data.Constants.GetLineHeight() != null)
                    label.LineHeight = Game.Data.Constants.GetLineHeight() ?? -1;
                else
                    label.LineHeight = Constants.LINE_HEIGHT;

                label.FontSize = FontSize(Game.Data.Constants.GetFontSize());
            }

            if ((Game.Data.Constants != null) && !String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font)))
                label.TextColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font));

            return label;
        }

        public static StackLayout ActionPlace()
        {
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = Constants.ACTIONPLACE_SPACING,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = Constants.ACTIONPLACE_PADDING,
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

                foreach(string color in textTypes.Keys.Where(x => text.Contains(x)))
                    actions.TextColor = textTypes[color] ?? actions.TextColor;

                actions.FontSize = FontSize(text.Contains("BIG|") ? TextFontSize.normal : TextFontSize.little);

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

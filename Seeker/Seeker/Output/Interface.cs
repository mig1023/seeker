using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Seeker.Gamebook;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static void AddSplitters(Description gamebook, ref string lastMarker, ref StackLayout options)
        {
            if ((List.Sort() == Constants.SORT_BY_SETTINGS) && (lastMarker != gamebook.Setting))
                AddSplitter(gamebook.Setting, ref lastMarker, gamebook.Setting, ref options);

            if (List.Sort() == Constants.SORT_BY_AUTHORS)
                AddSplitter(gamebook.AuthorsIndex()[0].ToString(), ref options, ref lastMarker);

            if (List.Sort() == Constants.SORT_BY_TITLE)
                AddSplitter(gamebook.Title[0].ToString(), ref options, ref lastMarker);

            if (List.Sort() == Constants.SORT_BY_PLAYTHROUGH_TIME)
                AddSplitter(Constants.PLAYTHROUGH_TIME[gamebook.PlaythroughTime], ref options, ref lastMarker);
        }

        private static void AddSplitter(string marker, ref StackLayout options, ref string lastMarker)
        {
            if (lastMarker != marker)
                AddSplitter(marker.ToUpper(), ref lastMarker, marker, ref options);
        }

        private static void AddSplitter(string splitter, ref string lastMarker, string marker, ref StackLayout options)
        {
            options.Children.Add(Interface.SortSplitter(splitter));
            lastMarker = marker;
        }

        public static Label SortSplitter(string setting) => new Label
        {
            Text = String.Format("― {0} ―", setting),
            HorizontalTextAlignment = TextAlignment.Center,
            FontSize = Font(NamedSize.Large),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20),
        };

        public static Image IllustrationImage(string image) => new Image
        {
            Source = image,
            Aspect = Aspect.AspectFit,
        };

        public static Entry Field(object binding, EventHandler<TextChangedEventArgs> changed)
        {
            Entry field = new Entry
            {
                Placeholder = "Введите свой ответ",
                BindingContext = binding,
                FontFamily = Interface.TextFontFamily(),
            };

            field.TextChanged += changed;

            return field;
        }
            
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

        private static string AndOtherMark(Description gamebook) =>
            (!String.IsNullOrEmpty(gamebook.Authors) ? " и др." : String.Empty);

        public static Label GamebookDisclaimer(Description gamebook)
        {
            string text = (!String.IsNullOrEmpty(gamebook.Authors) ? gamebook.Authors.Split(',', '-')[0] : gamebook.Author);

            if (List.Sort() == Constants.SORT_BY_AUTHORS)
                text = gamebook.AuthorsIndex();

            string disclaimerText = String.Format("© {0}, {1}", text.Trim() + AndOtherMark(gamebook), gamebook.Year);

            Label disclaimer = new Label
            {
                Text = disclaimerText,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Font(NamedSize.Micro),
            };

            return disclaimer;
        }

        public static Label LinkDisclaimer(string color)
        {
            Label link = new Label()
            {
                Text = Constants.DISCLAIMER_LINK,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Font(NamedSize.Micro),
                TextColor = Color.FromHex(color),
                FontAttributes = FontAttributes.Bold,
            };

            if (link.TextColor == Color.White)
                link.TextColor = Constants.LINK_COLOR_DEFAULT;

            return link;
        }
            
        private static void AddDisclaimerElement(string head, string body,
            ref StackLayout disclaimer, Frame border, bool little = false)
        {
            disclaimer.Children.Add(DisclaimerElement(head, CloseTapped(border), bold: true));
            disclaimer.Children.Add(DisclaimerElement(body, CloseTapped(border), little: little));
        }

        private static Label DisclaimerElement(string text, TapGestureRecognizer click, bool bold = false, bool little = false)
        {
            Label discliamerText = new Label
            {
                Text = text,
                Margin = (bold ? new Thickness(5, 5, 5, 0) : new Thickness(5, 0, 5, 5)),
            };

            if (bold)
                discliamerText.FontAttributes = FontAttributes.Bold;

            if (little)
                discliamerText.FontSize = Font(NamedSize.Micro);

            return LinkedDisclaimerElement(discliamerText, click);
        }

        private static Label LinkedDisclaimerElement(Label element, TapGestureRecognizer click)
        {
            element.GestureRecognizers.Add(click);
            return element;
        }

        private static TapGestureRecognizer OpenTapped(Frame disclaimer)
        {
            TapGestureRecognizer open = new TapGestureRecognizer();
            open.Tapped += (s, e) =>
            {
                disclaimer.IsVisible = !disclaimer.IsVisible;
                disclaimer.ForceLayout();
            };

            return open;
        }

        private static TapGestureRecognizer CloseTapped(Frame disclaimer)
        {
            TapGestureRecognizer close = new TapGestureRecognizer();
            close.Tapped += (s, e) => disclaimer.IsVisible = false;

            return close;
        }

        public static void Footer(ref StackLayout footer, EventHandler settingsHandler)
        {
            StackLayout footerLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Gainsboro,
                Padding = new Thickness(5),
            };

            Label settings = new Label
            {
                Text = "Изменить настройки",
                Margin = new Thickness(30, 15, 15, 15),
            };

            TapGestureRecognizer settingsClick = new TapGestureRecognizer();
            settingsClick.Tapped += settingsHandler;
            footerLayout.GestureRecognizers.Add(settingsClick);

            footerLayout.Children.Add(settings);

            footer.Children.Add(footerLayout);
        }

        public static void GamebookDisclaimerAdd(Description gamebook, ref StackLayout options)
        {
            Frame border = new Frame
            {
                BorderColor = Color.FromHex(gamebook.BookColor),
                Margin = new Thickness(0, 0, 0, Constants.DISCLAIMER_BORDER),
                IsVisible = false,
            };

            StackLayout textLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 5),
            };

            textLayout.Children.Add(LinkedDisclaimerElement(GamebookDisclaimer(gamebook), OpenTapped(border)));
            textLayout.Children.Add(LinkedDisclaimerElement(LinkDisclaimer(gamebook.BookColor), OpenTapped(border)));

            options.Children.Add(textLayout);

            StackLayout disclaimer = new StackLayout();

            if (!String.IsNullOrEmpty(gamebook.Original))
                AddDisclaimerElement(head: "Оригинальное название:", body: gamebook.Original, ref disclaimer, border);

            if (!String.IsNullOrEmpty(gamebook.Authors))
                AddDisclaimerElement(head: "Авторы:", body: Regex.Unescape(gamebook.Authors), ref disclaimer, border);
            else
                AddDisclaimerElement(head: "Автор:", body: gamebook.Author, ref disclaimer, border);

            if (!String.IsNullOrEmpty(gamebook.Translators))
                AddDisclaimerElement(head: "Переводчики:", body: Regex.Unescape(gamebook.Translators), ref disclaimer, border);
            else if (!String.IsNullOrEmpty(gamebook.Translator))
                AddDisclaimerElement(head: "Переводчик:", body: gamebook.Translator, ref disclaimer, border);

            if (!String.IsNullOrEmpty(gamebook.Text))
                AddDisclaimerElement(head: "Описание:", body: Regex.Unescape(gamebook.Text), ref disclaimer, border, little: true);

            string paragraphs = gamebook.ParagraphSizeLine();
            string size = Game.Services.SizeParse(gamebook.Size);
            string separator = (paragraphs.Contains('(') ? "\n" : " / ");
            AddDisclaimerElement(head: "Обьём:", body: String.Join(String.Empty, paragraphs, separator, size), ref disclaimer, border);

            border.GestureRecognizers.Add(CloseTapped(border));

            border.Content = disclaimer;
            options.Children.Add(border);
        }

        public static StackLayout SystemMenu() => new StackLayout()
        {
            Orientation = StackOrientation.Horizontal,
            Spacing = Constants.SYS_MENU_SPACING,
            HeightRequest = Constants.SYS_MENU_HIGHT,
            HorizontalOptions = LayoutOptions.FillAndExpand,
        };

        public static StackLayout DebugInformation(int id)
        {
            StackLayout info = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 3,
                Margin = new Thickness(0, 10, 0, 0),
            };

            Color fontColor = Color.Gray;

            if ((Game.Data.Constants != null) && !String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font)))
                fontColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font));

            info.Children.Add(Line(fontColor, "Текущий параграф: {0}", id));

            if (Game.Data.Path.Count > 1)
                info.Children.Add(Line(fontColor, "Путь: {0}", String.Join(" -> ", Game.Data.Path)));

            if (Game.Data.Triggers.Count > 0)
                info.Children.Add(Line(fontColor, "Триггеры: {0}", String.Join(", ", Game.Data.Triggers)));

            string debug = Game.Data.Debug();

            if (!String.IsNullOrEmpty(debug))
                info.Children.Add(Line(fontColor, "{0}", debug));

            List<string> healings = Game.Healing.Debug();

            if (healings.Count > 0)
                info.Children.Add(Line(fontColor, "Снаряжение:\n{0}", String.Join("\n", healings)));

            return info;
        }

        private static Label Line(Color color, string line, params object[] prms) =>
            new Label { Text = String.Format(line, prms), FontSize = FontSize(TextFontSize.micro), TextColor = color };

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

                    string background = Game.Data.Constants.GetColor(Buttons.ButtonTypes.Continue);

                    StackLayout splitterForm = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = Constants.SPLITTER_HIGHT,
                        BackgroundColor = (String.IsNullOrEmpty(background) ? Constants.SPLITTER_COLOR_DEFAULT : Color.FromHex(background)),
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
                            enemy.FontSize = FontSize(TextFontSize.small);
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

        public static string TextFontFamily(bool bold = false, bool italic = false, bool standart = false)
        {
            string font = String.Empty;
            int fontSetting = Game.Settings.GetValue("FontType");

            if (fontSetting > 0)
            {
                font = String.Format("{0}{1}", Constants.FONT_TYPE_VALUES[fontSetting], (bold ? "Bold" : String.Empty));
            }
            else if (standart || (Game.Data.Constants == null) || String.IsNullOrEmpty(Game.Data.Constants.GetFont()))
            {
                font = (bold ? "YanoneFontBold" : "YanoneFont");
            }
            else
            {
                font = String.Format("{0}{1}", Game.Data.Constants.GetFont(), (bold ? "Bold" : String.Empty));
            }
                
            if (italic)
            {
                font = "RobotoFontItalic";
            }

            return TextFontFamily(font);
        }

        public static string TextFontFamily(string fontName)
        {
            OnPlatform<string> OnPlatformDic = (OnPlatform<string>)App.Current.Resources[fontName];
            var fontFamily = OnPlatformDic.Platforms.FirstOrDefault((arg) => arg.Platform.FirstOrDefault() == Device.RuntimePlatform).Value;

            return fontFamily.ToString();
        }

        public static double FontSize(TextFontSize size, bool italic = false)
        {
            if (Game.Settings.GetValue("FontType") == 1)
                italic = false;

            if (italic && Constants.FontSizeItalic.ContainsKey(size))
                return Constants.FontSizeItalic[size];
            else if (!italic && Constants.FontSize.ContainsKey(size))
                return Constants.FontSize[size];
            else
                return Font(NamedSize.Medium);
        }

        public static double Font(NamedSize namedSize)
        {
            if (namedSize == NamedSize.Default)
            {
                bool robotoFont = (Game.Settings.GetValue("FontType") == 1);
                NamedSize size = (robotoFont ? NamedSize.Medium : NamedSize.Large);
                return Device.GetNamedSize(size, typeof(Label));
            }
            else
                return Device.GetNamedSize(namedSize, typeof(Label));
        }

        private static string RedStyle(string line) =>
            Game.Settings.IsEnabled("RedStyle") ? line.Replace("\\n\\n", "\\n\\t\\t\\t\\t") : line;

        public static View TextBySelect(Text text) =>
            text.Selected ? (View)SelectedText(text) : Text(text);

        public static ExtendedLabel Text(Text text)
        {
            ExtendedLabel label = Text(RedStyle(text.Content), italic: text.Italic, size: text.Size, selected: text.Selected);
            label.FontFamily = TextFontFamily(bold: text.Bold, italic: text.Italic);

            if (text.Alignment == "Center")
                label.HorizontalTextAlignment = TextAlignment.Center;
            else if (text.Alignment == "Right")
                label.HorizontalTextAlignment = TextAlignment.End;

            return label;
        }

        public static Grid SelectedText(Text text)
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition()
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(2) },
                    new ColumnDefinition { }
                },
                Margin = Constants.TEXT_LABEL_MARGIN,
            };

            grid.Children.Add(new BoxView { Color = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBar)) }, 0, 0);
            grid.Children.Add(Text(text), 1, 0);

            return grid;
        }

        public static ExtendedLabel Text(string text, bool defaultParams = false, bool italic = false,
            TextFontSize size = TextFontSize.nope, bool selected = false)
        {
            bool justyfy = (defaultParams ? false : (Game.Settings.IsEnabled("Justyfy")));

            ExtendedLabel label = new ExtendedLabel
            {
                FontSize = FontSize(defaultParams ? TextFontSize.normal : TextFontSize.little),
                Text = Regex.Unescape(RedStyle(text)),
                FontFamily = TextFontFamily(),
                JustifyText = justyfy,
                LineHeight = Constants.LINE_HEIGHT,
            };

            if (!selected)
                label.Margin = Constants.TEXT_LABEL_MARGIN;

            if (defaultParams)
                return label;

            int fontSize = Game.Settings.GetValue("FontSize");

            if (fontSize > 0)
            {
                label.FontSize = Constants.FONT_SIZE_VALUES[fontSize];
            }
            else if (size != TextFontSize.nope)
            {
                label.FontSize = FontSize(size);
            }
            else if(Game.Data.Constants != null)
            {
                label.FontSize = FontSize(Game.Data.Constants.GetFontSize(), italic: italic);
            }

            if ((Game.Data.Constants != null) && !String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font)))
            {
                label.TextColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font));
            }

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

        public static StackLayout MultipleButtonsPlace() => new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Spacing = Constants.ACTIONPLACE_SPACING,
            HorizontalOptions = LayoutOptions.FillAndExpand,
        };

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

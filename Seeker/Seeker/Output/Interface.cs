﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Seeker.Gamebook;
using System.Linq;
using System.Text.RegularExpressions;
using static Seeker.Game.Data;

namespace Seeker.Output
{
    class Interface
    {
        public enum TextFontSize { Micro, Small, Little, Normal, Big, nope };

        public static Image GamebookImage(Description gamebookDescr) => new Image
        {
            Source = gamebookDescr.Illustration,
            Aspect = Aspect.AspectFill,
        };

        public static void AddSplitters(Description gamebook, ref string lastMarker, ref StackLayout options)
        {
            if ((List.Sort() == Constants.SortBy["Setting"]) && (lastMarker != gamebook.Setting))
                AddSplitter(gamebook.Setting, ref lastMarker, gamebook.Setting, ref options);

            if (List.Sort() == Constants.SortBy["Author"])
                AddSplitter(gamebook.AuthorsIndex()[0].ToString(), ref options, ref lastMarker);

            if (List.Sort() == Constants.SortBy["Title"])
                AddSplitter(gamebook.Title[0].ToString(), ref options, ref lastMarker);

            if (List.Sort() == Constants.SortBy["Time"])
                AddSplitter(gamebook.PlaythroughTime, ref options, ref lastMarker);   
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
            Text = $"― {setting} ―",
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

        private static int ClearLen(string line) =>
            line.Replace("CROSSEDOUT|", String.Empty).Length;

        public static List<VerticalText> AdditionalStatusBar(List<string> statusLines)
        {
            List<VerticalText> statusLabels = new List<VerticalText>();

            bool whiteColor = !String.IsNullOrEmpty(Game.Data.Constants.GetColor(ColorTypes.AdditionalFont));

            double heightPart = statusLines.Count == 0 ? 1 :
                (int)Application.Current.MainPage.Height / statusLines.Sum(x => ClearLen(x));

            foreach (string status in statusLines)
            {
                VerticalText text = new VerticalText
                {
                    Value = status,
                    WhiteColor = whiteColor,
                    HeightRequest = ClearLen(status) * heightPart,
                };

                statusLabels.Add(text);
            }

            return statusLabels;
        }

        private static string AndOtherMark(Description gamebook) =>
            !String.IsNullOrEmpty(gamebook.Authors) ? " и др." : String.Empty;

        public static Label GamebookDisclaimer(Description gamebook)
        {
            string text = !String.IsNullOrEmpty(gamebook.Authors) ?
                gamebook.Authors.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries)[0] : gamebook.Author;

            if (List.Sort() == Constants.SortBy["Author"])
                text = gamebook.AuthorsIndex();

            string disclaimerText = $"© {text.Trim() + AndOtherMark(gamebook)}, {gamebook.Year}";

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
            ref StackLayout disclaimer, Frame border, Label change, bool little = false)
        {
            disclaimer.Children.Add(DisclaimerElement(head, CloseTapped(border, change), bold: true));
            disclaimer.Children.Add(DisclaimerElement(body, CloseTapped(border, change), little: little));
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

        private static TapGestureRecognizer OpenTapped(Frame disclaimer, Label changedPart)
        {
            TapGestureRecognizer open = new TapGestureRecognizer();
            open.Tapped += (s, e) =>
            {
                disclaimer.IsVisible = !disclaimer.IsVisible;

                changedPart.Text = disclaimer.IsVisible ?
                    Constants.DISCLAIMER_LINK_OPENED : Constants.DISCLAIMER_LINK;

                disclaimer.ForceLayout();
            };

            return open;
        }

        private static TapGestureRecognizer CloseTapped(Frame disclaimer, Label changedPart)
        {
            TapGestureRecognizer close = new TapGestureRecognizer();

            close.Tapped += (s, e) =>
            {
                disclaimer.IsVisible = false;
                changedPart.Text = Constants.DISCLAIMER_LINK;
            };

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

            Label change = LinkDisclaimer(gamebook.BookColor);

            textLayout.Children.Add(LinkedDisclaimerElement(GamebookDisclaimer(gamebook), OpenTapped(border, change)));
            textLayout.Children.Add(LinkedDisclaimerElement(change, OpenTapped(border, change)));

            options.Children.Add(textLayout);

            StackLayout disclaimer = new StackLayout();

            if (!String.IsNullOrEmpty(gamebook.Original))
            {
                AddDisclaimerElement("Оригинальное название:",
                    gamebook.Original, ref disclaimer, border, change);
            }
                
            if (!String.IsNullOrEmpty(gamebook.Authors))
            {
                AddDisclaimerElement("Авторы:",
                    Regex.Unescape(gamebook.Authors), ref disclaimer, border, change);
            }
            else
            {
                AddDisclaimerElement("Автор:",
                    gamebook.Author, ref disclaimer, border, change);
            }

            if (!String.IsNullOrEmpty(gamebook.Translators))
            {
                AddDisclaimerElement("Переводчики:",
                    Regex.Unescape(gamebook.Translators), ref disclaimer, border, change);
            }
            else if (!String.IsNullOrEmpty(gamebook.Translator))
            {
                AddDisclaimerElement("Переводчик:",
                    gamebook.Translator, ref disclaimer, border, change);
            }

            if (!String.IsNullOrEmpty(gamebook.Text))
            {
                AddDisclaimerElement("Описание:",
                    Regex.Unescape(gamebook.Text), ref disclaimer, border, change, little: true);
            }

            string paragraphs = gamebook.ParagraphSizeLine();
            string size = Game.Services.SizeParse(gamebook.Size);
            string separator = (paragraphs.Contains('(') ? "\n" : " / ");
            AddDisclaimerElement("Обьём:", String.Join(String.Empty, paragraphs, separator, size), ref disclaimer, border, change);

            border.GestureRecognizers.Add(CloseTapped(border, change));

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

            if (ColorFormConstants(Game.Data.ColorTypes.Font, out string color))
                fontColor = Color.FromHex(color);

            info.Children.Add(Line(fontColor, $"Текущий параграф: {id}"));

            if (Game.Data.Path.Count > 1)
                info.Children.Add(Line(fontColor, $"Путь: {String.Join(" -> ", Game.Data.Path)}"));

            if (Game.Data.Triggers.Count > 0)
                info.Children.Add(Line(fontColor, $"Триггеры: {String.Join(", ", Game.Data.Triggers)}"));

            info.Children.Add(Interface.SplitterLine(new Thickness(0, 15), Color.LightGray));

            string debug = Game.Data.Debug();

            if (!String.IsNullOrEmpty(debug))
            {
                List<string> debugLines = debug.Split('\n').Where(x => !String.IsNullOrEmpty(x)).ToList();

                if (debugLines.Count > 3)
                    info.Children.Add(GridDebugParams(debugLines, fontColor));
                else
                    info.Children.Add(Line(fontColor, debug));
            }

            List<string> healings = Game.Healing.Debug();

            if (healings.Count > 0)
            {
                info.Children.Add(Interface.SplitterLine(new Thickness(0, 15), Color.LightGray));
                info.Children.Add(Line(fontColor, "Снаряжение:"));

                for (int i = 0; i < healings.Count; i++)
                    info.Children.Add(Line(fontColor, "{i + 1}. {healings[i]}"));
            }

            return info;
        }

        private static View GridDebugParams(List<string> debugLines, Color fontColor)
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = Constants.DEBUG_GRIDROW_HEIGHT },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
                Padding = 0,
                RowSpacing = 0,
            };

            int currentRow = 0;

            for (int i = 0; i < debugLines.Count; i++)
            {
                int column = 1 - ((i + 1) % 2);
                grid.Children.Add(Line(fontColor, debugLines[i]), column, currentRow);

                if ((i > 0) && (column == 1))
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = Constants.DEBUG_GRIDROW_HEIGHT });
                    currentRow += 1;
                }
            }

            return grid;
        }

        private static Label Line(Color color, string line) => new Label
        {
            Text = line, 
            FontSize = FontSize(TextFontSize.Micro),
            TextColor = color,
        };

        public static View SplitterLine(Thickness? thickness, Color? color) => new BoxView
        {
            HeightRequest = 1,
            WidthRequest = 10,
            Color = color ?? Color.Black,
            Margin = thickness ?? new Thickness(0),
        };

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

                    enemies.Add(SplitterLine(new Thickness(0, 10, 0, -2), null));
                    enemies.Add(splitter);
                    enemies.Add(SplitterLine(new Thickness(0, -2, 0, 10), null));
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
                            enemy.FontSize = FontSize(TextFontSize.Small);
                        }
                        else
                        {
                            enemy.Text = line.ToUpper();
                            enemy.FontFamily = TextFontFamily(bold: true);
                            enemy.FontSize = FontSize(TextFontSize.Normal);
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
            string boldLine = bold ? "Bold" : String.Empty;

            if (fontSetting > 0)
            {
                font = $"{Constants.FONT_TYPE_VALUES[fontSetting]}{boldLine}";
            }
            else if (standart || (Game.Data.Constants == null) || String.IsNullOrEmpty(Game.Data.Constants.GetFont()))
            {
                font = bold ? "YanoneFontBold" : "YanoneFont";
            }
            else
            {
                font = $"{Game.Data.Constants.GetFont()}{boldLine}";
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
            {
                return Constants.FontSizeItalic[size];
            }
            else if (!italic && Constants.FontSize.ContainsKey(size))
            {
                return Constants.FontSize[size];
            }
            else
            {
                return Font(NamedSize.Medium);
            }
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
            {
                return Device.GetNamedSize(namedSize, typeof(Label));
            }
        }

        private static string RedStyle(string line) =>
            Game.Settings.IsEnabled("RedStyle") ? line.Replace("\\n\\n", "\\n\\t\\t\\t\\t") : line;

        public static View TextBySelect(Text text)
        {
            if (text.Selected)
            {
                return SelectedText(text);
            }
            else if (text.Box)
            {
                return BoxedText(text);
            }
            else
            {
                return Text(text);
            }
        }

        public static ExtendedLabel Text(Text text)
        {
            bool selected = text.Selected || text.Box;

            ExtendedLabel label = Text(RedStyle(text.Content), italic: text.Italic, size: text.Size, selected: selected);
            label.FontFamily = TextFontFamily(bold: text.Bold, italic: text.Italic);

            if (text.Alignment == "Center")
            {
                label.HorizontalTextAlignment = TextAlignment.Center;

                if (text.Bold)
                    label.Margin = new Thickness(Constants.TEXT_LABEL_MARGIN, Constants.TITLE_TXT_LABEL_MARGIN);
            }
            else if (text.Alignment == "Right")
            {
                label.HorizontalTextAlignment = TextAlignment.End;
            }

            return label;
        }

        private static View BoxedText(Text text)
        {
            string backgroundColor = Game.Data.Constants.GetColor(ColorTypes.Background);

            if (!String.IsNullOrEmpty(text.Background))
            {
                backgroundColor = text.Background;
            }
            else if (String.IsNullOrEmpty(backgroundColor))
            {
                backgroundColor = Constants.DEFAULT_COLORS[ColorTypes.BookColor];
            }

            StackLayout content = new StackLayout
            {
                BackgroundColor = Color.FromHex(backgroundColor),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(Constants.BOX_PADDING),
                Children = { Text(text) },
            };

            string borderColor = Game.Data.Constants.GetColor(ColorTypes.Font);

            if (String.IsNullOrEmpty(borderColor))
                borderColor = Constants.DEFAULT_COLORS[ColorTypes.Font];

            StackLayout border = new StackLayout
            {
                BackgroundColor = Color.FromHex(borderColor),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(Constants.BOX_BORDER),
                Children = { content },
            };

            return border;
        }

        private static View SelectedText(Text text)
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
                    new ColumnDefinition { Width = new GridLength(2) },
                    new ColumnDefinition { }
                },
                Margin = Constants.TEXT_LABEL_MARGIN,
            };

            BoxView verticalLine = new BoxView
            {
                Color = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.StatusBar)),
                Margin = new Thickness(0, 3, 0, 0)
            };

            grid.Children.Add(verticalLine, 0, 0);
            grid.Children.Add(Text(text), 2, 0);

            return grid;
        }

        public static ExtendedLabel Text(string text, bool defaultParams = false, bool italic = false, bool bold = false,
            TextFontSize size = TextFontSize.nope, bool selected = false)
        {
            bool justyfy = (defaultParams ? false : (Game.Settings.IsEnabled("Justyfy")));

            ExtendedLabel label = new ExtendedLabel
            {
                FontSize = FontSize(defaultParams ? TextFontSize.Normal : TextFontSize.Little),
                Text = Regex.Unescape(RedStyle(text)),
                FontFamily = TextFontFamily(bold: bold),
                JustifyText = justyfy,
                LineHeight = Constants.LINE_HEIGHT,
            };

            if (bold)
                label.FontAttributes = FontAttributes.Bold;

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
            else if (Game.Data.Constants != null)
            {
                label.FontSize = FontSize(Game.Data.Constants.GetFontSize(), italic: italic);
            }

            if (ColorFormConstants(Game.Data.ColorTypes.Font, out string color))
                label.TextColor = Color.FromHex(color);

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

            if (ColorFormConstants(Game.Data.ColorTypes.ActionBox, out string color))
                stackLayout.BackgroundColor = Color.FromHex(color);

            return stackLayout;
        }

        private static bool ColorFormConstants(Game.Data.ColorTypes colorType, out string color)
        {
            color = String.Empty;

            if (Game.Data.Constants == null)
                return false;

            color = Game.Data.Constants.GetColor(colorType);

            return !String.IsNullOrEmpty(color);
        }

        public static StackLayout MultipleButtonsPlace() => new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Spacing = Constants.ACTIONPLACE_SPACING,
            HorizontalOptions = LayoutOptions.FillAndExpand,
        };

        private static Color GetGoodColors(Game.Data.ColorTypes color, Color defaultColor)
        {
            string hexColor = Game.Data.Constants.GetColor(color);
            return String.IsNullOrEmpty(hexColor) ? defaultColor : Color.FromHex(hexColor);
        }

        public static List<View> Actions(List<string> actionsLines)
        {
            List<View> actionLabels = new List<View>();

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
                    ["BAD|"] = GetGoodColors(ColorTypes.BadColor, Color.Red),
                    ["GOOD|"] = GetGoodColors(ColorTypes.GoodColor, Color.Green),
                    ["BIG|"] = null,
                    ["BOLD|"] = null,
                    ["HEAD|"] = null,
                    ["LINE|"] = null,
                };

                foreach (string color in textTypes.Keys.Where(x => text.Contains(x)))
                    actions.TextColor = textTypes[color] ?? actions.TextColor;

                actions.FontSize = FontSize(text.Contains("BIG|") ? TextFontSize.Normal : TextFontSize.Little);

                if (text.Contains("BOLD|"))
                    bold = true;

                if (text.Contains("HEAD|"))
                {
                    actions.HorizontalTextAlignment = TextAlignment.Center;
                    actions.FontAttributes = FontAttributes.Bold;
                }
                else
                {
                    actions.HorizontalTextAlignment = TextAlignment.Start;
                }

                if (text.Contains("LINE|"))
                    actionLabels.Add(SplitterLine(null, null));

                foreach (string key in textTypes.Keys)
                    text = text.Replace(key, String.Empty);

                actions.Text = text;
                actions.FontFamily = TextFontFamily(bold: bold);

                actionLabels.Add(actions);
            }

            return actionLabels;
        }

        public static Entry BookmarkField()
        {
            Entry field = new Entry
            {
                Placeholder = Constants.BOOKMARK_SAVE_HOLDER,
                FontFamily = TextFontFamily(),
                FontSize = FontSize(TextFontSize.Big),
                BackgroundColor = Color.Gainsboro,
            };

            if (!String.IsNullOrEmpty(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font)))
                field.TextColor = Color.FromHex(Game.Data.Constants.GetColor(Game.Data.ColorTypes.Font));

            return field;
        }
    }
}

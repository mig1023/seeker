using System;
using Xamarin.Forms;
using Seeker.Gamebook;
using System.Linq;
using System.Text.RegularExpressions;

namespace Seeker.Output
{
    class Disclaimer
    {
        private static string AndOtherMark(Description gamebook) =>
            gamebook.Authors.Count > 1 ? " и др." : String.Empty;

        private static Label GamebookDisclaimer(Description gamebook)
        {
            string text = gamebook.Authors.First();
            string ltlInfo = gamebook.Year.ToString();

            if (List.Sort(Constants.SortBy.Author))
            {
                text = gamebook.AuthorsIndex();
            }

            if (List.Sort(Constants.SortBy.Paragraphs))
            {
                ltlInfo = gamebook.ParagraphSizeLine(split: true);
            }
            else if (List.Sort(Constants.SortBy.Size))
            {
                ltlInfo = gamebook.SizeLine();
            }

            string disclaimerText = $"© {text.Trim() + AndOtherMark(gamebook)}, {ltlInfo}";

            Label disclaimer = new Label
            {
                Text = disclaimerText,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Interface.Font(NamedSize.Micro),
            };

            return disclaimer;
        }

        private static Label Link(string color)
        {
            Label link = new Label()
            {
                Text = Constants.DISCLAIMER_LINK,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Interface.Font(NamedSize.Micro),
                TextColor = Color.FromHex(color),
                FontAttributes = FontAttributes.Bold,
            };

            if (link.TextColor == Color.White)
                link.TextColor = Constants.LINK_COLOR_DEFAULT;

            return link;
        }

        private static void AddElement(string head, string body,
            ref StackLayout disclaimer, Frame border, Label change, bool little = false)
        {
            disclaimer.Children.Add(Element(head, CloseTapped(border, change), bold: true));
            disclaimer.Children.Add(Element(body, CloseTapped(border, change), little: little));
        }

        private static Label Element(string text, TapGestureRecognizer click, bool bold = false, bool little = false)
        {
            Label discliamerText = new Label
            {
                Text = text,
                Margin = (bold ? new Thickness(5, 5, 5, 0) : new Thickness(5, 0, 5, 5)),
            };

            if (bold)
                discliamerText.FontAttributes = FontAttributes.Bold;

            if (little)
                discliamerText.FontSize = Interface.Font(NamedSize.Micro);

            return LinkedElement(discliamerText, click);
        }

        private static Label LinkedElement(Label element, TapGestureRecognizer click)
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

        public static void Gamebook(Description gamebook, ref StackLayout options)
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

            Label change = Link(gamebook.BookColor);

            textLayout.Children.Add(LinkedElement(GamebookDisclaimer(gamebook), OpenTapped(border, change)));
            textLayout.Children.Add(LinkedElement(change, OpenTapped(border, change)));

            options.Children.Add(textLayout);

            StackLayout disclaimer = new StackLayout();

            if (!String.IsNullOrEmpty(gamebook.Original))
            {
                AddElement("Оригинальное название:",
                    gamebook.Original, ref disclaimer, border, change);
            }

            if (gamebook.Authors.Count > 1)
            {
                string authors = String.Join("\n", gamebook.Authors);

                AddElement("Авторы:",
                    authors, ref disclaimer, border, change);
            }
            else
            {
                AddElement("Автор:",
                    gamebook.Authors.First(), ref disclaimer, border, change);
            }

            if (gamebook.Translators.Count > 1)
            {
                string translators = String.Join("\n", gamebook.Translators);

                AddElement("Переводчики:",
                    translators, ref disclaimer, border, change);
            }
            else if (gamebook.Translators.Count > 0)
            {
                AddElement("Переводчик:",
                    gamebook.Translators.First(), ref disclaimer, border, change);
            }

            if (!String.IsNullOrEmpty(gamebook.Text))
            {
                AddElement("Описание:",
                    Regex.Unescape(gamebook.Text), ref disclaimer, border, change, little: true);
            }

            string paragraphs = gamebook.ParagraphSizeLine();
            string size = gamebook.SizeLine();
            string separator = (paragraphs.Contains('(') ? "\n" : " / ");
            AddElement("Обьём:", String.Join(String.Empty, paragraphs, separator, size), ref disclaimer, border, change);

            border.GestureRecognizers.Add(CloseTapped(border, change));

            border.Content = disclaimer;
            options.Children.Add(border);
        }
    }
}

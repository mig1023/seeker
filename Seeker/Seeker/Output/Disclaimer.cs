﻿using System;
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
                text = gamebook.AuthorsIndex(noUpper: true);
            }

            if (List.Sort(Constants.SortBy.Paragraphs))
            {
                ltlInfo = gamebook.ParagraphSizeLine();
            }
            else if (List.Sort(Constants.SortBy.Size))
            {
                ltlInfo = gamebook.SizeLine();
            }
            else if (List.Sort(Constants.SortBy.Texts))
            {
                ltlInfo = gamebook.ParagraphsAverageSizeLine();
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

        private static void AddLtlElement(string head1, string body1, string head2, string body2,
            ref StackLayout disclaimer, Frame border, Label change)
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(Element(head1, CloseTapped(border, change), little: true, bold: true), 0, 0);
            grid.Children.Add(Element(body1, CloseTapped(border, change), little: true), 0, 1);
            grid.Children.Add(Element(head2, CloseTapped(border, change), little: true, bold: true), 1, 0);
            grid.Children.Add(Element(body2, CloseTapped(border, change), little: true), 1, 1);

            disclaimer.Children.Add(grid);
        }

        private static void AddElement(string head, string body, ref StackLayout disclaimer,
            Frame border, Label change, bool little = false, bool littleHead = false)
        {
            disclaimer.Children.Add(Element(head, CloseTapped(border, change), little: littleHead, bold: true));
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

        private static void FullDescription(Frame border, Label changedPart, Description gamebook)
        {
            StackLayout disclaimer = new StackLayout();

            if (!String.IsNullOrEmpty(gamebook.Original))
            {
                AddElement("Оригинальное название:",
                    gamebook.Original, ref disclaimer, border, changedPart);
            }

            if (gamebook.Authors.Count > 1)
            {
                string authors = String.Join("\n", gamebook.Authors);

                AddElement("Авторы:",
                    authors, ref disclaimer, border, changedPart);
            }
            else
            {
                AddElement("Автор:",
                    gamebook.Authors.First(), ref disclaimer, border, changedPart);
            }

            if (gamebook.Translators.Count > 1)
            {
                string translators = String.Join("\n", gamebook.Translators);

                AddElement("Переводчики:",
                    translators, ref disclaimer, border, changedPart);
            }
            else if (gamebook.Translators.Count > 0)
            {
                AddElement("Переводчик:",
                    gamebook.Translators.First(), ref disclaimer, border, changedPart);
            }

            if (!String.IsNullOrEmpty(gamebook.Text))
            {
                AddElement("Описание:",
                    Regex.Unescape(gamebook.Text), ref disclaimer, border, changedPart, little: true);
            }

            int paragraphSize = int.Parse(gamebook.Size) / gamebook.ParagraphSize();
            string sizeLine = Game.Services.CoinsNoun(paragraphSize, "слово", "слова", "слов");

            AddLtlElement("Кол-во параграфов", gamebook.ParagraphSizeLine(full: true),
                "Объём", gamebook.SizeLine(), ref disclaimer, border, changedPart);

            AddLtlElement("Размер параграфа", $"В среднем {paragraphSize} {sizeLine}",
                "Время игры", gamebook.PlaythroughTime, ref disclaimer, border, changedPart);

            if (!String.IsNullOrEmpty(gamebook.Additional))
            {
                AddElement("Дополнительная информация:", gamebook.Additional, ref disclaimer,
                    border, changedPart, little: true);
            }

            border.GestureRecognizers.Add(CloseTapped(border, changedPart));

            border.Content = disclaimer;
        }

        private static TapGestureRecognizer OpenTapped(Frame border, Label changedPart, Description gamebook)
        {
            TapGestureRecognizer open = new TapGestureRecognizer();
            open.Tapped += (s, e) =>
            {
                if (!border.IsVisible)
                {
                    FullDescription(border, changedPart, gamebook);

                    border.IsVisible = true;
                    changedPart.Text = Constants.DISCLAIMER_LINK_OPENED;
                }
                else
                {
                    border.Content = null;
                    border.IsVisible = false;
                    changedPart.Text = Constants.DISCLAIMER_LINK;
                }

                border.ForceLayout();
            };

            return open;
        }

        private static TapGestureRecognizer CloseTapped(Frame border, Label changedPart)
        {
            TapGestureRecognizer close = new TapGestureRecognizer();

            close.Tapped += (s, e) =>
            {
                border.Content = null;
                border.IsVisible = false;
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

            textLayout.Children.Add(LinkedElement(GamebookDisclaimer(gamebook), OpenTapped(border, change, gamebook)));
            textLayout.Children.Add(LinkedElement(change, OpenTapped(border, change, gamebook)));

            options.Children.Add(textLayout);
            options.Children.Add(border);
        }
    }
}

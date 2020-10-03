using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Interface
    {
        public static List<Label> StatusBar(List<string> statusLines)
        {
            List<Label> statusLabels = new List<Label>();

            foreach (string status in statusLines)
            {
                Label label = new Label();

                label.Text = status;
                label.FontSize = 12;
                label.TextColor = Color.White;
                label.BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(Data.ColorTypes.StatusBar));

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
            return new Button()
            {
                Text = gamebook,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = Color.FromHex(Gamebook.List.GetDescription(gamebook).BookColor)
            };
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
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        Text = enemyParam[0],
                        HorizontalTextAlignment = TextAlignment.Center
                    };

                    enemies.Add(enemy);

                    if (enemyParam.Length > 1)
                    {
                        Label param = new Label()
                        {
                            Text = enemyParam[1],
                            HorizontalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(0, -10, 0, 0)
                        };

                        enemies.Add(param);
                    }
                }
            }

            return enemies;
        }

        public static Button ActionButton(string actionName, bool enabled = true)
        {
            string color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Action);

            return new Button()
            {
                Text = actionName,
                TextColor = Xamarin.Forms.Color.White,
                IsEnabled = enabled,
                BackgroundColor = (enabled ? Color.FromHex(color) : Color.Gray)
            };
        }

        public static Button OptionButton(Game.Option option)
        {
            bool optionColor = !String.IsNullOrEmpty(option.OnlyIf) && !option.OnlyIf.Contains(">") && !option.OnlyIf.Contains("<");

            string color = Game.Data.Constants.GetButtonsColor(
                optionColor ? Game.Buttons.ButtonTypes.Option : Game.Buttons.ButtonTypes.Main
            );

            return new Button()
            {
                Text = option.Text,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = Color.FromHex(color)
            };
        }

        public static Label Aftertext(string text)
        {
            Label aftertext = new Label()
            {
                Text = text,
                Margin = 5,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            aftertext.Text = text.Replace("\n", System.Environment.NewLine);

            return aftertext;
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
                    actions.FontSize = 22;
                else
                    actions.FontSize = 10;

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

                actionLabels.Add(actions);
            }

            return actionLabels;
        }
    }
}

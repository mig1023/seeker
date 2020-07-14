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
                label.BackgroundColor = Color.FromHex(Game.Data.Constants.GetStatusBarColor());

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

        public static List<Label> Represent(List<string> enemiesLines)
        {
            List<Label> enemies = new List<Label>();

            foreach (string enemyLine in enemiesLines)
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

                Label param = new Label()
                {
                    Text = enemyParam[1],
                    HorizontalTextAlignment = TextAlignment.Center
                };

                enemies.Add(param);

            }

            return enemies;
        }

        public static Button ActionButton(string actionName)
        {
            return new Button()
            {
                Text = actionName,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Action))
            };
        }

        public static Button OptionButton(Game.Option option)
        {
            string color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Main);

            if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.OpenedOption.Contains(option.OnlyIf))
                return null;
            else if (!String.IsNullOrEmpty(option.OnlyIf))
                color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Option);

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
            return new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 20,
                BackgroundColor = Color.LightGray
            };
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

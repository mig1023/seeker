using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Settings
    {
        private delegate void SettingMethod();

        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> Options { get; set; }
        public string Description { get; set; }

        public static void Add(ref StackLayout settings)
        {
            foreach (Settings setting in Game.Xml.GetXmlSettings())
            {
                SettingOption(setting.Name, setting.Type, setting.Options, ref settings);

                if (!String.IsNullOrEmpty(setting.Description))
                    SettingDescription(setting.Description, ref settings);

                Splitter(ref settings);
            }

            SettingCheatingBlock(ref settings);

            Splitter(ref settings);

            SettingButton("Сбросить закладки", () => Game.Bookmarks.Clean(), ref settings, spacer: true);
            SettingButton("Сбросить сохранённые игры", () => Game.Continue.Clean(), ref settings);
            SettingButton("Сбросить все настройки", () => Game.Settings.Clean(), ref settings);
        }

        private static void SettingCheatingBlock(ref StackLayout settings)
        {
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            Label settingTitle = new Label
            {
                Text = "Читерство",
                VerticalOptions = LayoutOptions.Center,
            };

            stackLayout.Children.Add(settingTitle);

            Label brace = new Label
            {
                Text = "{",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 50,
                FontFamily = Interface.TextFontFamily("RobotoFontThin"),
                Margin = new Thickness(0, -10, 0, 0),
            };

            stackLayout.Children.Add(brace);

            StackLayout twoLine = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            SettingOption("Кнопка 'Назад'", "CheatingBack", null, ref twoLine);

            SettingOption("God mode", "Godmode", null, ref twoLine);

            stackLayout.Children.Add(twoLine);

            settings.Children.Add(stackLayout);
        }

        private static void Splitter(ref StackLayout settings) =>
            settings.Children.Add(Output.Splitter.Line(new Thickness(0, 15), Color.LightGray));

        private static void SettingButton(string settingName, SettingMethod Click, ref StackLayout settings, bool spacer = false)
        {
            Label settingButton = new Label
            {
                Text = settingName,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Interface.Font(NamedSize.Medium),
                TextColor = Color.Black,
                TextDecorations = TextDecorations.Underline,
            };

            if (spacer)
                settingButton.Margin = new Thickness(0, 15, 0, 0);

            TapGestureRecognizer click = new TapGestureRecognizer();
            click.Tapped += (sender, e) => SettingClick(sender, Click);
            settingButton.GestureRecognizers.Add(click);

            settings.Children.Add(settingButton);
        }

        private static void SettingDescription(string settingDescription, ref StackLayout settings)
        {
            Label description = new Label
            {
                Text = settingDescription,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Interface.Font(NamedSize.Micro),
            };

            settings.Children.Add(description);
        }

        private static void SettingOption(string settingName, string settingType, List<string> options, ref StackLayout settings)
        {
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            Label settingTitle = new Label
            {
                Text = $"{settingName}:",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Interface.Font(NamedSize.Small),
            };

            stackLayout.Children.Add(settingTitle);

            if (options == null)
            {
                Switch settingSwitcher = new Switch
                {
                    IsToggled = (Game.Settings.IsEnabled(settingType)),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                };
                settingSwitcher.Toggled += (sender, e) => SwitcherChanged(e, settingType);

                stackLayout.Children.Add(settingSwitcher);
            }
            else
            {
                Picker settingPicker = new Picker
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = Interface.Font(NamedSize.Medium),
                    HorizontalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Constants.PICKER_BACKGROUND,
                };

                settingPicker.SelectedIndexChanged += (sender, e) => SettingChanged(sender, settingType);

                foreach (string option in options)
                    settingPicker.Items.Add(option);

                settingPicker.SelectedIndex = Game.Settings.GetValue(settingType);

                stackLayout.Children.Add(settingPicker);
            }

            settings.Children.Add(stackLayout);
        }

        private static void SettingChanged(object sender, string setting) =>
            Game.Settings.SetValue(setting, (sender as Picker).SelectedIndex);

        private static void SwitcherChanged(ToggledEventArgs e, string setting) =>
            Game.Settings.SetValue(setting, (e.Value ? 1 : 0));

        private static void SettingClick(object sender, SettingMethod method)
        {
            (sender as Label).TextColor = Color.LightGray;
            method();
        }
    }
}

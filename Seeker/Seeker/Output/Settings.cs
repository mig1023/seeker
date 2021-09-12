using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Settings
    {
        public static void Add(ref StackLayout settings)
        {
            settings.Children.Add(SettingLayout("Размер шрифта", "FontSize", Constants.FONT_SIZE_SETTING, fontChanged));
            settings.Children.Add(SettingLayout("Текст по ширине", "Justyfy", Constants.JUSTYFY_SETTING, justyfyChanged));
        }

        private static StackLayout SettingLayout(string settingName, string settingType, List<string> options, EventHandler onClick)
        {
            StackLayout settingLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5, 0),
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label settingTitle = new Label
            {
                Text = settingName,
                VerticalTextAlignment = TextAlignment.Center,
            };

            settingLayout.Children.Add(settingTitle);

            Picker settingPicker = new Picker();

            foreach (string option in options)
                settingPicker.Items.Add(option);

            settingPicker.HorizontalOptions = LayoutOptions.End;
            settingPicker.SelectedIndex = Game.Settings.GetValue(settingType);
            settingPicker.SelectedIndexChanged += onClick;

            settingLayout.Children.Add(settingPicker);

            return settingLayout;
        }

        private static void fontChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("FontSize", (sender as Picker).SelectedIndex);

        private static void justyfyChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("Justyfy", (sender as Picker).SelectedIndex);
    }
}

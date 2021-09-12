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
            StackLayout fontLayout = SettingLayout("Размер шрифта");

            Picker fontPicker = new Picker();

            foreach (string option in Constants.FONT_SIZE_SETTING)
                fontPicker.Items.Add(option);

            fontPicker.HorizontalOptions = LayoutOptions.FillAndExpand;
            fontPicker.SelectedIndex = Game.Settings.GetValue("FontSize");
            fontPicker.SelectedIndexChanged += fontPicker_SelectedIndexChanged;

            fontLayout.Children.Add(fontPicker);
            settings.Children.Add(fontLayout);

            StackLayout justyfyLayout = SettingLayout("Текст по ширине");

            bool justyfyValue = (Game.Settings.GetValue("Justyfy") == 1);

            Switch justyfy = new Switch
            {
                IsToggled = justyfyValue,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                ThumbColor = Color.DarkGray,
                OnColor = Color.Gray,
            };
            justyfy.Toggled += justyfy_Toggled;

            justyfyLayout.Children.Add(justyfy);
            settings.Children.Add(justyfyLayout);
        }

        private static StackLayout SettingLayout(string settingName)
        {
            StackLayout settingLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            Label settingTitle = new Label
            {
                Text = settingName,
                VerticalTextAlignment = TextAlignment.Center,
            };

            settingLayout.Children.Add(settingTitle);

            return settingLayout;
        }

        private static void fontPicker_SelectedIndexChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("FontSize", (sender as Picker).SelectedIndex);

        private static void justyfy_Toggled(object sender, ToggledEventArgs e) =>
            Game.Settings.SetValue("Justyfy", (e.Value ? 1 : 0));
    }
}

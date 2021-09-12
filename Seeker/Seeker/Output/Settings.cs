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
            StackLayout fontLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5),
            };

            Label fontTitle = new Label
            {
                Text = "Размер шрифта",
                VerticalTextAlignment = TextAlignment.Center,
            };

            Picker fontPicker = new Picker();

            foreach (string option in Constants.FONT_SIZE_SETTING)
                fontPicker.Items.Add(option);

            fontPicker.SelectedIndex = Game.Settings.GetValue("FontSize");

            fontLayout.Children.Add(fontTitle);
            fontLayout.Children.Add(fontPicker);

            settings.Children.Add(fontLayout);

            fontPicker.SelectedIndexChanged += fontPicker_SelectedIndexChanged;
        }

        private static void fontPicker_SelectedIndexChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("FontSize", (sender as Picker).SelectedIndex);
    }
}

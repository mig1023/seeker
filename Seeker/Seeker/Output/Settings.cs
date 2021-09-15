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
            Grid settingGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                }
            };

            SettingOption("Основной шрифт", "FontType", Constants.FONT_TYPE_SETTING, FontTypeChanged, ref settingGrid);
            SettingOption("Размер шрифта", "FontSize", Constants.FONT_SIZE_SETTING, FontChanged, ref settingGrid);
            SettingOption("Текст по ширине", "Justyfy", Constants.JUSTYFY_SETTING, JustyfyChanged, ref settingGrid);
            SettingOption("Недоступные опции", "DisabledOption", Constants.OPTION_SETTING, OptionChanged, ref settingGrid);
            SettingOption("Отображать меню", "SystemMenu", Constants.MENU_SETTING, MenuChanged, ref settingGrid);

            SettingButton("Сбросить сохранённые игры", SaveClean, ref settingGrid, spacer: true);

            settings.Children.Add(settingGrid);
        }

        private static void SettingButton(string settingName, EventHandler onClick, ref Grid settingGrid, bool spacer = false)
        {
            int currentRow = AddNewRow(ref settingGrid);

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
            click.Tapped += onClick;
            settingButton.GestureRecognizers.Add(click);

            settingGrid.Children.Add(settingButton, 0, currentRow);
            Grid.SetColumnSpan(settingButton, 2);
        }

        private static void SettingOption(string settingName, string settingType, List<string> options,
            EventHandler onClick, ref Grid settingGrid)
        {
            int currentRow = AddNewRow(ref settingGrid);

            Label settingTitle = new Label
            {
                Text = String.Format("{0}:", settingName),
                VerticalOptions = LayoutOptions.Center,
                FontSize = Interface.Font(NamedSize.Small),
            };

            settingGrid.Children.Add(settingTitle, 0, currentRow);

            Picker settingPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Interface.Font(NamedSize.Medium),
            };
            
            settingPicker.SelectedIndexChanged += onClick;

            foreach (string option in options)
                settingPicker.Items.Add(option);

            settingPicker.SelectedIndex = Game.Settings.GetValue(settingType);

            settingGrid.Children.Add(settingPicker, 1, currentRow);
        }

        private static int AddNewRow(ref Grid settingGrid)
        {
            settingGrid.RowDefinitions.Add(new RowDefinition());
            return settingGrid.RowDefinitions.Count - 1;
        }

        private static void FontTypeChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("FontType", (sender as Picker).SelectedIndex);

        private static void FontChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("FontSize", (sender as Picker).SelectedIndex);

        private static void JustyfyChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("Justyfy", (sender as Picker).SelectedIndex);

        private static void OptionChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("DisabledOption", (sender as Picker).SelectedIndex);

        private static void MenuChanged(object sender, EventArgs e) =>
            Game.Settings.SetValue("SystemMenu", (sender as Picker).SelectedIndex);

        private static void SaveClean(object sender, EventArgs e)
        {
            (sender as Label).TextColor = Color.LightGray;
            Game.Continue.Clean();
        }
    }
}

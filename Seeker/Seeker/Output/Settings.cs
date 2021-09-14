﻿using System;
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

            SettingRow("Основной шрифт", "FontType", Constants.FONT_TYPE_SETTING, FontTypeChanged, ref settingGrid);
            SettingRow("Размер шрифта", "FontSize", Constants.FONT_SIZE_SETTING, FontChanged, ref settingGrid);
            SettingRow("Текст по ширине", "Justyfy", Constants.JUSTYFY_SETTING, JustyfyChanged, ref settingGrid);
            SettingRow("Недоступные опции", "DisabledOption", Constants.OPTION_SETTING, OptionChanged, ref settingGrid);
            SettingRow("Отображать меню", "SystemMenu", Constants.MENU_SETTING, MenuChanged, ref settingGrid);

            settings.Children.Add(settingGrid);
        }

        private static void SettingRow(string settingName, string settingType, List<string> options,
            EventHandler onClick, ref Grid settingGrid)
        {
            settingGrid.RowDefinitions.Add(new RowDefinition());

            int currentRow = settingGrid.RowDefinitions.Count - 1;

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
    }
}

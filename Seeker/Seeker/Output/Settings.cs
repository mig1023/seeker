using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Settings
    {
        private static Grid SettingGrid;
        private delegate void SettingMethod();

        public static void Add(ref StackLayout settings)
        {
            SettingGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 20 },
                    new ColumnDefinition { Width = 12 },
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
            };

            SettingOption("Основной шрифт", "FontType", Constants.FONT_TYPE_SETTING);
            SettingDescription("Выбор шрифта определяет не только отображение текста, но и текст кнопок, а также меню. " +
                "Будьте внимательны: не любой шрифт хорошо подойдёт под настройки некоторых игры.");

            SettingOption("Размер шрифта", "FontSize", Constants.FONT_SIZE_SETTING);
            SettingOption("Текст по ширине", "Justyfy", null);

            SettingOption("Недоступные опции", "DisabledOption", Constants.OPTION_SETTING);
            SettingOption("Отображать меню", "SystemMenu", null);
            SettingOption("Сортировка", "Sort", Constants.SORT_SETTING);
            SettingOption("Без оформления", "WithoutStyles", null);

            SettingCheatingBlock();
            SettingOption("Данные отладки", "Debug", null);

            SettingButton("Сбросить сохранённые игры", () => Game.Continue.Clean(), spacer: true);
            SettingButton("Сбросить все настройки", () => Game.Settings.Clean());

            settings.Children.Add(SettingGrid);
        }

        private static void SettingCheatingBlock()
        {
            int currentRow = AddNewRow(ref SettingGrid);

            VerticalText settingTitle = new VerticalText
            {
                Value = "Читерство",
                VerticalOptions = LayoutOptions.Center,
                LeftRotate = true,
            };

            SettingGrid.Children.Add(settingTitle, 0, currentRow);
            Grid.SetRowSpan(settingTitle, 2);

            Label brace = new Label
            {
                Text = "{",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 50,
                FontFamily = Interface.TextFontFamily("RobotoFontThin"),
                Margin = new Thickness(-5, -10, 0, 0),
            };

            SettingGrid.Children.Add(brace, 1, currentRow);
            Grid.SetRowSpan(brace, 2);

            SettingOption("Кнопка 'Назад'", "CheatingBack", null, row: currentRow);

            currentRow = AddNewRow(ref SettingGrid);

            SettingOption("God mode", "Godmode", null, row: currentRow);
        }     

        private static void SettingButton(string settingName, SettingMethod Click, bool spacer = false)
        {
            int currentRow = AddNewRow(ref SettingGrid);

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

            SettingGrid.Children.Add(settingButton, 0, currentRow);
            Grid.SetColumnSpan(settingButton, 4);
        }

        private static void SettingDescription(string settingDescription)
        {
            int currentRow = AddNewRow(ref SettingGrid);

            Label description = new Label
            {
                Text = settingDescription,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Interface.Font(NamedSize.Micro),
            };

            SettingGrid.Children.Add(description, 0, currentRow);
            Grid.SetColumnSpan(description, 4);
        }

        private static void SettingOption(string settingName, string settingType, List<string> options, int? row = null)
        {
            int currentRow = row ?? AddNewRow(ref SettingGrid);

            Label settingTitle = new Label
            {
                Text = String.Format("{0}:", settingName),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Interface.Font(NamedSize.Small),
            };

            if (row == null)
            {
                SettingGrid.Children.Add(settingTitle, 0, currentRow);
                Grid.SetColumnSpan(settingTitle, 3);
            }
            else
                SettingGrid.Children.Add(settingTitle, 2, currentRow);
             
            if (options == null)
            {
                Switch settingSwitcher = new Switch
                {
                    IsToggled = (Game.Settings.GetValue(settingType) == 1),
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center,
                };
                settingSwitcher.Toggled += (sender, e) => SwitcherChanged(e, settingType);

                SettingGrid.Children.Add(settingSwitcher, 3, currentRow);
            }
            else
            {
                Picker settingPicker = new Picker
                {
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = Interface.Font(NamedSize.Medium),
                };

                settingPicker.SelectedIndexChanged += (sender, e) => SettingChanged(sender, settingType);

                foreach (string option in options)
                    settingPicker.Items.Add(option);

                settingPicker.SelectedIndex = Game.Settings.GetValue(settingType);

                SettingGrid.Children.Add(settingPicker, 3, currentRow);
            }
        }

        private static int AddNewRow(ref Grid settingGrid)
        {
            settingGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
            return settingGrid.RowDefinitions.Count - 1;
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

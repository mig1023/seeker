using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using static Seeker.Game.Data;

namespace Seeker.Output
{
    internal class Debug
    {
        private static Label Line(Color color, string line) => new Label
        {
            Text = line,
            FontSize = Interface.FontSize(Interface.TextFontSize.Micro),
            TextColor = color,
        };

        public static StackLayout Information(int id)
        {
            StackLayout info = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 3,
                Margin = new Thickness(0, 10, 0, 0),
            };

            Color fontColor = Color.Gray;

            if (Interface.ColorFormConstants(ColorTypes.Font, out string color))
                fontColor = Color.FromHex(color);

            info.Children.Add(Line(fontColor, $"Текущий параграф: {id}"));

            if (Path.Count > 1)
                info.Children.Add(Line(fontColor, $"Путь: {String.Join(" -> ", Path)}"));

            if (Triggers.Count > 0)
                info.Children.Add(Line(fontColor, $"Триггеры: {String.Join(", ", Triggers)}"));

            info.Children.Add(Splitter.Line(new Thickness(0, 15), Color.LightGray));

            Game.Data.MethodFromBook("Character.Debug", out string debug);

            if (!String.IsNullOrEmpty(debug))
            {
                List<string> debugLines = debug.Split('\n').Where(x => !String.IsNullOrEmpty(x)).ToList();

                if (debugLines.Count > 3)
                    info.Children.Add(GridParams(debugLines, fontColor));
                else
                    info.Children.Add(Line(fontColor, debug));
            }

            List<string> healings = Game.Healing.Debug();

            if (healings.Count > 0)
            {
                info.Children.Add(Splitter.Line(new Thickness(0, 15), Color.LightGray));
                info.Children.Add(Line(fontColor, "Снаряжение:"));

                for (int i = 0; i < healings.Count; i++)
                    info.Children.Add(Line(fontColor, $"{i + 1}. {healings[i]}"));
            }

            return info;
        }

        private static View GridParams(List<string> debugLines, Color fontColor)
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = Constants.DEBUG_GRIDROW_HEIGHT },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
                Padding = 0,
                RowSpacing = 0,
            };

            int currentRow = 0;

            for (int i = 0; i < debugLines.Count; i++)
            {
                int column = 1 - ((i + 1) % 2);
                grid.Children.Add(Line(fontColor, debugLines[i]), column, currentRow);

                if ((i > 0) && (column == 1))
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = Constants.DEBUG_GRIDROW_HEIGHT });
                    currentRow += 1;
                }
            }

            return grid;
        }
    }
}

using Seeker.Gamebook;
using System;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Splitter
    {
        public static void Add(Description gamebook, ref string lastMarker, ref StackLayout options)
        {
            if (List.Sort(Constants.SortBy.Setting) && (lastMarker != gamebook.Setting))
                Add(gamebook.Setting, ref options, ref lastMarker);

            if (List.Sort(Constants.SortBy.Author))
                Add(gamebook.AuthorsIndex()[0].ToString(), ref options, ref lastMarker);

            if (List.Sort(Constants.SortBy.Title))
                Add(gamebook.Title[0].ToString(), ref options, ref lastMarker);

            if (List.Sort(Constants.SortBy.Time))
                Add(gamebook.PlaythroughTime, ref options, ref lastMarker);

            if (List.Sort(Constants.SortBy.Year))
                Add(Years(gamebook.Year), ref options, ref lastMarker);
        }

        private static void Add(string marker, ref StackLayout options, ref string lastMarker)
        {
            if (lastMarker == marker)
                return;

            options.Children.Add(Sort(marker, lastMarker));
            lastMarker = marker;
        }

        private static Label Sort(string setting, string lastMarker) => new Label
        {
            Text = $"― {setting} ―",
            HorizontalTextAlignment = TextAlignment.Center,
            FontSize = Interface.Font(NamedSize.Large),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, (String.IsNullOrEmpty(lastMarker) ? 5 : 20), 0, 20),
        };

        public static View Line(Thickness? thickness, Color? color) => new BoxView
        {
            HeightRequest = 1,
            WidthRequest = 10,
            Color = color ?? Color.Black,
            Margin = thickness ?? new Thickness(0),
        };

        private static string Years(int year)
        {
            char decade = year.ToString()[2];
            string centry = year < 2000 ? String.Empty : "20";
            return $"{centry}{decade}0-е";
        }
    }
}

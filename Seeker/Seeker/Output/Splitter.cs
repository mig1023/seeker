using Seeker.Gamebook;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Splitter
    {
        public static void Add(Description gamebook, ref string lastMarker, ref StackLayout options)
        {
            if (List.Sort(Constants.SortBy.Setting) && (lastMarker != gamebook.Setting))
                Add(gamebook.Setting, ref lastMarker, gamebook.Setting, ref options);

            if (List.Sort(Constants.SortBy.Author))
                Add(gamebook.AuthorsIndex()[0].ToString(), ref options, ref lastMarker);

            if (List.Sort(Constants.SortBy.Title))
                Add(gamebook.Title[0].ToString(), ref options, ref lastMarker);

            if (List.Sort(Constants.SortBy.Time))
                Add(gamebook.PlaythroughTime, ref options, ref lastMarker);
        }

        private static void Add(string marker, ref StackLayout options, ref string lastMarker)
        {
            if (lastMarker != marker)
                Add(marker, ref lastMarker, marker, ref options);
        }

        private static void Add(string splitter, ref string lastMarker, string marker, ref StackLayout options)
        {
            options.Children.Add(Sort(splitter));
            lastMarker = marker;
        }

        private static Label Sort(string setting) => new Label
        {
            Text = $"― {setting} ―",
            HorizontalTextAlignment = TextAlignment.Center,
            FontSize = Interface.Font(NamedSize.Large),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20),
        };

        public static View Line(Thickness? thickness, Color? color) => new BoxView
        {
            HeightRequest = 1,
            WidthRequest = 10,
            Color = color ?? Color.Black,
            Margin = thickness ?? new Thickness(0),
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using static Seeker.Game.Data;

namespace Seeker.Output
{
    class StatusBar
    {
        private static int Clear(string line) =>
            line.Replace("CROSSEDOUT|", String.Empty).Length;

        public static List<Label> Main(List<string> statusLines)
        {
            List<Label> statusLabels = new List<Label>();

            string textColor = Game.Data.Constants.GetColor(ColorTypes.StatusFont);

            foreach (string status in statusLines)
            {
                Label label = new Label
                {
                    Text = status + Convert.ToChar(160),
                    FontSize = Constants.STATUSBAR_FONT,
                    TextColor = (String.IsNullOrEmpty(textColor) ? Color.White : Color.FromHex(textColor)),
                    BackgroundColor = Color.FromHex(Game.Data.Constants.GetColor(ColorTypes.StatusBar)),

                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                statusLabels.Add(label);
            }

            return statusLabels;
        }

        public static List<VerticalText> Additional(List<string> statusLines)
        {
            List<VerticalText> statusLabels = new List<VerticalText>();

            bool whiteColor = !String.IsNullOrEmpty(Game.Data.Constants.GetColor(ColorTypes.AdditionalFont));
            bool equalParts = Game.Data.Constants.ShowAdditionalStatusesEqualParts();

            double heightPart = statusLines.Count == 0 ? 1 :
                (int)Application.Current.MainPage.Height / statusLines.Sum(x => Clear(x));

            foreach (string status in statusLines)
            {
                VerticalText text = new VerticalText
                {
                    Value = status,
                    WhiteColor = whiteColor,
                };

                if (!equalParts)
                    text.HeightRequest = Clear(status) * heightPart;

                statusLabels.Add(text);
            }

            return statusLabels;
        }
    }
}

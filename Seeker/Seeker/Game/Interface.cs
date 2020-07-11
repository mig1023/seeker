using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Interface
    {
        public static List<Label> StatusBar(List<string> statusLines)
        {
            List<Label> statusLabels = new List<Label>();

            foreach (string status in statusLines)
            {
                Label label = new Label();

                label.Text = status;
                label.FontSize = 12;
                label.TextColor = Color.White;
                label.BackgroundColor = Color.FromHex(Game.Data.Constants.GetStatusBarColor());

                label.HorizontalTextAlignment = TextAlignment.Center;
                label.VerticalTextAlignment = TextAlignment.Center;

                label.HorizontalOptions = LayoutOptions.FillAndExpand;
                label.VerticalOptions = LayoutOptions.FillAndExpand;

                statusLabels.Add(label);
            }

            return statusLabels;
        }
    }
}

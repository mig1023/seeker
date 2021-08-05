using System;
using Xamarin.Forms;

namespace Seeker.Output
{
    public class ExtendedLabel : Label
    {
        public static BindableProperty JustifyTextProperty =
            BindableProperty.Create(nameof(JustifyText), typeof(Boolean), typeof(ExtendedLabel), false, BindingMode.OneWay);

        public bool JustifyText
        {
            get => (Boolean)GetValue(JustifyTextProperty);
            set => SetValue(JustifyTextProperty, value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Output
{
    public class VerticalText : View
    {
        public static BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value), typeof(string), typeof(string), null, BindingMode.TwoWay, null, (bindable, oldValue, newValue) => { }
        );

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}

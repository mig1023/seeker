using Xamarin.Forms;

namespace Seeker.Output
{
    public class VerticalText : View
    {
        public static BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(string),
            null, BindingMode.TwoWay, null, (bindable, oldValue, newValue) => { });

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static BindableProperty ColorProperty = BindableProperty.Create(nameof(WhiteColor), typeof(bool), typeof(bool),
            null, BindingMode.TwoWay, null, (bindable, oldValue, newValue) => { });

        public static BindableProperty RotateSide = BindableProperty.Create(nameof(LeftRotate), typeof(bool), typeof(bool),
            null, BindingMode.TwoWay, null, (bindable, oldValue, newValue) => { });

        public bool WhiteColor
        {
            get => (bool)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public bool LeftRotate
        {
            get => (bool)GetValue(RotateSide);
            set => SetValue(RotateSide, value);
        }
    }
}

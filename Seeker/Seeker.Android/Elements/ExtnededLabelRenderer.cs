using Android.Content;
using Seeker.Output;
using Seeker.Game;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(Seeker.Droid.ExtnededLabelRenderer))]
namespace Seeker.Droid
{
    public class ExtnededLabelRenderer : LabelRenderer
    {
        public ExtnededLabelRenderer(Context context) : base(context) => Services.DoNothing();

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if ((Element != null) && (Element as ExtendedLabel).JustifyText && (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O))
                Control.JustificationMode = Android.Text.JustificationMode.InterWord;
        }
    }
}
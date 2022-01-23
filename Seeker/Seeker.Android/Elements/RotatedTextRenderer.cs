using Android.Content;
using Android.Graphics;
using Android.Text;
using Seeker.Droid;
using Seeker.Output;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VerticalText), typeof(RotatedTextRenderer))]
namespace Seeker.Droid
{
    public class RotatedTextRenderer : ViewRenderer
    {
        private Context context;

        public RotatedTextRenderer(Context c) : base(c) => context = c;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is VerticalText)
            {
                string title = ((VerticalText)e.NewElement).Value;
                bool color = ((VerticalText)e.NewElement).WhiteColor;
                bool left = ((VerticalText)e.NewElement).LeftRotate;
                SetNativeControl(new RotatedTextView(context, title, color, left));
            }
        }
    }

    public class RotatedTextView : Android.Views.View
    {
        int textSize = 22;
        string text = String.Empty;
        bool whiteText = false;
        bool leftRotate = false;

        TextPaint textPaint = null;

        public RotatedTextView(Context c, string title, bool white, bool left) : base(c)
        {
            text = title;
            whiteText = white;
            leftRotate = left;

            InitLabelView();
        }

        private void InitLabelView() => this.textPaint = new TextPaint
        {
            AntiAlias = true,
            TextAlign = Paint.Align.Center,
            TextSize = textSize,
            Color = (this.whiteText ? Android.Graphics.Color.White : Android.Graphics.Color.Black),
        };

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (!string.IsNullOrEmpty(this.text))
            {
                float x = (Width / 2) - textSize / 3;
                float y = (Height / 2);

                if (leftRotate)
                {
                    canvas.Rotate(-90);
                    canvas.DrawText(this.text, -y, x + 2, this.textPaint);
                }
                else
                {
                    canvas.Rotate(90);
                    canvas.DrawText(this.text, y, -x, this.textPaint);
                }
            }
        }
    }
}
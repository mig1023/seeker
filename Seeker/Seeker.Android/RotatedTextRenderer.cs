using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Seeker;
using Seeker.Droid;
using Seeker.Output;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VerticalText), typeof(RotatedTextRenderer))]
namespace Seeker.Droid
{
    public class RotatedTextRenderer : ViewRenderer
    {
        private Context _context;

        public RotatedTextRenderer(Context c) : base(c)
        {
            _context = c;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is VerticalText)
            {
                string title = ((VerticalText)e.NewElement).Value;
                bool color = ((VerticalText)e.NewElement).WhiteColor;
                SetNativeControl(new RotatedTextView(_context, title, color));
            }
        }
    }

    public class RotatedTextView : Android.Views.View
    {
        private int textSize = 22;
        private string _text;
        private bool _white;
        private TextPaint _textPaint;

        public RotatedTextView(Context c, string title, bool white) : base(c)
        {
            _text = title;
            _white = white;
            initLabelView();
        }

        private void initLabelView()
        {
            this._textPaint = new TextPaint();
            this._textPaint.AntiAlias = true;
            this._textPaint.TextAlign = Paint.Align.Center;
            this._textPaint.TextSize = textSize;
            this._textPaint.Color = (this._white ? Android.Graphics.Color.White : Android.Graphics.Color.Black);
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (!string.IsNullOrEmpty(this._text))
            {
                float x = (Width / 2) - textSize / 3;
                float y = (Height / 2);

                canvas.Rotate(90);
                canvas.DrawText(this._text, y, -x, this._textPaint);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Android.Content.Res;
using System.IO;

[assembly: Dependency(typeof(Seeker.Droid.GetAssets))]
namespace Seeker.Droid
{
    class GetAssets : Other.IAssets
    {
        public string GetFromAssets(string name)
        {
            AssetManager assets = Android.App.Application.Context.Assets;

            string content;

            using (StreamReader sr = new StreamReader(assets.Open(name)))
            {
                content = sr.ReadToEnd();
            }

            return content;
        }
    }
}
using System;
using Xamarin.Forms;
using Android.Content.Res;
using System.IO;

[assembly: Dependency(typeof(Seeker.Droid.GetAssets))]
namespace Seeker.Droid
{
    class GetAssets : Abstract.IAssets
    {
        public string GetFromAssets(string name)
        {
            AssetManager assets = Android.App.Application.Context.Assets;

            string content = String.Empty;

            using (StreamReader sr = new StreamReader(assets.Open(name)))
                content = sr.ReadToEnd();

            return content;
        }
    }
}
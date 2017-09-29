using System.Reflection;
using Android.App;
using Android.OS;
using Mobile.Svg;
using MvvmCross.Droid.Views;

namespace Mobile.Android.Views
{
    [Activity(Label = "Base")]
    public class BaseView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
			// Setup SVG Lib
			XamSvg.Setup.InitSvgLib();
			// Config XamSvg which assembly to search for svg when "res:" is used
			XamSvg.Shared.Config.ResourceAssembly = typeof(SVGImages).GetTypeInfo().Assembly;
            base.OnCreate(bundle);
        }
    }
}

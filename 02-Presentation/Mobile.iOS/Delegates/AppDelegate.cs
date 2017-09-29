using Foundation;
using UIKit;
using Mobile.Svg;
using System.Reflection;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Platform;
using MvvmCross.Core.ViewModels;

namespace Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate
    {
		#region Overrides

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            InitializeApp();
            return true;
        }

		#endregion Overrides

		#region Private Methods

        /// <summary>
        /// Initializes the app.
        /// </summary>
        private void InitializeApp()
        {
			// Initialize SVG
			XamSvg.Setup.InitSvgLib();
			//Tells XamSvg in which assembly to search for svg when "res:" is used
			XamSvg.Shared.Config.ResourceAssembly = typeof(SVGImages).GetTypeInfo().Assembly;

			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			// MVX Setup
			var presenter = new MvxIosViewPresenter(this, Window);
			var setup = new Mobile.iOS.Mvx.Setup(this, presenter);
			setup.Initialize();
			var startup = MvvmCross.Platform.Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			Window.MakeKeyAndVisible();
        }

		#endregion Private Methods
	}
}


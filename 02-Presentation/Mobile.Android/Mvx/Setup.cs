using Android.Content;
using Mobile.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform.Platform;
// NOTE: The namespace _must_ be the root namespace, otherwise MVX is not initialized properly.
namespace Mobile.Android
{
    public class Setup : MvxAndroidSetup
    {
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}
    }
}

using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Mobile.Android.Views
{
    [Activity(Label = "Home")]
    public class HomeView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
			base.OnCreate(bundle);
        }

		protected override void OnViewModelSet()
		{
			SetContentView(Resource.Layout.HomeLayout);
		}
    }
}

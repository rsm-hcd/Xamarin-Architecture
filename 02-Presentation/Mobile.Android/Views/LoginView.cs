using Acr.UserDialogs;
using Android.App;
using Android.OS;
using Mobile.ViewModels.ViewModels;

namespace Mobile.Android.Views
{
    [Activity(Label = "Login", MainLauncher = true)]
    public partial class LoginView : BaseView
    {
        protected override void OnCreate(Bundle bundle)
        {
			// Create your application here
			UserDialogs.Init(this);
            base.OnCreate(bundle);
        }

		protected override void OnViewModelSet()
		{
            var vm = (LoginViewModel)ViewModel;
			vm.Initialize();
			SetContentView(Resource.Layout.LoginLayout);
		}

        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            ConfigureScreenControls();
        }
    }
}

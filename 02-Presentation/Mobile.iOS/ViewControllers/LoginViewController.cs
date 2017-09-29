using System;
using Mobile.iOS.ViewControllers.Base;
using Mobile.ViewModels.ViewModels;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace Mobile.iOS.ViewControllers
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class LoginViewController : BaseScrollViewController<LoginViewModel>
    {
        public LoginViewController()
        {
        }

        private LoginViewModel VM {
            get
            {
                return (LoginViewModel)this.ViewModel;
            }
        }

		#region Overrides

		/// <summary>
		/// Hide the status bar
		/// </summary>
		/// <returns><c>true</c>, if status bar hidden was preferred, <c>false</c> otherwise.</returns>
		public override bool PrefersStatusBarHidden()
		{
			return true;
		}

		protected override string ScreenName => "Login";

		/// <summary>
		/// Creates view and applies layout contraints.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeViews();
            ConfigureLayoutConstraints();
            VM.Initialize();
		}

		/// <summary>
		/// Runs prior to the view appearing. This is where we perform logic that relies on subviews having sizing data.
		/// </summary>
		/// <param name="animated"></param>
		public override void ViewWillAppear(bool animated)
		{
			NavigationController?.SetNavigationBarHidden(true, false);
			base.ViewWillAppear(animated);
		}

		#endregion Overrides
    }
}

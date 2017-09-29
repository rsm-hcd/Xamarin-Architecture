using Mobile.iOS.ViewControllers.Base;
using Mobile.ViewModels.ViewModels;

namespace Mobile.iOS.ViewControllers
{
    public partial class HomeViewController : BaseViewController<HomeViewModel>
    {
        private HomeViewModel _viewModel;

		public HomeViewModel VM
		{
			get { return _viewModel ?? (_viewModel = ViewModel as HomeViewModel); }
		}
        public HomeViewController()
        {
        }

		#region Overrides

		/// <summary>
		/// Hide the status bar
		/// </summary>
		/// <returns><c>true</c>, if status bar hidden was preferred, <c>false</c> otherwise.</returns>
		public override bool PrefersStatusBarHidden()
		{
            return false;
		}

		protected override string ScreenName => "Home";

		/// <summary>
		/// Creates view and applies layout contraints.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeViews();
            ConfigureLayoutConstraints();
		}

		/// <summary>
		/// Runs prior to the view appearing. This is where we perform logic that relies on subviews having sizing data.
		/// </summary>
		/// <param name="animated"></param>
		public override void ViewWillAppear(bool animated)
		{
			NavigationController?.SetNavigationBarHidden(false, false);
			base.ViewWillAppear(animated);
		}

		#endregion Overrides
    }
}

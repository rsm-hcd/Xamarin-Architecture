using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Mobile.iOS.Views.Cells
{
    public partial class UserCell : MvxTableViewCell
    {
		public UserCell() : base()
		{
			InitializeViews();
			InitializeBindings();
            ConfigureLayoutConstraints();
		}

		public UserCell(IntPtr handle) : base(handle)
        {
			InitializeViews();
			InitializeBindings();
			ConfigureLayoutConstraints();
		}

		[Export("requiresConstraintBasedLayout")]
		bool UseNewLayout()
		{
			return true;
		}
    }
}

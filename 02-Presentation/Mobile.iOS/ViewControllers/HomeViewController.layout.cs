using UIKit;

namespace Mobile.iOS.ViewControllers
{
    public partial class HomeViewController
    {
		#region Private Methods

		/// <summary> 
		/// Lays out the subviews inside the presenter's super view. 
		/// </summary> 
		void ConfigureLayoutConstraints()
		{
			View.AddConstraints(new[] {
				NSLayoutConstraint.Create(UsersTable, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(UsersTable, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0),
				NSLayoutConstraint.Create(UsersTable, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0),
				NSLayoutConstraint.Create(UsersTable, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0)
		    });
		}

		#endregion Private Methods
	}
}

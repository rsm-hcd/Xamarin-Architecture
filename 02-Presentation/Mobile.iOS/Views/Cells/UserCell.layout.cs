using System;
using Mobile.ViewModels;
using UIKit;

namespace Mobile.iOS.Views.Cells
{
    public partial class UserCell
    {
		#region Private Methods

		/// <summary> 
		/// Lays out the subviews inside the presenter's super view. 
		/// </summary> 
		void ConfigureLayoutConstraints()
		{
			ContentView.AddConstraints(new[] {
                NSLayoutConstraint.Create(NameLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Top, 1, .25f * AppSizes.MARGIN),
				NSLayoutConstraint.Create(NameLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, AppSizes.MARGIN),
				NSLayoutConstraint.Create(NameLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -AppSizes.MARGIN),
				NSLayoutConstraint.Create(NameLabel, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AppSizes.FIELD_HEIGHT)
			});
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, NameLabel, NSLayoutAttribute.Bottom, 1, 0),
				NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, AppSizes.MARGIN),
				NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -AppSizes.MARGIN),
                NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AppSizes.FIELD_HEIGHT + (.25f * AppSizes.MARGIN))
			});
		}

		#endregion Private Methods
	}
}

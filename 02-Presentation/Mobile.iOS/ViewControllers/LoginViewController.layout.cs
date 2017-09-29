using System;
using Mobile.ViewModels;
using UIKit;

namespace Mobile.iOS.ViewControllers
{
    public partial class LoginViewController
    {
		#region Private Methods

		/// <summary> 
		/// Lays out the subviews inside the presenter's super view. 
		/// </summary> 
		void ConfigureLayoutConstraints()
		{
			// Logo 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Top, 1, 2f * AppSizes.MARGIN),
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AvatarImage.FillHeight),
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AvatarImage.FillWidth)
		    });
			// Username 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, AvatarImage, NSLayoutAttribute.Bottom, 1, 2f * AppSizes.MARGIN),
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AppSizes.FIELD_HEIGHT),
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, AppSizes.MARGIN),
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -AppSizes.MARGIN)
	        });
			// Password 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, UsernameField, NSLayoutAttribute.Bottom, 1, AppSizes.MARGIN),
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AppSizes.FIELD_HEIGHT),
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, AppSizes.MARGIN),
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -AppSizes.MARGIN)
	        });
			// Login Button 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, PasswordField, NSLayoutAttribute.Bottom, 1, AppSizes.MARGIN * 2f),
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AppSizes.FIELD_HEIGHT * 1.5f),
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, AppSizes.MARGIN),
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -AppSizes.MARGIN)
	        });
			// Message Label
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, SubmitButton, NSLayoutAttribute.Bottom, 1, AppSizes.MARGIN * 2f),
				NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual, null, NSLayoutAttribute.NoAttribute, 1, AppSizes.FIELD_HEIGHT),
				NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, AppSizes.MARGIN),
				NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -AppSizes.MARGIN)
			});
		}

		#endregion Private Methods
	}
}

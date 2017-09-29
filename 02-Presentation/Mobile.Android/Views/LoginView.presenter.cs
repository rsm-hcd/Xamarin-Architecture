using System;
using Android.Widget;
using Mobile.ViewModels.ViewModels;
using MvvmCross.Plugins.Color.Droid;

namespace Mobile.Android.Views
{
    public partial class LoginView
    {
        #region Member Variables

		EditText _passwordField;
        EditText _usernameField;

		#endregion Member Variables

		#region Private Methods

        void ConfigureScreenControls()
        {
            var vm = (LoginViewModel)ViewModel;
            GetFields();
            _passwordField.SetHintTextColor(vm.GrayColor.ToAndroidColor());
            _usernameField.SetHintTextColor(vm.GrayColor.ToAndroidColor());
        }

        void GetFields()
        {
            _passwordField = FindViewById<EditText>(Resource.Id.passwordField);
            _usernameField = FindViewById<EditText>(Resource.Id.usernameField);
        }

		#endregion Private Methods
	}
}

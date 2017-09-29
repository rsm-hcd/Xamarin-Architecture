using Mobile.Services.Realm;
using Shouldly;
using NUnit.Framework;
using AutoMapper;
using Mobile.ViewModels.ViewModels;
using MvvmCross.Core.Navigation;
using Acr.UserDialogs;
using Mobile.ViewModels.Tests.Shared.Mocks;

namespace Mobile.ViewModels.Tests
{
    [TestFixture ()]
	public class LoginViewModelTest : ViewModelTestBase
	{
		#region Member Variables

		MvxNavigationService _navigationService;
        LoginViewModel _sut;
        UserService _userDatabaseService;
        IUserDialogs _userDialogs;

		#endregion Member Variables

		#region Setup

        [SetUp]
		public void LoginViewModelTestSetup()
		{
            _navigationService = new MvxNavigationService();
            _userDatabaseService = new UserService(Mapper.Instance);
            _userDialogs = new UserDialogsMock();
            _sut = new LoginViewModel(_navigationService, _userDatabaseService, _userDialogs);
		}

		#endregion Setup

		#region Login User

		[Test()]
		public void LoginUser_User_Does_Not_Exist_Has_Errors()
		{
            // Arrange
            base.Setup();
			_sut.Username = "badperson";
			_sut.Password = "notreal";
            // Act
            _sut.LoginCommand.Execute();
            // Assert
            _sut.HasErrors.ShouldBeTrue();
		}

		[Test()]
		public void LoginUser_User_Does_Exists()
		{
			// Arrange
			base.Setup();
			_sut.Username = "test@test.com";
			_sut.Password = "passw0rd!";
            // Act
			_sut.LoginCommand.Execute();
			// Assert
			_sut.HasErrors.ShouldBeFalse();
		}

		#endregion Login User
	}
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Mobile.Core.Interfaces.Services.Database;
using Mobile.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Mobile.ViewModels.ViewModels
{
    public class HomeViewModel : BaseViewModel, IMvxViewModel
    {
        #region Member Variables

        readonly IUserDialogs _userDialog;
        List<User> _users;
        readonly IUserService _userService;

		#endregion Member Variables

		#region Constructors

		public HomeViewModel(IUserService userService, IUserDialogs userDialog) : base()
        {
			_userDialog = userDialog;
			_userService = userService;
        }

		#endregion Constructors

		#region Overrides
		
        public Task Initialize()
		{
            // Get all users
            LoadData();
			return base.Initialize();
		}

        #endregion Overrides

        #region Public Properties

		public List<User> Users
		{
			get
			{
				return _users;
			}
			set
			{
				SetProperty(ref _users, value);
			}
		}

        public string TitleText => AppText.USERS_LIST;

		#endregion Public Properties

		#region Public Methods

		#endregion Public Methods

		#region Private Methods

		async Task LoadData()
		{
			_userDialog.ShowLoading("Loading Users...", MaskType.Black);
			await Task.Factory.StartNew(GetUsers);
			_userDialog.HideLoading();
		}

        void GetUsers()
        {
			// Get all the users we just saved to ensure they are persisited in the database. 
			Users = _userService.GetAll().ToList();
        }

        #endregion Private Methods
    }
}
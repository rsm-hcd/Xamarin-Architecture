using AutoMapper;
using Mobile.Services.Realm;
using MvvmCross.Test.Core;
using Realms;

namespace Mobile.ViewModels.Tests
{
	public class ViewModelTestBase : MvxIoCSupportingTest
	{
        #region Constants

        // TODO: Change this to a name that is representative of your app.
        const string DATABASE_NAME = "app.realm";

		#endregion Constants
		
        #region Protected Variables

		protected readonly Realm realmContext;

		#endregion Protected Variables

		#region Constructor

		public ViewModelTestBase()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<User, Core.Models.User>();
				cfg.CreateMap<User, Core.Models.User>().ReverseMap();
			});
			realmContext = Realm.GetInstance(new RealmConfiguration(DATABASE_NAME) { 
				SchemaVersion = 1
			});
		}

		#endregion Constructor
	}
}

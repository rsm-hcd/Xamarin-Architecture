using System.Reflection;
using Acr.UserDialogs;
using AutoMapper;
using Mobile.Services.Realm;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Splat;

namespace Mobile.ViewModels
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
            // Initialize mappings
            InitializeMappings();
			// Register AutoMapper
			Mvx.RegisterSingleton<IMapper>(() => Mapper.Instance);

            // Register Realm Services
            var assembly = typeof(Mobile.Services.Realm.Services.RepositoryService).GetTypeInfo().Assembly;
			assembly.CreatableTypes()
				.InNamespace("Mobile.Services.Realm")
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsSingleton();

			// Register Dialogs
			Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
			Locator.CurrentMutable.RegisterConstant(new Logger(), typeof(ILogger));

			// Register  
			RegisterAppStart<ViewModels.LoginViewModel>();
		}

		private void InitializeMappings()
		{
            Mapper.Initialize(cfg => {
                cfg.CreateMap<User, Core.Models.User>();
				cfg.CreateMap<User, Core.Models.User>().ReverseMap(); 
            });
		}
	}
}

using System.Collections.Generic;
using Mobile.Core.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.UI;

namespace Mobile.ViewModels.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        public BaseViewModel()
        {
            Errors = new List<Error>();    
        }

		#region Public Properties

        public List<Error> Errors { get; set; }

		public bool HasErrors
		{
			get
			{
				return Errors?.Count > 0;
			}
		}

        public MvxColor GrayColor => AppColors.Gray;

        public MvxColor PrimaryColor => AppColors.Primary;

        public MvxColor SecondaryColor => AppColors.Secondary;

        public MvxColor NeutralColor => AppColors.White;

		#endregion Public Properties
    }
}

using Mobile.iOS.Extensions;
using Mobile.iOS.TableSources;
using Mobile.iOS.Utilities;
using Mobile.ViewModels.ViewModels;
using MvvmCross.Binding.BindingContext;
using UIKit;
using XamSvg;

namespace Mobile.iOS.ViewControllers
{
    public partial class HomeViewController
    {
        #region Properties

        #region Views

		UsersTableSource TableSource { get; set; }
        UITableView UsersTable { get; set; }
		
        #endregion Views

        #endregion Properties

        /// <summary>
        /// Configures the views for the settings screen.
        /// </summary>
        void InitializeViews()
        {
            View.BackgroundColor = AppColors.WHITE.ToUIColor();

            UsersTable = new UITableView
            {
                RowHeight = 70f,
                SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            TableSource = new UsersTableSource(VM.Users, UsersTable);
            UsersTable.Source = TableSource;
            // Add the subviews to the content view.
            View.AddSubviews(UsersTable);
            SetupMvxBindings();
            UsersTable.ReloadData();
        }

        void SetupMvxBindings()
        {
            var bindings = this.CreateBindingSet<HomeViewController, HomeViewModel>();
            bindings.Bind(TableSource)
                    .To(vm => vm.Users);
			bindings.Bind(this)
					.For(v => v.Title)
					.To(vm => vm.TitleText)
					.OneTime();
            bindings.Apply();
        }
    }
}

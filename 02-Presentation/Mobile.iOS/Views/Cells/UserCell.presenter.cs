using Mobile.Core.Models;
using Mobile.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Plugins.Color.iOS;
using UIKit;

namespace Mobile.iOS.Views.Cells
{
    public partial class UserCell
    {
        #region Views

        UILabel EmailLabel { get; set; }
        UILabel NameLabel { get; set; }

        #endregion Views

        #region Private Methods

        void InitializeViews()
        {
            EmailLabel = new UILabel
            {
				TextColor = AppColors.Secondary.ToNativeColor(),
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            NameLabel = new UILabel
            {
                TextColor = AppColors.Primary.ToNativeColor(),
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            ContentView.AddSubviews(NameLabel, EmailLabel);
        }


        void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                var bindings = this.CreateBindingSet<UserCell, User>();
                bindings.Bind(EmailLabel)
                        .For(l => l.Text)
                        .To(vm => vm.Email);
                bindings.Bind(NameLabel)
                        .For(l => l.Text)
                        .To(vm => vm.FullName);          
                bindings.Apply();
            });
        }

        #endregion Private Methods
    }
}

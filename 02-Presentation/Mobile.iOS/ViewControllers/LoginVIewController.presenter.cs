using Mobile.ViewModels;
using Mobile.ViewModels.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS;
using MvvmCross.Plugins.Color.iOS;
using UIKit;
using XamSvg;

namespace Mobile.iOS.ViewControllers
{
    public partial class LoginViewController
    {
        #region Constants

        const float TEXT_SIZE = 16f;

        #endregion Constants

        #region Properties

        #region Views

        UISvgImageView AvatarImage { get; set; }
        UILabel MessageLabel { get; set; }
        UITextField PasswordField { get; set; }
        UIButton SubmitButton { get; set; }
        UITextField UsernameField { get; set; }

        #endregion Views

        #endregion Properties

        /// <summary>
        /// Configures the views for the settings screen.
        /// </summary>
        void InitializeViews()
        {
            // Avatar Image
            AvatarImage = new UISvgImageView("res:avatar", 80f, 80f)
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            // Username field
            UsernameField = new UITextField
            {
                AutocapitalizationType = UITextAutocapitalizationType.None,
                AutocorrectionType = UITextAutocorrectionType.No,
				BorderStyle = UITextBorderStyle.RoundedRect,
                Font = UIFont.SystemFontOfSize(TEXT_SIZE, UIFontWeight.Regular),
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            UsernameField.Layer.BorderWidth = 1f;
            UsernameField.Layer.BorderColor = VM.SecondaryColor.ToNativeColor().CGColor;
            // Password field
            PasswordField = new UITextField
            {
				BorderStyle = UITextBorderStyle.RoundedRect,
                Font = UIFont.SystemFontOfSize(TEXT_SIZE, UIFontWeight.Regular),
                SecureTextEntry = true,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            PasswordField.Layer.BorderWidth = 1f;
            PasswordField.Layer.BorderColor = VM.SecondaryColor.ToNativeColor().CGColor;
            // Submit Button
            SubmitButton = new UIButton(UIButtonType.RoundedRect)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            SubmitButton.SetTitleColor(VM.NeutralColor.ToNativeColor(), UIControlState.Normal);
            // Message Label
            MessageLabel = new UILabel
            {
                Font = UIFont.SystemFontOfSize(TEXT_SIZE, UIFontWeight.Regular),
				Lines = 0,
				LineBreakMode = UILineBreakMode.WordWrap,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            // Add the subviews to the content view.
            ContentView.AddSubviews(AvatarImage, UsernameField, PasswordField, SubmitButton, MessageLabel);
            SetupMvxBindings();
        }

        void SetupMvxBindings()
        {
            var bindings = this.CreateBindingSet<LoginViewController, LoginViewModel>();
            bindings.Bind(ContentView)
                    .For(t => t.BackgroundColor)
                    .To(vm => vm.NeutralColor)
                    .OneTime()
                    .WithConversion(AppText.NATIVE_COLOR);
			bindings.Bind(ContentView.Superview)
					.For(t => t.BackgroundColor)
					.To(vm => vm.NeutralColor)
					.OneTime()
					.WithConversion(AppText.NATIVE_COLOR);
            bindings.Bind(UsernameField)
                    .For(t => t.Text)
                    .To(vm => vm.Username);
            bindings.Bind(UsernameField)
                    .For(t => t.Placeholder)
                    .To(vm => vm.UsernamePlaceholderText)
                    .OneTime();
			bindings.Bind(UsernameField)
					.For(t => t.TextColor)
					.To(vm => vm.PrimaryColor)
					.OneTime()
					.WithConversion(AppText.NATIVE_COLOR);
            bindings.Bind(PasswordField)
                    .For(t => t.Text)
                    .To(vm => vm.Password);
            bindings.Bind(PasswordField)
                    .For(t => t.Placeholder)
                    .To(vm => vm.PasswordPlaceholderText)
                    .OneTime();
			bindings.Bind(PasswordField)
					.For(t => t.TextColor)
					.To(vm => vm.PrimaryColor)
					.OneTime()
					.WithConversion(AppText.NATIVE_COLOR);
            bindings.Bind(SubmitButton)
                    .For(b => b.BindTitle())
                    .To(vm => vm.LoginButtonText)
                    .OneTime();
            bindings.Bind(SubmitButton)
                    .For(b => b.BindTouchUpInside())
                    .To(vm => vm.LoginCommand);
            bindings.Bind(SubmitButton)
                    .For(b => b.BackgroundColor)
                    .To(vm => vm.SecondaryColor)
                    .OneTime()
                    .WithConversion(AppText.NATIVE_COLOR);
            bindings.Bind(MessageLabel)
                    .For(l => l.Text)
                    .To(vm => vm.Message);
            bindings.Bind(MessageLabel)
                    .For(t => t.TextColor)
                    .To(vm => vm.PrimaryColor)
                    .OneTime()
                    .WithConversion(AppText.NATIVE_COLOR);
            bindings.Apply();
        }
    }
}

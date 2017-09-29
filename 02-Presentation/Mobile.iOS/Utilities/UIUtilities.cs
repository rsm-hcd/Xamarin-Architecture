using CoreGraphics;
using Foundation;
using LocalAuthentication;
using Mobile.iOS.Extensions;
using Mobile.iOS.Utilities;
using System;
using System.Linq;
using UIKit;

namespace SyncedCare.Mobile.Presentation.iOS.Helpers
{
    public static class UIUtilities
    {
        #region Properties

        #endregion Properties

        #region Methods

        /// <summary>
        /// Pushes the user to the supplied view controller.
        /// </summary>
        /// <typeparam name="T">The type of the view controller to take the user to.</typeparam>
        /// <param name="storyboard">The storyboard that contains the view controller</param>
        /// <param name="navigationController">An instance of the navigation controller.</param>
        public static void PushToScreen<T>(UIStoryboard storyboard, UINavigationController navigationController)
		{
			var typeName = typeof(T).ToString();
			var controllerName = typeName.Contains(".") ? typeName.Substring(typeName.LastIndexOf(".", StringComparison.CurrentCulture) + 1) : typeName;
			var viewController = storyboard.InstantiateViewController(controllerName);
			Convert.ChangeType(viewController, typeof(T));
			if (viewController != null)
			{
				navigationController.PushViewController(viewController, true);
			}
		}

        /// <summary>
        /// Generates an image of a solid color with the supplied dimenations.
        /// </summary>
        /// <param name="color">The color of the image.</param>
        /// <param name="size">The dimensions of the image.</param>
        /// <returns>A rectangular image of a solid color.</returns>
        public static UIImage CreateImageFromColor(UIColor color, CGSize size)
        {
            var rect = new CGRect(new CGPoint(0,0), size);
            UIGraphics.BeginImageContextWithOptions(size, false, 0);
            color.SetFill();
            UIGraphics.RectFill(rect);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

	     /// <summary>
	     /// Calls a phone number with the app that is configured to handle telephone requests.
	     /// </summary>
	     /// <param name="phoneNumber"></param>
	     public static void CallPhoneNumber(string phoneNumber)
	     {
			// Bail if empty
			if (string.IsNullOrEmpty(phoneNumber))
			{
				return;
			}
			// Extract digits only
			var trimmed = new string(phoneNumber.Where(c => char.IsDigit(c)).ToArray());
			// Bail if not vaild phone
			if (string.IsNullOrEmpty(trimmed) || trimmed.Length < 7)
			{
				return;
			}
			try
			{
				var url = new NSUrl("tel://" + trimmed);
				if (UIApplication.SharedApplication.CanOpenUrl(url))
				{
					UIApplication.SharedApplication.OpenUrl(url);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Unable to call phone number: {trimmed}");
				Console.Write(ex);
			}
	     }

        /// <summary>
        /// Will try to open the url in the devices default browser
        /// </summary>
        /// <param name="url">The url to open</param>
        public static void OpenUrl(string url)
        {
            var urlToOpen = new NSUrl(url);
            if (UIApplication.SharedApplication.CanOpenUrl(urlToOpen))
            {
                UIApplication.SharedApplication.OpenUrl(urlToOpen);
            }
        }

        /// <summary>
        /// Will try to open the map in Google Maps if installed. If not it falls back to apple maps.
        /// </summary>
        /// <param name="address"></param>
        public static void OpenMap(string address)
        {
            var encoded = System.Net.WebUtility.UrlEncode(address);
            var google = new NSUrl("comgooglemaps://?q=" + encoded);
            var apple = new NSUrl("http://maps.apple.com?q=" + encoded);
            if (UIApplication.SharedApplication.CanOpenUrl(new NSUrl("comgooglemaps://")))
            {
                UIApplication.SharedApplication.OpenUrl(google);
            }
            else if(UIApplication.SharedApplication.CanOpenUrl(apple))
            {
                UIApplication.SharedApplication.OpenUrl(apple);
            }
        }

		/// <summary>
		/// Adds a gesture recognizer to the view to dismiss the keyboard when editing ends.
		/// </summary>
		/// <param name="view">The view to apply the gesture recognizer to.</param>
		public static void ApplyKeyboardDismissToView(this UIView view, UIView viewToCheck)
		{
            var g = new UITapGestureRecognizer(() =>
            {
                var firstResponder = GetFirstResponder(viewToCheck);
                if (firstResponder != null)
                {
                    firstResponder.ResignFirstResponder();
                }
            })
            {
                CancelsTouchesInView = false //for iOS5
            };
            view.AddGestureRecognizer(g);
		}

		/// <summary>
		/// Gets the first responder for a view (recursive).
		/// </summary>
		/// <returns>The first responder.</returns>
		/// <param name="view">The view to search.</param>
		public static UIView GetFirstResponder(UIView view)
		{
			if (view.IsFirstResponder)
			{
				return view;
			}
			if (view.Subviews != null)
			{
				foreach (var v in view.Subviews)
				{
					var firstResponder = GetFirstResponder(v);
					if (firstResponder != null)
					{
						return firstResponder;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the next and previous subviews based on the current index.
		/// </summary>
		/// <returns>The next and previous subviews.</returns>
		/// <param name="container">The UI view containing the child subviews.</param>
		/// <param name="index">The current subview index to base next and previous off of.</param>
		public static SurroundingSubviews GetNextAndPreviousSubviews(UIView container, int index)
		{
			if (container?.Subviews?.Length > 0)
			{
				var previous = index == 0 || container.Subviews.Length == 1 ? null : container.Subviews[index - 1];
				var next = index == container.Subviews.Length - 1 ? null : container.Subviews[index + 1];
				return new SurroundingSubviews(previous, next);
			}
			return null;
		}

		/// <summary>
		/// Updates the secure data in keychain.
		/// </summary>
		/// <param name="username">The value to set the username as (blank to remove).</param>
		/// <param name="password">The value to set the username as (blank to remove).</param>
		/// <returns>If the update was successful.</returns>
		public static bool UpdateKeyChainData(string username, string password)
		{
			var secureData = KeychainHelper.GetSecureDataFromKeychain();
			if (secureData != null)
			{
				secureData.Username = username;
				secureData.Password = password;
				return KeychainHelper.StoreSecureDataInKeychain(secureData);
			}
			return false;
		}

		/// <summary>
		/// Prompts for touch id authenticaion.
		/// </summary>
		/// <param name="responseAction">Code to execute after a response is received from Touch ID.</param>
		public static LAContext PromptForTouchID(LAContextReplyHandler responseAction)
		{
			
			var context = new LAContext();
			context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Logging in with Touch ID", responseAction);
			return context;
		}

		/// <summary>
		/// Prompts user for notifications.
		/// </summary>
		public static void RequestPushNotificationSetting()
		{
			// Ask user for local notifications
			var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Sound | UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
		}

		/// <summary>
		/// Gets a text field.
		/// </summary>
		/// <returns>The text field.</returns>
		/// <param name="placeholder">Placeholder text.</param>
		/// <param name="fontSize">The font size</param>
		/// <param name="isSecure">If set to <c>true</c> the field is treated as a password entry.</param>
		public static UITextField GetTranslucentTextField(string placeholder, float fontSize, bool isSecure = false)
		{
			var placeholderAttr = new NSAttributedString(placeholder, new UIStringAttributes { Font = UIFont.SystemFontOfSize(fontSize, UIFontWeight.Regular), ForegroundColor = AppColors.WHITE.ToUIColor(.5f) });
			var leftViewForPadding = new UIView(new CGRect(0, 0, 21, 45));
			var textField = new UITextField()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				AttributedPlaceholder = placeholderAttr,
				Font = UIFont.SystemFontOfSize(fontSize, UIFontWeight.Regular),
				TextColor = AppColors.WHITE.ToUIColor(),
				BorderStyle = UITextBorderStyle.None,
				ClipsToBounds = true,
				SecureTextEntry = isSecure,
				LeftView = leftViewForPadding,
				LeftViewMode = UITextFieldViewMode.Always
			};
			textField.Layer.BorderWidth = 1f;
			textField.Layer.CornerRadius = 6f;
			textField.Layer.MasksToBounds = true;
			StyleTranslucentFieldAsUnfocused(textField);
			return textField;
		}

		/// <summary>
		/// Styles the field as unfocused.
		/// </summary>
		/// <param name="field">Field.</param>
		public static void StyleTranslucentFieldAsUnfocused(UITextField field)
		{
			UIView.Animate(.3f, () =>
			{
				field.BackgroundColor = AppColors.WHITE.ToUIColor(.06f);
				field.Layer.BorderColor = AppColors.WHITE.ToUIColor(.3f).CGColor;
			});
		}

		/// <summary>
		/// Styles the field as focused.
		/// </summary>
		/// <param name="field">Field.</param>
		public static void StyleTranslucentFieldAsFocused(UITextField field)
		{
			UIView.Animate(.3f, () =>
			{
				field.BackgroundColor = AppColors.WHITE.ToUIColor(.2f);
				field.Layer.BorderColor = AppColors.WHITE.ToUIColor().CGColor;
			});
		}

		#endregion Methods
	}
}
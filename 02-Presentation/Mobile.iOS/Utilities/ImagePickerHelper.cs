using AVFoundation;
using Foundation;
using Mobile.iOS.Delegates;
using Mobile.iOS.ViewControllers.Base;
using Photos;
using System;
using UIKit;
using Mobile.ViewModels.ViewModels;

namespace Mobile.iOS.Utilities
{
    public static class ImagePickerHelper
    {
        public static UIImagePickerController picker;
        public static Action<NSDictionary> _callback;
        public static Action _onCancel;

        /// <summary>
        /// Determines if the device supports a front facing or rear facing camera.
        /// </summary>
        public static bool CameraAvailable
        {
            get
            {
                return UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear) || UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);
            }
        }

        /// <summary>
        /// Intializes the image picker delagate to handle what occurs when a photo is picked/taken or the user cancels.
        /// </summary>
        static void Init()
        {
            picker = new UIImagePickerController();
            picker.Delegate = new ImagePickerDelegate(_callback, _onCancel);
        }

		/// <summary>
		/// Opens a camera capture view controller.
		/// </summary>
		/// <param name="parent">The parent view controller that invoked the method.</param>
		/// <param name="callback">Action to call after photo is captured.</param>
		/// <param name="onCancel">Action to call if canceling from camera.</param>
		/// <param name="cameraToUse">Can set to default the front or rear camera.</param>
		public static void TakePicture<TViewModel>(BaseViewController<TViewModel> parent, Action<NSDictionary> callback, Action onCancel, UIImagePickerControllerCameraDevice cameraToUse = UIImagePickerControllerCameraDevice.Front)
            where TViewModel : BaseViewModel
		{
			var status = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (status == AVAuthorizationStatus.NotDetermined)
			{
				AVCaptureDevice.RequestAccessForMediaType(AVMediaType.Video, (accessGranted) =>
				{
					if (accessGranted)
					{
						// Open the camera
						parent.InvokeOnMainThread(delegate
						{
							InitializeCamera(parent, callback, onCancel, cameraToUse);
						});
					}
					// If they have said no do nothing.
				});
			}
			else if (status == AVAuthorizationStatus.Authorized)
			{
				// Open the camera
				InitializeCamera(parent, callback, onCancel, cameraToUse);
			}
			else
			{
				// They said no in the past, so show them an alert to direct them to the app's settings.
				OpenSettingsAlert(parent, "Camera Access Required", "Authorization to access the camera is required to use this feature of the app.");
			}
		}

        /// <summary>
        /// Opens the photo library to allow the user to choose a picture.
        /// </summary>
        /// <param name="parent">The parent view controller that invoked the method.</param>
        /// <param name="callback">The function to call after a picture was chosen.</param>
        public static void SelectPicture<TViewModel>(BaseViewController<TViewModel> parent, Action<NSDictionary> callback, Action onCancel)
            where TViewModel : BaseViewModel
        {
			var status = PHPhotoLibrary.AuthorizationStatus;
			if (status == PHAuthorizationStatus.NotDetermined)
			{
				PHPhotoLibrary.RequestAuthorization((PHAuthorizationStatus authStatus) =>
				{
					if (authStatus == PHAuthorizationStatus.Authorized)
					{
						// Show the media picker.
						parent.InvokeOnMainThread(delegate
						{
							InitializePhotoPicker(parent, callback, onCancel);
						});
					}
					// If they have said no do nothing.
				});
			}
			else if (status == PHAuthorizationStatus.Authorized)
			{
				// Show the media picker.
				InitializePhotoPicker(parent, callback, onCancel);
			}
			else
			{
				// They said no in the past, so show them an alert to direct them to the app's settings.
				OpenSettingsAlert(parent, "Photo Access Required", "Authorization to access photos is required to use this feature of the app.");
			}
        }

		/// <summary>
		/// Opens an alert with a button to dismiss or go to the app's settings.
		/// </summary>
		/// <param name="parent">Parent view controller</param>
		/// <param name="title">Title of the alert</param>
		/// <param name="message">Alert message</param>
		private static void OpenSettingsAlert<TViewModel>(BaseViewController<TViewModel> parent, string title, string message)
            where TViewModel : BaseViewModel
		{
			parent.InvokeOnMainThread(delegate
			{
				//Create Alert
				var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
				//Add Cancel Action
				alertController.AddAction(UIAlertAction.Create("Return to App", UIAlertActionStyle.Default, null));
				// Add Open Settings Action
				alertController.AddAction(UIAlertAction.Create("Open Settings", UIAlertActionStyle.Default, (UIAlertAction obj) => { 
					var settingsString = UIKit.UIApplication.OpenSettingsUrlString;
					var url = new NSUrl(settingsString);
					UIApplication.SharedApplication.OpenUrl(url);
				}));
				// Present Alert
				parent.PresentViewController(alertController, true, null);
			});
		}

		/// <summary>
		/// Initializes the photo picker view controller.
		/// </summary>
		/// <param name="parent">Parent view controller</param>
		/// <param name="callback">Callback once photo is selected.</param>
		/// <param name="onCancel">On cancel action.</param>
		private static void InitializePhotoPicker<TViewModel>(BaseViewController<TViewModel> parent, Action<NSDictionary> callback, Action onCancel)
            where TViewModel : BaseViewModel
		{
			_callback = callback;
			_onCancel = onCancel;
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			parent.PresentViewController(picker, true, null);
		}

		/// <summary>
		/// Initializes the camera.
		/// </summary>
		/// <param name="parent">Parent view controller.</param>
		/// <param name="callback">Callback once photo is taken</param>
		/// <param name="onCancel">On cancel action</param>
		/// <param name="cameraToUse">Camera to use.</param>
		private static void InitializeCamera<TViewModel>(BaseViewController<TViewModel> parent, Action<NSDictionary> callback, Action onCancel, UIImagePickerControllerCameraDevice cameraToUse)
            where TViewModel : BaseViewModel
		{
			_callback = callback;
			_onCancel = onCancel;
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.Camera;
			var cameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(cameraToUse);
			if (cameraAvailable)
			{
				picker.CameraDevice = cameraToUse;
				parent.PresentViewController(picker, true, null);

			}
			else
			{
				parent.ShowAlert("The camera is unavailable", "No Camera");
			}
		}

        /// <summary>
        /// Dismiss the modal photo
        /// </summary>
        public static void CloseImagePicker()
        {
            if (picker != null)
            {
                picker.DismissViewController(true, null);
                picker.Dispose();
                picker = null;
            }
        }

		/// <summary>
		/// Determines if the user has granted access to the camera
		/// </summary>
		/// <returns><c>true</c>, if camera enabled was enabled, <c>false</c> otherwise.</returns>
		private static bool IsCameraEnabled()
		{
			var isEnabled = false;
			var authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			switch (authStatus)
			{
				case AVAuthorizationStatus.NotDetermined:
				case AVAuthorizationStatus.Restricted:
				case AVAuthorizationStatus.Denied:
					break;
				case AVAuthorizationStatus.Authorized:
					isEnabled = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return isEnabled;
		}
    }
}

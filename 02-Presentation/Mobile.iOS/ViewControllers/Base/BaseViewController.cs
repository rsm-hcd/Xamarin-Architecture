using Foundation;
using System;
using System.IO;
using System.Net;
using UIKit;
using MvvmCross.iOS.Views;
using Mobile.ViewModels.ViewModels;

namespace Mobile.iOS.ViewControllers.Base
{
    public abstract partial class BaseViewController<TViewModel> : MvxViewController 
        where TViewModel : BaseViewModel
    {
        #region Variables

        private UIAlertController _alert;

        #endregion Variables

        #region Constructors

        protected BaseViewController(IntPtr handle) : base(handle)
        {
        }

        protected BaseViewController() : base()
        {
        }

        #endregion Constructors

        #region Properties

		protected abstract string ScreenName { get; }

        protected nfloat TopBarHeight
        {
            get
            {
                return (NavigationController != null ? NavigationController.NavigationBar.Bounds.Size.Height : 0) + UIApplication.SharedApplication.StatusBarFrame.Size.Height;
            }
        }

        #endregion Properties

        #region Overrides

		/// <summary>
		/// Style the navigation bar. Initialize the loading panel.
		/// </summary>
        public override void ViewDidLoad()
        {
			base.ViewDidLoad();
        }

		/// <summary>
		/// Wire up notifications for entering and background and foreground. Add gradient to the loading panel.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

		/// <summary>
		/// Remove notification observers for entering foreground and background.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
		}

		/// <summary>
		/// Tracks the screen view when the view appears
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		#endregion Overrides

		#region Protected Methods

		/// <summary>
        /// Displays an alert dialog.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">(optional) The title of the alert. Defaults to 'Error'.</param>
        public void ShowAlert(string message, string title="Error")
        {
            InvokeOnMainThread(delegate
            {
                //Create Alert
                _alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                //Add Action
                _alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                // Present Alert
                PresentViewController(_alert, true, null);
            });
        }

        /// <summary>
        /// Checks to see if the remote image has been cached localy, and if not it is downloaded asynchronously,
        /// and cached locally after the download completes. If a callback is supplied it is called after the local 
        /// image is available.
        /// </summary>
        /// <param name="imageUrl">The remote file to access.</param>
        /// <param name="fileNameSuffix">The custom text to append to the file name</param>
        /// <param name="onDownloadCompleted">The callback for once the image is available.</param>
        protected void SaveRemoteImageLocally(string imageUrl, string fileNameSuffix, Action<string> onDownloadCompleted)
        {
            /*
                Example Call:
                SaveRemoteImageLocally("http://www.mountnittany.org/assets/images/physician-546-115x0/image-4137.jpg", (string localPath) => {
                    BeginInvokeOnMainThread(() => {
                        imgTest.Image = UIImage.FromFile(localPath);
                    });
                });
            */
            try
            {
                if (imageUrl.IndexOf(".") > -1)
                {
                    var url = new Uri(imageUrl);
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					string localFilename = string.Empty;
					if (!string.IsNullOrEmpty(fileNameSuffix))
					{
						localFilename = Path.GetFileName(url.LocalPath.Insert(url.LocalPath.IndexOf("."), fileNameSuffix));
					}
					else
					{
						localFilename = Path.GetFileName(url.LocalPath);
					}
                    string localPath = Path.Combine(documentsPath, localFilename);
                    // Check to see if the image has been cached locally, and if so load it from the device storage.
                    if (File.Exists(localPath))
                    {
                        if (onDownloadCompleted != null)
                        {
                            onDownloadCompleted(localPath);
                        }
                    }
                    else
                    {
                        // Since the file is not cached locally, we will make an async web request to download the file.
                        var webClient = new WebClient();
                        webClient.DownloadDataCompleted += (s, e) =>
                        {
                            try
                            {
                                var bytes = e.Result; // get the downloaded data
                                File.WriteAllBytes(localPath, bytes); // writes to local storage
                                if (onDownloadCompleted != null)
                                {
                                    onDownloadCompleted(localPath);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.StackTrace);
                            }
                        };

                        webClient.DownloadDataAsync(url);
                    }
                }
            }
            catch (Exception)
            {
                ShowAlert("Unable to download the image.");
            }
        }

        #endregion Protected Methods

		#region Private Methods

		#endregion Private Methods
    }
}

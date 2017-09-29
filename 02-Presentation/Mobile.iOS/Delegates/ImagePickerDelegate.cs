using Foundation;
using System;
using UIKit;

namespace Mobile.iOS.Delegates
{
    public class ImagePickerDelegate : UIImagePickerControllerDelegate
    {
        #region Properties

		public Action<NSDictionary> Callback    { get; set; }
        public Action               OnCancel    { get; set; }

        #endregion Properties

        public ImagePickerDelegate(Action<NSDictionary> callback, Action onCancel)
        {
            Callback = callback;
            OnCancel = onCancel;
        }

        public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            picker.DismissModalViewController(true);
            Callback?.Invoke(info);
        }

        public override void Canceled(UIImagePickerController picker)
        {
            OnCancel?.Invoke();
        }
    }
}

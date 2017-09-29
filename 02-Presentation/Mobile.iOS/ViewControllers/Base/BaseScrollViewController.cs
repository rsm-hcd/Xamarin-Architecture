using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using Mobile.ViewModels.ViewModels;

namespace Mobile.iOS.ViewControllers.Base
{
    public abstract partial class BaseScrollViewController<TViewModel> : BaseViewController<TViewModel>
        where TViewModel : BaseViewModel
    {
        #region Variables 

        private NSObject _keyboardShowObserver;
        private NSObject _keyboardHideObserver;
        private NSObject _uiTextViewEditingObserver;
        private NSObject _uiTextViewStoppedEditingObserver;
        private NSObject _uiTextFieldEditingObserver;
        private NSObject _uiTextFieldStoppedEditingObserver;
        protected UIView _activeField;
		protected NSLayoutConstraint _contentViewBottomConstraint;

        #endregion Variables

        #region Properties

        protected float ScrollVerticalOffset { get; set; }
		protected UIView ScrollViewBelow { get; set; }
		protected UIView PinScrollViewBottomTo { get; set; }
		protected UIScrollView ScrollView { get; set; }
		protected UIView ContentView { get; set; }
        protected bool DelayScrollViewLayout { get; set; }

        #endregion Properties

        #region Constructor

        protected BaseScrollViewController(IntPtr handle) : base(handle)
        {
        }
        protected BaseScrollViewController() : base()
        {
        }

        #endregion Constructor

        #region Overrides

		/// <summary>
		/// Configures the scroll view.
		/// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            AddScrollContainers();
        }

		/// <summary>
		/// Adds keyboard and text field observers.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            // register for keyboard notifications
            _keyboardShowObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardWillShow);
            _keyboardHideObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidHideNotification, KeyboardWillHide);
            // Observe whenever a text view begins and end editing.
            _uiTextViewEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextView.TextDidBeginEditingNotification, FieldIsEditing);
            _uiTextViewStoppedEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextView.TextDidEndEditingNotification, FieldStoppedEditing);
            _uiTextFieldEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidBeginEditingNotification, FieldIsEditing);
            _uiTextFieldStoppedEditingObserver = NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidEndEditingNotification, FieldStoppedEditing);
			// Bind the bottom of the content view to the bottom of the last subview in the content view. This allows the content view to auto-grow.
			UpdateContentViewBottomConstraint();
        }

		/// <summary>
		/// Removes keyboard and text field observers.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
			RemoveObservers();
        }

		#endregion Overrides

		#region Protected Methods

		/// <summary>
		/// Sets the bottom constraint of the content view based on the last subview.
		/// </summary>
		protected virtual void UpdateContentViewBottomConstraint()
		{
			if (ContentView.Subviews != null && ContentView.Subviews.Length > 0)
			{
				if (_contentViewBottomConstraint != null)
				{
					ContentView.RemoveConstraint(_contentViewBottomConstraint);
				}
				var last = ContentView.Subviews[ContentView.Subviews.Length - 1];
				_contentViewBottomConstraint = NSLayoutConstraint.Create(ContentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, last, NSLayoutAttribute.Bottom, 1, 40f);
				ContentView.AddConstraint(
					_contentViewBottomConstraint
				);
				ContentView.SetNeedsLayout();
				ContentView.LayoutIfNeeded();
			}
		}

        /// <summary>
        /// Creates the layout constraints for the views and fields on the screen.
        /// </summary>
        protected void SetupContainerLayoutConstraints(UIView mainView, UIScrollView scrollView, UIView contentView, float scrollViewVerticalOffset, UIView scrollViewBelow, UIView pinScrollViewBottomTo)
        {
            // scrollView constraints
            mainView.AddConstraints(new[] {
                NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.Right, 1, 0),
                NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.Left, 1, 0)
            });

			if (pinScrollViewBottomTo != null)
			{
				mainView.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, pinScrollViewBottomTo, NSLayoutAttribute.Top, 1, 0));
			}
			else
			{
				mainView.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.Bottom, 1, 0));
			}

			if (scrollViewBelow != null)
			{
				mainView.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, scrollViewBelow, NSLayoutAttribute.Bottom, 1, scrollViewVerticalOffset));
			}
			else
			{
				mainView.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.Top, 1, scrollViewVerticalOffset));
			}

            // Add constraints for content view
            mainView.AddConstraints(new[] {
                NSLayoutConstraint.Create(contentView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Top, 1, 0),
                NSLayoutConstraint.Create(contentView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Right, 1, 0),
                NSLayoutConstraint.Create(contentView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Bottom, 1, 0),
                NSLayoutConstraint.Create(contentView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Left, 1, 0),
                NSLayoutConstraint.Create(contentView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.Width, 1, 0)
            });
        }

        #endregion Protected Methods

		#region Private Methods

		/// <summary>
		/// Adds the scroll view and content view to the main view. Configures layout constraints.
		/// </summary>
		private void AddScrollContainers()
		{
			ScrollView = new UIScrollView() { TranslatesAutoresizingMaskIntoConstraints = false };
			ContentView = new UIView() { TranslatesAutoresizingMaskIntoConstraints = false };
			ScrollView.AddSubview(ContentView);
			View.AddSubview(ScrollView);
			if (!DelayScrollViewLayout)
			{
				// Setup scroll view and content view constraints
				SetupContainerLayoutConstraints(View, ScrollView, ContentView, ScrollVerticalOffset, ScrollViewBelow, PinScrollViewBottomTo);
			}
		}

		/// <summary>
        /// Called when the Keyboard is going to be hidden
        /// </summary>
        /// <param name="notification"></param>
        private void KeyboardWillHide(NSNotification notification)
		{
			UIView.Animate(.3f, () =>
			{
				var contentInsets = UIEdgeInsets.Zero;
				ScrollView.ContentInset = contentInsets;
				ScrollView.ScrollIndicatorInsets = contentInsets;
			});
		}

		/// <summary>
		/// Called whenever the keyboard is going to be displayed.
		/// </summary>
		/// <param name="notification"></param>
		private void KeyboardWillShow(NSNotification notification)
		{
			if (_activeField != null)
			{
				var info = notification.UserInfo;
				var keyboardSize = ((NSValue)info.ValueForKey(UIKeyboard.FrameBeginUserInfoKey)).CGRectValue.Size;
				UIView.Animate(.3f, () => { 
					CenterViewInScroll(_activeField, ScrollView, (float)keyboardSize.Height);
				});
			}
		}

		/// <summary>
		/// Centers the view in scroll.
		/// </summary>
		/// <param name="viewToCenter">View to center.</param>
		/// <param name="scrollView">Scroll view.</param>
		/// <param name="keyboardHeight">Keyboard height.</param>
		private void CenterViewInScroll(UIView viewToCenter, UIScrollView scrollView, float keyboardHeight)
		{
			var contentInsets = new UIEdgeInsets(0.0f, 0.0f, keyboardHeight, 0.0f);
			scrollView.ContentInset = contentInsets;
			scrollView.ScrollIndicatorInsets = contentInsets;

			// Position of the active field relative isnside the scroll view
			var relativeFrame = viewToCenter.Superview.ConvertRectToView(viewToCenter.Frame, scrollView);

			bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
			var spaceAboveKeyboard = (landscape ? scrollView.Frame.Width : scrollView.Frame.Height) - keyboardHeight;

			// Move the active field to the center of the available space
			var offset = relativeFrame.Y - (spaceAboveKeyboard - viewToCenter.Frame.Height) / 2;
			scrollView.ContentOffset = new CGPoint(0, offset);
		}

		/// <summary>
		/// Sets which field is currently being edited
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void FieldIsEditing(NSNotification notification)
		{
			_activeField = (UIView)notification.Object;
		}

		/// <summary>
		/// Nulls out the current edited field.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void FieldStoppedEditing(NSNotification notification)
		{
			_activeField = null;
		}

		/// <summary>
		/// Removes any observers that may have been configured.
		/// </summary>
		private void RemoveObservers()
		{
			var observersToRemove = new List<NSObject>();
			if (_keyboardShowObserver != null)
			{
				observersToRemove.Add(_keyboardShowObserver);
			}
			if (_keyboardHideObserver != null)
			{
				observersToRemove.Add(_keyboardHideObserver);
			}
			if (_uiTextViewEditingObserver != null)
			{
				observersToRemove.Add(_uiTextViewEditingObserver);
			}
			if (_uiTextViewStoppedEditingObserver != null)
			{
				observersToRemove.Add(_uiTextViewStoppedEditingObserver);
			}
			if (_uiTextFieldEditingObserver != null)
			{
				observersToRemove.Add(_uiTextFieldEditingObserver);
			}
			if (_uiTextFieldStoppedEditingObserver != null)
			{
				observersToRemove.Add(_uiTextFieldStoppedEditingObserver);
			}
			if (observersToRemove.Count > 0)
			{
				NSNotificationCenter.DefaultCenter.RemoveObservers(observersToRemove.ToArray());
			}
		}

		#endregion Private Methods
    }
}

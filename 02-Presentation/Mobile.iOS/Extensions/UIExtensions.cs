using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using UIKit;
using Mobile.iOS.Utilities;

namespace Mobile.iOS.Extensions
{
    public static class UIExtensions
    {
		#region Public Methods

		/// <summary>
        /// Generates a UIColor from a provided Hex string and alpha value
        /// </summary>
        /// <param name="hexString">hex string</param>
        /// <param name="alpha">alpha value (0.0 - 1.0)</param>
        /// <returns></returns>
        public static UIColor ToUIColor(this string hexString, nfloat alpha)
        {
            hexString = hexString.Replace("#", "");

            if (hexString.Length == 3)
                hexString = hexString + hexString;

            if (hexString.Length != 6)
                throw new Exception("Invalid hex string");

            var red = int.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            var green = int.Parse(hexString.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            var blue = int.Parse(hexString.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

            return UIColor.FromRGB(red, green, blue).ColorWithAlpha(alpha);
        }

        /// <summary>
        /// Generates a UIColor from a provided Hex string with alpha value set to 1
        /// </summary>
        /// <param name="hexString">hex string</param>
        /// <returns></returns>
        public static UIColor ToUIColor(this string hexString)
        {
            return ToUIColor(hexString, 1);
        }

        /// <summary>
        /// Renders the supplied view as a circle (assuming that the dimensions are the same).
        /// </summary>
        /// <param name="view">The UIView to make into a circle.</param>
        public static void MakeCircle(this UIView view)
        {
			view.LayoutIfNeeded();
            view.Layer.CornerRadius = view.Bounds.Size.Width / 2;
            view.Layer.MasksToBounds = true;
        }

		/// <summary>
		/// Adds a new layer to the view's parent and then adds the view as a subview to the shadow view.
		/// </summary>
		/// <returns>The shadow view.</returns>
		/// <param name="view">The view to add a shadow to.</param>
		/// <param name="color">The shadow color.</param>
		/// <param name="shadowRadius">The shadow radius.</param>
		/// <param name="shadowOffset">The shadow offset.</param>
		/// <param name="opacity">The shadow opacity.</param>
        public static UIView AddShadow(this UIView view, UIColor color, float shadowRadius, CGSize shadowOffset, float opacity)
        {
            var shadowView = new UIView(view.Frame);
            shadowView.Layer.ShadowColor = color.CGColor;
            shadowView.Layer.ShadowOffset = shadowOffset;
            shadowView.Layer.ShadowRadius = shadowRadius;
            shadowView.Layer.ShadowOpacity = opacity;
            shadowView.Layer.MasksToBounds = false;
            shadowView.ClipsToBounds = false;
            view.Superview.InsertSubviewBelow(shadowView, view);
            shadowView.AddSubview(view);
            return shadowView;
        }

        /// <summary>
        /// Adds a gradient layer as a background layer to a view.
        /// </summary>
        /// <param name="view">The view to add the gradient to.</param>
        /// <param name="gradient">A gradient layer.</param>
        /// <param name="index">The index to insert the gradient layer (default=0)</param>
        public static void AddGradientBackground(this UIView view, CAGradientLayer gradient, int index = 0, bool animate = false)
        {
            gradient.Frame = view.Bounds;
            gradient.MasksToBounds = true;
            CAGradientLayer existingGradient = null;

            if (view.Layer.Sublayers != null)
            {
                foreach (var l in view.Layer.Sublayers)
                {
                    if (l.GetType() == typeof(CAGradientLayer))
                    {
                        existingGradient = (CAGradientLayer)l;
                        break;
                    }
                }
            }
			view.Layer.InsertSublayer(gradient, index);
			gradient.Opacity = animate ? 0 : 1;
			if (existingGradient != null)
			{
				existingGradient.Opacity = 1;
				view.Layer.InsertSublayer(gradient, index + 1);
				if (animate)
				{
					// Create fade in animation for the new gradient
					var fadein = CABasicAnimation.FromKeyPath("opacity");
					fadein.SetTo(NSNumber.FromFloat(1));
					fadein.Duration = .5;
					fadein.AutoReverses = false;
					fadein.RemovedOnCompletion = false;
					fadein.FillMode = CAFillMode.Forwards;
					// Create fade out animation for the existing gradient
					var fadeout = CABasicAnimation.FromKeyPath("opacity");
					fadeout.SetTo(NSNumber.FromFloat(0));
					fadeout.Duration = .5;
					fadeout.AutoReverses = false;
					fadeout.RemovedOnCompletion = false;
					fadeout.FillMode = CAFillMode.Forwards;
					fadeout.AnimationStopped += (sender, e) =>
					{
						existingGradient.RemoveFromSuperLayer();
					};
					// Execute the animations.
					gradient.AddAnimation(fadein, "opacityIN");
					existingGradient.AddAnimation(fadeout, "opacityOUT");
				}
				else
				{
					existingGradient.RemoveFromSuperLayer();
				}
			}
        }

		/// <summary>
		/// Removes a gradient background from a view.
		/// </summary>
		/// <param name="view">The view to remove the gradient layer from.</param>
		public static void RemoveGradientBackground(this UIView view)
		{
			if (view.Layer.Sublayers != null)
			{
				foreach (var l in view.Layer.Sublayers)
				{
					if (l.GetType() == typeof(CAGradientLayer))
					{
						l.RemoveFromSuperLayer();
						break;
					}
				}
			}
		}

        /// <summary>
        /// Adds an inner shadow layer to the referenced view.
        /// </summary>
        /// <param name="view">The UIView to apply the shadow to.</param>
        /// <param name="shadowColor">The color of the shadow.</param>
        /// <param name="shadowOpacity">The opacity of the shadow.</param>
        /// <param name="shadowRadius">The radius of the shadow.</param>
        /// <param name="shadowOffset">The offset of the shadow relative to its parent.</param>
        public static void AddInnerShadow(this UIView view, UIColor shadowColor, float shadowOpacity, float shadowRadius, CGSize shadowOffset)
        {
            var innerShadow = new CALayer();
            // Shadow path (1pt ring around bounds)
            var path = UIBezierPath.FromRect(innerShadow.Bounds.Inset(-1, -1));
            var cutout = UIBezierPath.FromRect(innerShadow.Bounds).BezierPathByReversingPath();
            path.AppendPath(cutout);
            innerShadow.ShadowPath = path.CGPath;
            innerShadow.MasksToBounds = true;
            // Shadow properties
            innerShadow.ShadowColor = shadowColor.CGColor;
            innerShadow.ShadowOffset = shadowOffset;
            innerShadow.ShadowOpacity = shadowOpacity;
            innerShadow.ShadowRadius = shadowRadius;
            view.Layer.AddSublayer(innerShadow);
        }

		/// <summary>
		/// Fades a view in.
		/// </summary>
		/// <param name="view">The view to fade in.</param>
		/// <param name="delay">Animation delay.</param>
		/// <param name="afterAnimation">After animation callback.</param>
        public static void FadeIn(this UIView view, float delay = 0f, Action afterAnimation = null)
        {
			view.Alpha = 0;
			view.Hidden = false;
			UIView.Animate(0.3f, delay, UIViewAnimationOptions.CurveLinear, () =>
			{
				view.Alpha = 1;
                afterAnimation?.Invoke();
            }, null);
        }

		/// <summary>
		/// Fades a view out.
		/// </summary>
		/// <param name="view">The view to fade.</param>
		/// <param name="delay">Animation delay.</param>
		/// <param name="afterAnimation">After animation callback.</param>
        public static void FadeOut(this UIView view, float delay = 0f, Action afterAnimation = null)
        {
			UIView.Animate(0.3f, delay, UIViewAnimationOptions.CurveLinear, () =>
			{
				view.Alpha = 0;
			}, () =>
			{
				view.Hidden = true;
                afterAnimation?.Invoke();
            });
        }

        /// <summary>
        /// Rounds 1 to 4 corners of a view.
        /// </summary>
        /// <param name="view">The view to apply the rounding to.</param>
        /// <param name="corners">The corners to round</param>
        /// <param name="radii">The size of the border radius.</param>
        public static void RoundCorners(this UIView view, UIRectCorner corners, CGSize radii)
        {
            view.Layer.CornerRadius = 0;
            var shapePath = UIBezierPath.FromRoundedRect(view.Bounds, corners, radii);
            using (var cornerLayer = new CAShapeLayer())
            {
                cornerLayer.Frame = view.Bounds;
                cornerLayer.Path = shapePath.CGPath;
                view.Layer.Mask = cornerLayer;
            }
        }

        /// <summary>
        /// Adds borders (as UIViews) to a view.
        /// </summary>
        /// <param name="view">The view to add the borders to.</param>
        /// <param name="edges">The edges to add borders to.</param>
        /// <param name="borderColor">The color of the borders.</param>
        /// <param name="thickness">The thickness of the borders.</param>
        public static void AddBordersToView(this UIView view, UIRectEdge edges, UIColor borderColor, float thickness = 1)
        {
            if(edges.HasFlag(UIRectEdge.Top) || edges.HasFlag(UIRectEdge.All))
            {
                var top = GetBorderView(borderColor);
                view.Add(top);
                view.AddConstraints(new[] {
                    NSLayoutConstraint.Create(top, NSLayoutAttribute.Top, NSLayoutRelation.Equal, view, NSLayoutAttribute.Top, 1, -thickness),
                    NSLayoutConstraint.Create(top, NSLayoutAttribute.Left, NSLayoutRelation.Equal, view, NSLayoutAttribute.Left, 1, 0),
                    NSLayoutConstraint.Create(top, NSLayoutAttribute.Right, NSLayoutRelation.Equal, view, NSLayoutAttribute.Right, 1, 0),
                    NSLayoutConstraint.Create(top, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, thickness)
                });
            }
            if (edges.HasFlag(UIRectEdge.Right) || edges.HasFlag(UIRectEdge.All))
            {
                var right = GetBorderView(borderColor);
                view.Add(right);
                view.AddConstraints(new[] {
                    NSLayoutConstraint.Create(right, NSLayoutAttribute.Top, NSLayoutRelation.Equal, view, NSLayoutAttribute.Top, 1, 0),
                    NSLayoutConstraint.Create(right, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, view, NSLayoutAttribute.Bottom, 1, 0),
                    NSLayoutConstraint.Create(right, NSLayoutAttribute.Right, NSLayoutRelation.Equal, view, NSLayoutAttribute.Right, 1, thickness),
                    NSLayoutConstraint.Create(right, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, thickness)
                });
            }
            if (edges.HasFlag(UIRectEdge.Bottom) || edges.HasFlag(UIRectEdge.All))
            {
                var bottom = GetBorderView(borderColor);
                view.Add(bottom);
                view.AddConstraints(new[] {
                    NSLayoutConstraint.Create(bottom, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, view, NSLayoutAttribute.Bottom, 1, thickness),
                    NSLayoutConstraint.Create(bottom, NSLayoutAttribute.Left, NSLayoutRelation.Equal, view, NSLayoutAttribute.Left, 1, 0),
                    NSLayoutConstraint.Create(bottom, NSLayoutAttribute.Right, NSLayoutRelation.Equal, view, NSLayoutAttribute.Right, 1, 0),
                    NSLayoutConstraint.Create(bottom, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, thickness)
                });
            }
            if (edges.HasFlag(UIRectEdge.Left) || edges.HasFlag(UIRectEdge.All))
            {
                var left = GetBorderView(borderColor);
                view.Add(left);
                view.AddConstraints(new[] {
                    NSLayoutConstraint.Create(left, NSLayoutAttribute.Top, NSLayoutRelation.Equal, view, NSLayoutAttribute.Top, 1, 0),
                    NSLayoutConstraint.Create(left, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, view, NSLayoutAttribute.Bottom, 1, 0),
                    NSLayoutConstraint.Create(left, NSLayoutAttribute.Left, NSLayoutRelation.Equal, view, NSLayoutAttribute.Left, 1, -thickness),
                    NSLayoutConstraint.Create(left, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, thickness)
                });
            }
        }

        /// <summary>
        /// Creates a byte array from an image
        /// </summary>
        /// <param name="image">The image to convert.</param>
        /// <returns>A byte array containing image data.</returns>
        public static byte[] ToNSData(this UIImage image)
        {

            if (image == null)
            {
                return null;
            }
            NSData data = null;

            try
            {
                data = image.AsJPEG();
                return data.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                if (data != null)
                {
                    data.Dispose();
                    data = null;
                }
            }
        }

        /// <summary>
        /// Converts a byte array to an image.
        /// </summary>
        /// <param name="data">The byte array containing image data.</param>
        /// <returns>An image.</returns>
        public static UIImage ToImage(this byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            UIImage image = null;
			UIImage orientedImage = null;
            try
            {
                image = new UIImage(NSData.FromArray(data));
				orientedImage = new UIImage(image.CGImage, 1, image.Orientation);
                data = null;
            }
            catch (Exception)
            {
                return null;
            }
            return orientedImage;
        }

		/// <summary>
		/// Makes the view's layer contain a shadow.
		/// </summary>
		/// <param name="view">View.</param>
        public static void MakeLayerShadow(this UIView view)
        {
            view.Layer.ShadowColor = AppColors.BLACK.ToUIColor().CGColor;
            view.Layer.ShadowOffset = new CGSize(0f, 10f);
            view.Layer.ShadowRadius = 5f;
            view.Layer.ShadowOpacity = .2f;
            view.Layer.MasksToBounds = false;
        }

		/// <summary>
		/// Invokes an action on the main UI thread
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <param name="controller">View controller containiner the main UI thread</param>
		public static void ExecuteOnMainThread(this Action action, UIViewController controller)
		{
			controller.BeginInvokeOnMainThread(() =>
			{
				action();
			});
		}

		#endregion Public Methods

		#region Private Methods

        /// <summary>
        /// Gets a view to use as a view's border.
        /// </summary>
        /// <param name="borderColor">The border color.</param>
        /// <returns>A view to use as a border.</returns>
        private static UIView GetBorderView(UIColor borderColor)
        {
            return new UIView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                BackgroundColor = borderColor
            };
        }

		#endregion Private Methods
    }
}

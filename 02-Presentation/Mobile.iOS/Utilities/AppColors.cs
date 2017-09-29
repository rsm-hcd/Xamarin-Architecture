using CoreAnimation;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;
using UIKit;
using XamSvg;
using Mobile.iOS.Extensions;

namespace Mobile.iOS.Utilities
{
	public static class AppColors
	{
		#region HEX Colors

		public const string BLACK = "000000";
		public const string WHITE = "FFFFFF";

		#endregion

		#region Gradients

		#endregion Gradients

		#region ColorMaps

		public static UIColor ToUIColor(string color)
		{
			return UIExtensions.ToUIColor(color);
		}

		public static ISvgColorMapper GetColorMapper(string fromColorHex, string toColorHex)
		{
			return GetColorMapper(new Dictionary<string, string> { { fromColorHex, toColorHex } });
		}

		public static ISvgColorMapper GetColorMapper(Dictionary<string, string> mappings)
		{
			return SvgColorMapperFactory.FromDic(mappings);
		}

		#endregion ColorMaps

		#region Private Methods

		/// <summary>
		/// Gets a basic 2 stop linear gradient.
		/// </summary>
		/// <returns>The linear gradient.</returns>
		/// <param name="startColor">Start color.</param>
		/// <param name="endColor">End color.</param>
		private static CAGradientLayer GetLinearGradient(UIColor startColor, UIColor endColor)
		{
			var colors = new CGColor[] { startColor.CGColor, endColor.CGColor };
			var stopOne = 0.0f;
			var stopTwo = 1f;
			var locations = new NSNumber[] { stopOne, stopTwo };
            var gradient = new CAGradientLayer
            {
                Colors = colors,
                Locations = locations
            };
            return gradient;
		}

		#endregion Private Methods
	}
}

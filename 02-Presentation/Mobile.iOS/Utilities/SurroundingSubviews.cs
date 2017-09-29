using UIKit;

namespace SyncedCare.Mobile.Presentation.iOS
{
	public class SurroundingSubviews
	{
		public UIView Previous { get; private set; }
		public UIView Next { get; private set;}

		public SurroundingSubviews(UIView previous, UIView next)
		{
			Previous = previous;
			Next = next;
		}
	}
}


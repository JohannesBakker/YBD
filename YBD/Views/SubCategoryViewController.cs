using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace YBD
{
	public partial class SubCategoryViewController : UIViewController
	{
		public SubCategoryViewController () : base ("SubCategoryViewController", null)
		{
		}

		public SubCategoryViewController(IntPtr ptr) : base(ptr)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}


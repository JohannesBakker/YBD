using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace YBD
{
	public partial class AboutUsViewController : UIViewController
	{
		public AboutUsViewController () : base ("AboutUsViewController", null)
		{
		}

		public AboutUsViewController(IntPtr ptr) : base(ptr)
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


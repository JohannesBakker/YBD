using System;
using CoreGraphics;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace YBD
{
	public partial class StartVideoViewController : UIViewController
	{
		bool bFinished = false;
		List<String> fileList = new List<String> ();
		NSTimer timer;

		public StartVideoViewController () : base ("StartVideoViewController", null)
		{
			Global.startVideoController = this;
		}

		public StartVideoViewController (IntPtr handle) : base (handle)
		{
			Global.startVideoController = this;
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
			try{
				var files = System.IO.Directory.EnumerateFiles("./quotes/");
				if (files != null) {
					foreach (var file in files) {
						if (file != null) {
							String filename = (String)file;
							if (filename.EndsWith (".png") || filename.EndsWith (".jpg"))
								fileList.Add (filename);
						}
					}
				}
			}
			catch (Exception e) {
			}
		}

		#region UI EVENTS
		partial void btnBeginClicked (Foundation.NSObject sender)
		{
			_start();
		}
		#endregion

		#region PRIVATE METHODS
		private void _start()
		{
			if (bFinished == false)
			{
				bFinished = true;
				Global.mainController.quickStartVideo();
			}
		}
		#endregion

		#region PUBLIC METHODS
		public void startPlaying()
		{
			bFinished = false;
			if (fileList.Count > 0) {
				Random rand = new Random ();
				int idx = rand.Next () % fileList.Count;
				String backName = fileList[idx];

				_imgBackground.Image = UIImage.FromFile (backName);
			}

			timer = NSTimer.CreateRepeatingScheduledTimer (TimeSpan.FromSeconds (5), delegate {
				_start ();
				timer.Invalidate();
			});
		}
		#endregion
	}
}


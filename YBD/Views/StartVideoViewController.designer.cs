// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace YBD
{
	[Register ("StartVideoViewController")]
	partial class StartVideoViewController
	{
		[Outlet]
		UIKit.UIButton _btnBegin { get; set; }

		[Outlet]
		UIKit.UIImageView _imgBackground { get; set; }

		[Action ("btnBeginClicked:")]
		partial void btnBeginClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_btnBegin != null) {
				_btnBegin.Dispose ();
				_btnBegin = null;
			}

			if (_imgBackground != null) {
				_imgBackground.Dispose ();
				_imgBackground = null;
			}
		}
	}
}

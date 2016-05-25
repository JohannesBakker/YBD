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
	[Register ("SelectVideoViewController")]
	partial class SelectVideoViewController
	{
		[Outlet]
		UIKit.UIImageView _imgBackground { get; set; }

		[Outlet]
		UIKit.UIView _mainView { get; set; }

		[Outlet]
		UIKit.UITableView _tblVideoList { get; set; }

		[Action ("btnClose_Clicked:")]
		partial void btnClose_Clicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_imgBackground != null) {
				_imgBackground.Dispose ();
				_imgBackground = null;
			}

			if (_tblVideoList != null) {
				_tblVideoList.Dispose ();
				_tblVideoList = null;
			}

			if (_mainView != null) {
				_mainView.Dispose ();
				_mainView = null;
			}
		}
	}
}

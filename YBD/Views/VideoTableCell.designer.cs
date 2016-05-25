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
	[Register ("VideoTableCell")]
	partial class VideoTableCell
	{
		[Outlet]
		UIKit.UIButton _btnCheck { get; set; }

		[Outlet]
		UIKit.UIButton _btnStartPlay { get; set; }

		[Outlet]
		UIKit.UIImageView _imgSelected { get; set; }

		[Outlet]
		UIKit.UIImageView _imgSnapshot { get; set; }

		[Outlet]
		UIKit.UILabel _txtCategory { get; set; }

		[Outlet]
		UIKit.UILabel _txtDescription { get; set; }

		[Outlet]
		UIKit.UILabel _txtVideoName { get; set; }

		[Action ("btnCheckClicked:")]
		partial void btnCheckClicked (Foundation.NSObject sender);

		[Action ("btnStartPlayClicked:")]
		partial void btnStartPlayClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_btnCheck != null) {
				_btnCheck.Dispose ();
				_btnCheck = null;
			}

			if (_btnStartPlay != null) {
				_btnStartPlay.Dispose ();
				_btnStartPlay = null;
			}

			if (_imgSelected != null) {
				_imgSelected.Dispose ();
				_imgSelected = null;
			}

			if (_imgSnapshot != null) {
				_imgSnapshot.Dispose ();
				_imgSnapshot = null;
			}

			if (_txtCategory != null) {
				_txtCategory.Dispose ();
				_txtCategory = null;
			}

			if (_txtDescription != null) {
				_txtDescription.Dispose ();
				_txtDescription = null;
			}

			if (_txtVideoName != null) {
				_txtVideoName.Dispose ();
				_txtVideoName = null;
			}
		}
	}
}

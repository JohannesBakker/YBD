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
	[Register ("YBDViewController")]
	partial class YBDViewController
	{
		[Outlet]
		UIKit.UIButton _btnAboutUs { get; set; }

		[Outlet]
		UIKit.UIButton _btnCategory { get; set; }

		[Outlet]
		UIKit.UIButton _btnContactUs { get; set; }

		[Outlet]
		UIKit.UIButton _btnMainView { get; set; }

		[Outlet]
		UIKit.UIButton _btnPopEditDone { get; set; }

		[Outlet]
		UIKit.UIButton _btnPopSave { get; set; }

		[Outlet]
		UIKit.UIButton _btnVideoStart { get; set; }

		[Outlet]
		UIKit.UITextField _txtPopEdit { get; set; }

		[Outlet]
		UIKit.UIView _viewAboutUs { get; set; }

		[Outlet]
		UIKit.UIView _viewContactUs { get; set; }

		[Outlet]
		UIKit.UIView _viewExplore { get; set; }

		[Outlet]
		UIKit.UIView _viewFlow { get; set; }

		[Outlet]
		UIKit.UIView _viewPopupDelete { get; set; }

		[Outlet]
		UIKit.UIView _viewPopupDeleteBar { get; set; }

		[Outlet]
		UIKit.UIView _viewPopupEdit { get; set; }

		[Outlet]
		UIKit.UIView _viewPopupEditBar { get; set; }

		[Outlet]
		UIKit.UIView _viewPopupStart { get; set; }

		[Outlet]
		UIKit.UIView _viewPopupStartBar { get; set; }

		[Outlet]
		UIKit.UIView _viewStartVideo { get; set; }

		[Outlet]
		UIKit.UIView _viewVideoList { get; set; }

		[Outlet]
		UIKit.UIView _viewVideoPlayer { get; set; }

		[Action ("_btnVideoClose:")]
		partial void _btnVideoClose (Foundation.NSObject sender);

		[Action ("btnAboutUsClick:")]
		partial void btnAboutUsClick (Foundation.NSObject sender);

		[Action ("btnCategoryClick:")]
		partial void btnCategoryClick (Foundation.NSObject sender);

		[Action ("btnContactUsClick:")]
		partial void btnContactUsClick (Foundation.NSObject sender);

		[Action ("btnMainClick:")]
		partial void btnMainClick (Foundation.NSObject sender);

		[Action ("btnPopCancelClicked:")]
		partial void btnPopCancelClicked (Foundation.NSObject sender);

		[Action ("btnPopDeleteClicked:")]
		partial void btnPopDeleteClicked (Foundation.NSObject sender);

		[Action ("btnPopDoneClicked:")]
		partial void btnPopDoneClicked (Foundation.NSObject sender);

		[Action ("btnPopEditCloseClicked:")]
		partial void btnPopEditCloseClicked (Foundation.NSObject sender);

		[Action ("btnPopSaveClicked:")]
		partial void btnPopSaveClicked (Foundation.NSObject sender);

		[Action ("btnPopStartClicked:")]
		partial void btnPopStartClicked (Foundation.NSObject sender);

		[Action ("btnPopStartCloseClicked:")]
		partial void btnPopStartCloseClicked (Foundation.NSObject sender);

		[Action ("btnVideoStartClick:")]
		partial void btnVideoStartClick (Foundation.NSObject sender);

		[Action ("txtPopEditChanged:")]
		partial void txtPopEditChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_btnAboutUs != null) {
				_btnAboutUs.Dispose ();
				_btnAboutUs = null;
			}

			if (_btnCategory != null) {
				_btnCategory.Dispose ();
				_btnCategory = null;
			}

			if (_btnContactUs != null) {
				_btnContactUs.Dispose ();
				_btnContactUs = null;
			}

			if (_btnMainView != null) {
				_btnMainView.Dispose ();
				_btnMainView = null;
			}

			if (_btnPopEditDone != null) {
				_btnPopEditDone.Dispose ();
				_btnPopEditDone = null;
			}

			if (_btnPopSave != null) {
				_btnPopSave.Dispose ();
				_btnPopSave = null;
			}

			if (_btnVideoStart != null) {
				_btnVideoStart.Dispose ();
				_btnVideoStart = null;
			}

			if (_txtPopEdit != null) {
				_txtPopEdit.Dispose ();
				_txtPopEdit = null;
			}

			if (_viewAboutUs != null) {
				_viewAboutUs.Dispose ();
				_viewAboutUs = null;
			}

			if (_viewContactUs != null) {
				_viewContactUs.Dispose ();
				_viewContactUs = null;
			}

			if (_viewExplore != null) {
				_viewExplore.Dispose ();
				_viewExplore = null;
			}

			if (_viewFlow != null) {
				_viewFlow.Dispose ();
				_viewFlow = null;
			}

			if (_viewPopupDelete != null) {
				_viewPopupDelete.Dispose ();
				_viewPopupDelete = null;
			}

			if (_viewPopupDeleteBar != null) {
				_viewPopupDeleteBar.Dispose ();
				_viewPopupDeleteBar = null;
			}

			if (_viewPopupEdit != null) {
				_viewPopupEdit.Dispose ();
				_viewPopupEdit = null;
			}

			if (_viewPopupEditBar != null) {
				_viewPopupEditBar.Dispose ();
				_viewPopupEditBar = null;
			}

			if (_viewPopupStart != null) {
				_viewPopupStart.Dispose ();
				_viewPopupStart = null;
			}

			if (_viewPopupStartBar != null) {
				_viewPopupStartBar.Dispose ();
				_viewPopupStartBar = null;
			}

			if (_viewStartVideo != null) {
				_viewStartVideo.Dispose ();
				_viewStartVideo = null;
			}

			if (_viewVideoList != null) {
				_viewVideoList.Dispose ();
				_viewVideoList = null;
			}

			if (_viewVideoPlayer != null) {
				_viewVideoPlayer.Dispose ();
				_viewVideoPlayer = null;
			}
		}
	}
}

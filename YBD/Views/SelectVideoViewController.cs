using System;
using CoreGraphics;
using System.Collections.Generic;
using System.Globalization;
using Foundation;
using UIKit;
using ObjCRuntime;

namespace YBD
{
	public partial class SelectVideoViewController : UIViewController
	{
		int _numofHour = 0;
		int _category = -1;

		List<int> _listSelected = new List<int>();
		int[] _listSelImages = new int[Global.NUM_SELECT_MAX];

		CGPoint _ptListStart = new CGPoint (-350, 145);
		CGPoint _ptListEnd = new CGPoint (219, 145);

		CGPoint _ptDisListStart = new CGPoint (219, 145);
		CGPoint _ptDisListEnd = new CGPoint (654, 145);
		double animationTime = 0.5;

		public SelectVideoViewController () : base ("SelectVideoViewController", null)
		{
		}

		public SelectVideoViewController(IntPtr ptr) : base(ptr)
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
			Global.selectController = this;
		}

		#region BUTTON EVENTS
		partial void btnClose_Clicked (Foundation.NSObject sender)
		{
			setAnimationView (_mainView);
			preAnimation (_ptDisListStart);
			doAnimation (_ptDisListEnd, new AfterAnimationModule(_af_ani_CloseClicked));
		}
		#endregion

		#region PRIVATE METHODS
		private void _af_ani_CloseClicked()
		{
			if (Global.mainController != null)
			{
				Global.mainController.closeCategory();
			}
		}

		private void changeBackground(int category)
		{
			UIImage _imageSource;

			switch (category) {
			case Global.VIDEO_TYPE_CALMING:
				_imageSource = UIImage.FromFile (Global.SEL_VIDEO_BG_TYPE_CALMING);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			case Global.VIDEO_TYPE_WARMUP:
				_imageSource = UIImage.FromFile(Global.SEL_VIDEO_BG_TYPE_WARMUP);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			case Global.VIDEO_TYPE_STRENGTH:
				_imageSource = UIImage.FromFile(Global.SEL_VIDEO_BG_TYPE_STRENGTH);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			case Global.VIDEO_TYPE_FOCUS:
				_imageSource = UIImage.FromFile(Global.SEL_VIDEO_BG_TYPE_FOCUS);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			case Global.VIDEO_LENTYPE_FIFTEEN:
				_imageSource = UIImage.FromFile(Global.SEL_VIDEO_BG_LEN_FIFTEEN);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			case Global.VIDEO_LENTYPE_THIRTY:
				_imageSource = UIImage.FromFile(Global.SEL_VIDEO_BG_LEN_THIRTY);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			case Global.VIDEO_LENTYPE_FOURTYFIVE:
				_imageSource = UIImage.FromFile(Global.SEL_VIDEO_BG_LEN_FOURTYFIVE);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			case Global.VIDEO_LENTYPE_ONEHOUR:
				_imageSource = UIImage.FromFile(Global.SEL_VIDEO_BG_LEN_ONEHOUR);
				_tblVideoList.Source = new VideoTableDataSource (category);
				break;
			default:
				_imageSource = null;
				_tblVideoList.Source = null;
				break;

			}

			_tblVideoList.ReloadData ();
			_imgBackground.Image = _imageSource; //.(null, UIControlState.Normal);
		}

		private String getSelectionImage(int id)
		{
			String ret = "";
			int itemId = -1;

			for (int i = 0; i < Global.NUM_SELECT_MAX; i++) {
				if (_listSelImages [i] == id) {
					itemId = i;
					break;
				}
			}

			if (itemId == 0)
				ret = "videoselected11.png";
			else if (itemId == 1)
				ret = "videoselected21.png";
			else if (itemId == 2)
				ret = "videoselected31.png";
			else if (itemId == 3)
				ret = "videoselected41.png";

			return ret;
		}

		private String getSelectedImage(int id)
		{
			String ret = "";
			int itemId = -1;

			for (int i = 0; i < Global.NUM_SELECT_MAX; i++) {
				if (_listSelImages [i] == -1) {
					_listSelImages [i] = id;
					itemId = i;
					break;
				}
			}

			if (itemId == 0)
				ret = "videoselected11.png";
			else if (itemId == 1)
				ret = "videoselected21.png";
			else if (itemId == 2)
				ret = "videoselected31.png";
			else if (itemId == 3)
				ret = "videoselected41.png";

			return ret;
		}

		private void removeSelectedImage(int id)
		{
			for (int i = 0; i < Global.NUM_SELECT_MAX; i++) {
				if (_listSelImages [i] == id)
					_listSelImages [i] = -1;
			}
		}

		private void clearSelImages()
		{
			for (int i = 0; i < Global.NUM_SELECT_MAX; i++) {
				_listSelImages [i] = -1;
			}
		}

		#endregion

		#region PUBLIC METHODS
		public void showVideoList(int hourSelect, int category)
		{
			_category = category;
			_numofHour = hourSelect;
			_listSelected.Clear ();
			clearSelImages ();

			changeBackground (category);
		}

		public int getNumHours()
		{
			return _numofHour;
		}

		public String addItemToSelList(int id)
		{
			if (_listSelected.Count >= _numofHour) {
				return "";
			}

			_listSelected.Add (id);
			String ret = getSelectedImage(id);

			if (_listSelected.Count >= _numofHour)
				Global.mainController.finishSelect (_listSelImages);

			return ret;
		}

		public String GetImageSelectionState(int id)
		{
			return getSelectionImage (id);
		}

		public void removeItemFromSelList(int id)
		{
			_listSelected.Remove (id);
			removeSelectedImage (id);
		}

		public void animationView()
		{
			setAnimationView (_mainView);
			preAnimation (_ptListStart);
			doAnimation (_ptListEnd, null);
		}
		#endregion

		#region ANIMATIONS
		private delegate void AfterAnimationModule();

		UIView _aniView = null;
		CGPoint _ptStart, _ptEnd;
		AfterAnimationModule _module = null;

		private void setAnimationView(UIView view)
		{
			_aniView = view;
		}

		private void preAnimation(CGPoint ptStart)
		{
			_aniView.Center = ptStart;
			_ptStart = ptStart;
			_module = null;
		}

		private void doAnimation(CGPoint ptEnd, AfterAnimationModule module)
		{
			_ptEnd = ptEnd;
			_module = module;

			UIView.BeginAnimations ("slideAnimation");

			UIView.SetAnimationDuration (animationTime);
			UIView.SetAnimationTransition (UIViewAnimationTransition.None, _aniView, false);

			UIView.SetAnimationDelegate (this);
			UIView.SetAnimationDidStopSelector (
				new Selector ("slideAnimationFinished"));

			_aniView.Center = ptEnd;
			UIView.CommitAnimations ();
		}

		[Export("slideAnimationFinished")]
		void SlideStopped ()
		{
			_aniView.Center = _ptEnd;
			if (_module != null)
				_module ();
		}
		#endregion
	}
}


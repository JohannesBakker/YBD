using System;
using CoreGraphics;
using System.IO;
using System.Collections.Generic;
using Foundation;
using UIKit;
using AVFoundation;
using CoreMedia;
using ObjCRuntime;

namespace YBD
{
	public partial class YBDViewController : UIViewController
	{
		DatabaseMgr _dbMgr;
		FavoriteMgr _favMgr;

		UIImage _selectedImage;

		int _selectedFavId = -1;
		int _curPage = -1;
		int _prevPage = -1;
		int[] _favidList = new int[Global.NUM_SELECT_MAX];

		AVPlayer _player;
		AVPlayerLayer _playerLayer;
		AVAsset _asset;
		AVPlayerItem _playerItem;

		List<String> _videoPlayList = new List<String> ();
		int _curPlayList = -1;
		bool bStopPlay = false;

		NSObject playToEndObserver = null;

		CGPoint _ptPopStart = new CGPoint(284, -49);
		CGPoint _ptPopEnd = new CGPoint(284, 25);
		double animationTime = 0.5;

		public YBDViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			try
			{
				Global.mainController = this;
				_selectedImage = UIImage.FromFile(Global.TAB_SELECTED_IMGNAME);

				_btnPopEditDone.SetImage(UIImage.FromFile(Global.ENTERFLOW_BUTTON_OFF_IMGNAME), UIControlState.Disabled);
				_txtPopEdit.AttributedPlaceholder = new NSAttributedString("Enter flow name...", foregroundColor: UIColor.White);

				changeView(Global.TAB_YOURFLOW);
				_curPage = Global.TAB_YOURFLOW;

				NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextFieldTextDidChangeNotification, TextChangedEvent);

				copyDatabase();
				_favMgr = new FavoriteMgr();
				_favMgr.initialize();

				Global.flowController.refreshFavorite();
				Global.categoryController.refreshFavorite();

				_player = new AVPlayer ();
				_playerLayer = AVPlayerLayer.FromPlayer (_player);
				_playerLayer.Frame = _viewVideoPlayer.Frame;
				_viewVideoPlayer.Layer.AddSublayer (_playerLayer);

				_player.ActionAtItemEnd = AVPlayerActionAtItemEnd.None; 

				UISwipeGestureRecognizer recognizer = new UISwipeGestureRecognizer (OnRightSwipeDetected);
				recognizer.Direction = UISwipeGestureRecognizerDirection.Right;
				_viewVideoPlayer.AddGestureRecognizer (recognizer);

				UISwipeGestureRecognizer recognizer1 = new UISwipeGestureRecognizer (OnLeftSwipeDetected);
				recognizer1.Direction = UISwipeGestureRecognizerDirection.Left;
				_viewVideoPlayer.AddGestureRecognizer (recognizer1);

				_txtPopEdit.ShouldReturn += (textField) => { 
					textField.ResignFirstResponder();
					return true; 
				};
			}
			catch (Exception e) {
			}
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion

		#region View Button Events
		partial void btnAboutUsClick (Foundation.NSObject sender)
		{
			changeView(Global.TAB_ABOUTUS);
		}

		partial void btnCategoryClick (Foundation.NSObject sender)
		{
			changeView(Global.TAB_EXPLORE);
		}

		partial void btnContactUsClick (Foundation.NSObject sender)
		{
			changeView(Global.TAB_CONTACTUS);
		}

		partial void btnMainClick (Foundation.NSObject sender)
		{
			changeView(Global.TAB_YOURFLOW);
		}

		partial void btnVideoStartClick (Foundation.NSObject sender)
		{
			List<String> randomPlayList = getRandomPlayList ();
			_videoPlayList = randomPlayList;

			changeView(Global.TAB_QUICKSTART);
		}

		private void _af_ani_btnPopCancelClicked()
		{
			_viewPopupDelete.Hidden = true;
			Global.flowController.refreshFavorite();
			Global.categoryController.refreshFavorite();
		}

		partial void btnPopCancelClicked (Foundation.NSObject sender)
		{
			setAnimationView(_viewPopupDeleteBar);
			preAnimation(_ptPopEnd);
			doAnimation(_ptPopStart, new AfterAnimationModule(_af_ani_btnPopCancelClicked));
		}

		private void _af_ani_btnPopDeleteClicked()
		{
			_viewPopupDelete.Hidden = true;
			//Global.dbMgr.removeFavFromTable(_selectedFavId);
			_favMgr.removeFavorite (_selectedFavId);
			Global.flowController.refreshFavorite();
			Global.categoryController.refreshFavorite();
		}

		partial void btnPopDeleteClicked (Foundation.NSObject sender)
		{
			setAnimationView(_viewPopupDeleteBar);
			preAnimation(_ptPopEnd);
			doAnimation(_ptPopStart, new AfterAnimationModule(_af_ani_btnPopDeleteClicked));
		}

		private void _af_ani_btnPopDoneClicked()
		{
			int favId = _favMgr.getNewFavId();
			if (favId != -1)
			{
				_favMgr.addNewFavorite(favId, _txtPopEdit.Text);

				for (int i = 0; i < Global.NUM_SELECT_MAX; i++)
				{
					if (_favidList[i] != -1)
					{
						_favMgr.addVideoIdToFavItem(favId, _favidList[i]);
					}
				}
			}

			_viewPopupEdit.Hidden = true;
			closeCategory();

			Global.flowController.refreshFavorite();
			Global.categoryController.refreshFavorite();
		}

		partial void btnPopDoneClicked (Foundation.NSObject sender)
		{
			_txtPopEdit.ResignFirstResponder();

			setAnimationView(_viewPopupEditBar);
			preAnimation(_ptPopEnd);
			doAnimation(_ptPopStart, new AfterAnimationModule(_af_ani_btnPopDoneClicked));
		}

		private void _af_ani_btnPopEditCloseClicked()
		{
			_viewPopupEdit.Hidden = true;
		}

		partial void btnPopEditCloseClicked (Foundation.NSObject sender)
		{
			_txtPopEdit.ResignFirstResponder();

			setAnimationView(_viewPopupEditBar);
			preAnimation(_ptPopEnd);
			doAnimation(_ptPopStart, new AfterAnimationModule(_af_ani_btnPopEditCloseClicked));
		}

		private void _af_ani_btnPopSaveClicked()
		{
			_txtPopEdit.Text = "";
			_txtPopEdit.BackgroundColor = UIColor.DarkGray;

			_txtPopEdit.BecomeFirstResponder ();

			_btnPopEditDone.Enabled = false;
			_viewPopupStart.Hidden = true;
			_viewPopupEdit.Hidden = false;
			this.View.BringSubviewToFront (_viewPopupEdit);

			// PopupEdit Animation
			setAnimationView (_viewPopupEditBar);
			preAnimation (_ptPopStart);
			doAnimation (_ptPopEnd, null);
		}

		partial void btnPopSaveClicked (Foundation.NSObject sender)
		{
			setAnimationView(_viewPopupStartBar);
			preAnimation(_ptPopEnd);
			doAnimation(_ptPopStart, new AfterAnimationModule(_af_ani_btnPopSaveClicked));
		}

		private void _af_ani_btnPopStartClicked()
		{
			_viewPopupStart.Hidden = true;
			List<String> videoPlayList = new List<String>();
			for (int i = 0; i < Global.NUM_SELECT_MAX; i++)
			{
				if (_favidList[i] != -1)
				{
					String videoName = Global.dbMgr.getVideoFromId(_favidList[i]);
					videoPlayList.Add(videoName);
				}
			}

			startVideoPlay (videoPlayList);
			//playVideo();
		}

		partial void btnPopStartClicked (Foundation.NSObject sender)
		{
			setAnimationView(_viewPopupStartBar);
			preAnimation(_ptPopEnd);
			doAnimation(_ptPopStart, new AfterAnimationModule(_af_ani_btnPopStartClicked));
		}

		private void _af_ani_btnPopStartCloseClicked()
		{
			_viewPopupStart.Hidden = true;
		}

		partial void btnPopStartCloseClicked (Foundation.NSObject sender)
		{
			setAnimationView(_viewPopupStartBar);
			preAnimation(_ptPopEnd);
			doAnimation(_ptPopStart, new AfterAnimationModule(_af_ani_btnPopStartCloseClicked));
		}

		partial void txtPopEditChanged (Foundation.NSObject sender)
		{
			/*String curText = _txtPopEdit.Text;
			if (curText == null || curText.Equals("") == true)
				_btnPopEditDone.Enabled = false;
			else
				_btnPopEditDone.Enabled = true;*/
		}

		private void TextChangedEvent(NSNotification notification)
		{
			UITextField field = (UITextField)notification.Object;
			if (notification.Object == _txtPopEdit) {
				String curText = _txtPopEdit.Text;
				if (curText == null || curText.Equals ("") == true) {
					_btnPopEditDone.Enabled = false;
					_txtPopEdit.BackgroundColor = UIColor.DarkGray;
				} else {
					_btnPopEditDone.Enabled = true;
					_txtPopEdit.BackgroundColor = UIColor.White;
				}
			}
		}

		partial void _btnVideoClose (Foundation.NSObject sender)
		{
			bStopPlay = true;
			_playerItem.Seek(_playerItem.Duration);
			_viewVideoPlayer.Hidden = true;
		}
		#endregion

		#region PRIVATE METHODS
		private void copyDatabase()
		{
			var appdir = NSBundle.MainBundle.ResourcePath;
			var seedFile = Path.Combine (appdir, Global.DATABASE_FILENAME);

			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine (documents, Global.DATABASE_FILENAME);
			var versionName = Path.Combine (documents, Global.VERSION_FILENAME);

			if (!File.Exists (filename))
				File.Copy (seedFile, filename);
			else {
				if (!File.Exists (versionName)) {
					File.Delete (filename);
					File.Copy (seedFile, filename);
				} else {
					StreamReader reader = File.OpenText (versionName);
					String version = reader.ReadLine ();
					reader.Close ();
					int oldVersion = int.Parse (version);
					if (oldVersion < Global.CURRENT_APP_VERSION) {
						File.Delete (filename);
						File.Copy (seedFile, filename);
					}
				}
			}

			if (File.Exists (versionName)) {
				File.Delete (versionName);
			}

			File.WriteAllText (versionName, Global.CURRENT_APP_VERSION.ToString ());

			//if (File.Exists (filename))
			//	File.Delete (filename);
			//File.Copy(seedFile, filename);

			_dbMgr = new DatabaseMgr(filename);
		}

		private void resetTabImages ()
		{
			_btnAboutUs.SetBackgroundImage(null, UIControlState.Normal);
			_btnCategory.SetBackgroundImage(null, UIControlState.Normal);
			_btnContactUs.SetBackgroundImage(null, UIControlState.Normal);
			_btnMainView.SetBackgroundImage(null, UIControlState.Normal);
			_btnVideoStart.SetBackgroundImage(null, UIControlState.Normal);
		}

		private void hideAllViews()
		{
			_viewFlow.Hidden = true;
			_viewExplore.Hidden = true;
			_viewStartVideo.Hidden = true;
			_viewAboutUs.Hidden = true;
			_viewContactUs.Hidden = true;
			_viewVideoList.Hidden = true;
			_viewPopupStart.Hidden = true;
			_viewPopupEdit.Hidden = true;
			_viewPopupDelete.Hidden = true;
			_viewVideoPlayer.Hidden = true;
		}

		private void changeView(int index)
		{
			_prevPage = _curPage;
			_curPage = index;
			resetTabImages ();

			if (index != Global.TAB_QUICKSTART && index != Global.TAB_VIDEOLIST)
				hideAllViews ();

			switch (index) {
			case Global.TAB_YOURFLOW:
				_btnMainView.SetBackgroundImage (_selectedImage, UIControlState.Normal);

				if (_prevPage == Global.TAB_EXPLORE) {
					_viewExplore.Hidden = false;
					_viewExplore.Frame = new CGRect (900, 900, 300, 300);
					Global.categoryController.preflipCards (true);
					Global.categoryController.flipCards ();
				} else {
					Global.flowController.preflipCards (false);
					_viewFlow.Hidden = false;
					_viewFlow.Frame = new CGRect (900, 900, 300, 300);
				}
				break;
			case Global.TAB_EXPLORE:
				_btnCategory.SetBackgroundImage (_selectedImage, UIControlState.Normal);

				if (_prevPage == Global.TAB_YOURFLOW) {
					_viewFlow.Hidden = false;
					Global.flowController.preflipCards (true);
					Global.flowController.flipCards ();
				} else {
					_viewExplore.Hidden = false;
					Global.categoryController.preflipCards (false);
				}
				break;
			case Global.TAB_VIDEOLIST:
				_btnCategory.SetBackgroundImage (_selectedImage, UIControlState.Normal);
				_viewVideoList.Hidden = false;
				break;
			case Global.TAB_QUICKSTART:
				_btnVideoStart.SetBackgroundImage (_selectedImage, UIControlState.Normal);
				_viewStartVideo.Hidden = false;
				this.View.BringSubviewToFront (_viewStartVideo);
				Global.startVideoController.startPlaying ();
				break;
			case Global.TAB_ABOUTUS:
				_btnAboutUs.SetBackgroundImage (_selectedImage, UIControlState.Normal);
				_viewAboutUs.Hidden = false;
				break;
			case Global.TAB_CONTACTUS:
				_btnContactUs.SetBackgroundImage (_selectedImage, UIControlState.Normal);
				_viewContactUs.Hidden = false;
				break;
			}
		}

		private void playtoNextList()
		{
			_curPlayList++;
			if (_curPlayList >= _videoPlayList.Count) {
				_player.Pause ();
				_player.Dispose ();
				_viewVideoPlayer.Hidden = true;
			} else {
				if (playToEndObserver != null) {
					playToEndObserver.Dispose ();
					playToEndObserver = null;
				}
				_asset = AVAsset.FromUrl (NSUrl.FromFilename ("./videos/" + _videoPlayList[_curPlayList]));
				_playerItem = new AVPlayerItem (_asset);

				_player.ReplaceCurrentItemWithPlayerItem (_playerItem);
				playToEndObserver = AVPlayerItem.Notifications.ObserveDidPlayToEndTime (PlayToEndNotificationHandler);

				_player.Play ();
				_viewVideoPlayer.Hidden = false;
			}
		}

		private void playtoPrevList()
		{
			_curPlayList--;
			if (_curPlayList < 0) {
				_curPlayList = 0;
			} else {
				if (playToEndObserver != null) {
					playToEndObserver.Dispose ();
					playToEndObserver = null;
				}
				_asset = AVAsset.FromUrl (NSUrl.FromFilename ("./videos/" + _videoPlayList[_curPlayList]));
				_playerItem = new AVPlayerItem (_asset);

				_player.ReplaceCurrentItemWithPlayerItem (_playerItem);
				playToEndObserver = AVPlayerItem.Notifications.ObserveDidPlayToEndTime (PlayToEndNotificationHandler);

				_player.Play ();
				_viewVideoPlayer.Hidden = false;
			}
		}

		private void PlayToEndNotificationHandler (object sender, NSNotificationEventArgs args)
		{
			if (bStopPlay == false)
				playtoNextList ();
		}

		private List<String> getRandomPlayList()
		{
			List<String> retList = new List<String> ();
			for (int i = Global.VIDEO_TYPE_WARMUP; i <= Global.VIDEO_TYPE_CALMING; i++) {
				List<VideoRecord> typeList = Global.dbMgr.getVideoList (i + 1);
				if (typeList.Count > 0) {
					Random a = new Random ();
					int randomId = a.Next () % typeList.Count;
					retList.Add (typeList [randomId].video);
				}
			}

			return retList;
		}

		private void OnLeftSwipeDetected ()
		{
			if (bStopPlay == false && _curPlayList < _videoPlayList.Count - 1)
				playtoNextList ();
		}

		private void OnRightSwipeDetected ()
		{
			if (bStopPlay == false && _curPlayList > 0)
				playtoPrevList ();
		}
		#endregion

		#region PUBLIC METHODS
		public void selectCategory(int hourSelect, int category, int currentPage)
		{
			_prevPage = currentPage;

			if (category >= Global.VIDEO_FAV_FIRST && category <= Global.VIDEO_FAV_FOURTH) {
				FavoriteItem favItem = Global.favMgr.getFavoriteFromId (category - Global.VIDEO_FAV_FIRST);
				if (favItem != null) {
					List<String> videoPlayList = new List<String> ();
					for (int i = 0; i < Global.NUM_SELECT_MAX; i++) {
						if (favItem.itemArr [i] != -1) {
							String videoName = Global.dbMgr.getVideoFromId (favItem.itemArr [i]);
							videoPlayList.Add (videoName);
						}
					}

					startVideoPlay (videoPlayList);
					//playVideo ();
				}
			} else {
				if (Global.selectController != null) {
					Global.selectController.showVideoList (hourSelect, category);
					Global.selectController.animationView ();
				}
				changeView (Global.TAB_VIDEOLIST);
			}
		}

		public void finishCardAnimation(int category)
		{
			if (category == Global.TAB_YOURFLOW) {
				_viewExplore.Hidden = true;
				_viewFlow.Hidden = false;
				Global.categoryController.preflipCards (false);
			} else if (category == Global.TAB_EXPLORE) {
				_viewExplore.Hidden = false;
				_viewFlow.Hidden = true;
				Global.flowController.preflipCards (false);
			}
		}

		public void closeCategory()
		{
			changeView (_prevPage);
		}

		public void finishSelect(int[] selIds)
		{
			_viewPopupStart.Hidden = false;

			int favId = _favMgr.getNewFavId ();
			if (favId == -1)
				_btnPopSave.Enabled = false;
			else
				_btnPopSave.Enabled = true;

			this._favidList = selIds;
			this.View.BringSubviewToFront (_viewPopupStart);

			// PopupStart Animation
			setAnimationView (_viewPopupStartBar);
			preAnimation (_ptPopStart);
			doAnimation (_ptPopEnd, null);
		}

		public void editFavorite(int favid)
		{
			_viewPopupEdit.Hidden = false;
			this.View.BringSubviewToFront (_viewPopupEdit);
		}

		public void deleteFavorite(int favid)
		{
			_selectedFavId = favid;
			_viewPopupDelete.Hidden = false;
			this.View.BringSubviewToFront (_viewPopupDelete);

			// PopupDelete Animation
			setAnimationView (_viewPopupDeleteBar);
			preAnimation (_ptPopStart);
			doAnimation (_ptPopEnd, null);
		}

		public void changeEditFavorite()
		{
			Global.bEditingFavorite = !Global.bEditingFavorite;
			Global.categoryController.refreshFavorite ();
			Global.flowController.refreshFavorite ();
		}

		public void startVideoPlay(List<String> videoPlayList)
		{
			_videoPlayList = videoPlayList;
			//changeView (Global.TAB_QUICKSTART);
			_viewStartVideo.Hidden = false;
			this.View.BringSubviewToFront (_viewStartVideo);
			Global.startVideoController.startPlaying ();
		}

		public void quickStartVideo()
		{
			_viewStartVideo.Hidden = true;
			playVideo ();
		}

		public void playVideo()
		{
			bStopPlay = false;
			//_videoPlayList = filePlayList;
			_curPlayList = -1;

			playtoNextList ();
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


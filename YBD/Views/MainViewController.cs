using System;
using CoreGraphics;
using Foundation;
using UIKit;
using ObjCRuntime;
using CoreGraphics;
using CoreAnimation;

namespace YBD
{
	public partial class MainViewController : UIViewController
	{
		double animationTime = 1.0f;

		UIButton[] btnNavArr = new UIButton[Global.NUM_FAVORITE_MAX];
		UIButton[] btnNavDelArr = new UIButton[Global.NUM_FAVORITE_MAX];
		UILabel[] lblNavArr = new UILabel[Global.NUM_FAVORITE_MAX];

		public MainViewController () : base ("MainViewController", null)
		{
			Global.flowController = this;
		}

		public MainViewController(IntPtr ptr) : base(ptr)
		{
			Global.flowController = this;
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

			btnNavArr [0] = _btnNav1;
			btnNavArr [1] = _btnNav2;
			btnNavArr [2] = _btnNav3;
			btnNavArr [3] = _btnNav4;

			btnNavDelArr [0] = _btnNav1Del;
			btnNavDelArr [1] = _btnNav2Del;
			btnNavDelArr [2] = _btnNav3Del;
			btnNavDelArr [3] = _btnNav4Del;

			lblNavArr [0] = _lblNav1;
			lblNavArr [1] = _lblNav2;
			lblNavArr [2] = _lblNav3;
			lblNavArr [3] = _lblNav4;

			preflipCards (false);
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#region BUTTON EVENTS
		partial void btnFav1Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_FIRST, Global.TAB_YOURFLOW);
			}
		}

		partial void btnFav2Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_SECOND, Global.TAB_YOURFLOW);
			}
		}

		partial void btnFav3Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_THIRD, Global.TAB_YOURFLOW);
			}
		}

		partial void btnFav4Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_FOURTH, Global.TAB_YOURFLOW);
			}
		}

		partial void btnLenFifteenClicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(1, Global.VIDEO_LENTYPE_FIFTEEN, Global.TAB_YOURFLOW);
			}
		}

		partial void btnLenFourtyFiveClicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(3, Global.VIDEO_LENTYPE_FOURTYFIVE, Global.TAB_YOURFLOW);
			}
		}

		partial void btnLenOneHourClicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(4, Global.VIDEO_LENTYPE_ONEHOUR, Global.TAB_YOURFLOW);
			}
		}

		partial void btnLenThirtyClicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(2, Global.VIDEO_LENTYPE_THIRTY, Global.TAB_YOURFLOW);
			}
		}

		partial void btnEditFavClicked (Foundation.NSObject sender)
		{
			Global.mainController.changeEditFavorite();
		}

		partial void btnFav1DelClicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(0);
		}

		partial void btnFav2DelClicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(1);
		}

		partial void btnFav3DelClicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(2);
		}

		partial void btnFav4DelClicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(3);
		}
		#endregion

		#region PUBLIC METHODS
		public void refreshFavorite()
		{
			bool bExistData = false;

			for (int i = 0; i < Global.NUM_FAVORITE_MAX; i++) {
				FavoriteItem item = Global.favMgr.getFavoriteFromId (i);
				if (item == null || item.name.Equals ("") == true) {
					btnNavArr[i].SetBackgroundImage (UIImage.FromFile ("favoritflowboxempty1.png"), UIControlState.Normal);
					btnNavDelArr [i].Hidden = true;
					lblNavArr [i].Text = "Empty";
				} else {
					btnNavArr[i].SetBackgroundImage (UIImage.FromFile (FavoriteMgr.getFavoriteImgName(i)), UIControlState.Normal);
					btnNavDelArr [i].Hidden = !Global.bEditingFavorite;
					lblNavArr [i].Text = item.name;

					bExistData = true;
				}
			}

			_btnEditFav.Hidden = !bExistData;
		}

		public void preflipCards(bool bAnimation)
		{
			preAnimation (bAnimation);
		}

		public void flipCards()
		{
			doAnimation(_btnQuick, _btnTempWarmup, new Selector("AnimationFinishedQuick"));
			doAnimation(_btnLight, _btnTempStrength, new Selector("AnimationFinishedLight"));
			doAnimation(_btnRecommended, _btnTempFocus, new Selector("AnimationFinishedRecommended"));
			doAnimation(_btnAdvanced, _btnTempCalming, new Selector("AnimationFinishedAdvanced"));

			doAnimation (_txtQuick, _txtTempWarmUp, null);
			doAnimation (_txtLight, _txtTempStrength, null);
			doAnimation (_txtRecommended, _txtTempFocus, null);
			doAnimation (_txtAdvanced, _txtTempCalming, null);
		}
		#endregion

		#region ANIMATIONS
		int i = 0;
		void preAnimation(bool bAnimation)
		{
			i = 0;

			if (bAnimation) {
				_btnQuick.Hidden = false;
				_btnLight.Hidden = false;
				_btnRecommended.Hidden = false;
				_btnAdvanced.Hidden = false;

				_txtQuick.Hidden = false;
				_txtLight.Hidden = false;
				_txtRecommended.Hidden = false;
				_txtAdvanced.Hidden = false;

				_btnTempWarmup.Hidden = false;
				_btnTempFocus.Hidden = false;
				_btnTempStrength.Hidden = false;
				_btnTempCalming.Hidden = false;

				_txtTempWarmUp.Hidden = false;
				_txtTempFocus.Hidden = false;
				_txtTempStrength.Hidden = false;
				_txtTempCalming.Hidden = false;

				_btnQuick.Alpha = 1.0f;
				_btnLight.Alpha = 1.0f;
				_btnRecommended.Alpha = 1.0f;
				_btnAdvanced.Alpha = 1.0f;

				_txtQuick.Alpha = 1.0f;
				_txtLight.Alpha = 1.0f;
				_txtRecommended.Alpha = 1.0f;
				_txtAdvanced.Alpha = 1.0f;

				_btnTempWarmup.Alpha = 0.0f;
				_btnTempFocus.Alpha = 0.0f;
				_btnTempStrength.Alpha = 0.0f;
				_btnTempCalming.Alpha = 0.0f;

				_txtTempWarmUp.Alpha = 0.0f;
				_txtTempFocus.Alpha = 0.0f;
				_txtTempStrength.Alpha = 0.0f;
				_txtTempCalming.Alpha = 0.0f;

			} else {
				_btnQuick.Hidden = false;
				_btnLight.Hidden = false;
				_btnRecommended.Hidden = false;
				_btnAdvanced.Hidden = false;

				_txtQuick.Hidden = false;
				_txtLight.Hidden = false;
				_txtRecommended.Hidden = false;
				_txtAdvanced.Hidden = false;

				_btnTempWarmup.Hidden = true;
				_btnTempFocus.Hidden = true;
				_btnTempStrength.Hidden = true;
				_btnTempCalming.Hidden = true;

				_txtTempWarmUp.Hidden = true;
				_txtTempFocus.Hidden = true;
				_txtTempStrength.Hidden = true;
				_txtTempCalming.Hidden = true;

				_btnQuick.Alpha = 1.0f;
				_btnLight.Alpha = 1.0f;
				_btnRecommended.Alpha = 1.0f;
				_btnAdvanced.Alpha = 1.0f;

				_txtQuick.Alpha = 1.0f;
				_txtLight.Alpha = 1.0f;
				_txtRecommended.Alpha = 1.0f;
				_txtAdvanced.Alpha = 1.0f;
			}
		}

		[Export("AnimationFinishedQuick")]
		void AnimationFinishedQuick ()
		{
			_btnTempWarmup.Hidden = true;
			_txtTempWarmUp.Hidden = true;
		}

		[Export("AnimationFinishedLight")]
		void AnimationFinishedLight ()
		{
			_btnTempStrength.Hidden = true;
			_txtTempStrength.Hidden = true;
		}

		[Export("AnimationFinishedRecommended")]
		void AnimationFinishedRecommended ()
		{
			_btnTempFocus.Hidden = true;
			_txtTempFocus.Hidden = true;
		}

		[Export("AnimationFinishedAdvanced")]
		void AnimationFinishedAdvanced ()
		{
			_btnTempCalming.Hidden = true;
			_txtTempCalming.Hidden = true;
			Global.mainController.finishCardAnimation (Global.TAB_EXPLORE);
		}

		void doAnimation(UIView _fromView, UIView _toView, Selector _selector)
		{
			i++;
			UIView.BeginAnimations ("flipAnimation" + i);
			UIView.SetAnimationBeginsFromCurrentState (false);

			UIView.SetAnimationDuration (animationTime);

			UIView.SetAnimationTransition (UIViewAnimationTransition.FlipFromLeft, _fromView, false);

			_fromView.Alpha = 0.0f;

			UIView.SetAnimationDelegate (this);

			UIView.CommitAnimations ();

			i++;
			UIView.BeginAnimations ("flipAnimation" + i);
			UIView.SetAnimationBeginsFromCurrentState (false);

			UIView.SetAnimationDuration (animationTime);

			UIView.SetAnimationTransition (UIViewAnimationTransition.FlipFromLeft, _toView, false);

			_toView.Alpha = 1.0f;

			UIView.SetAnimationDelegate (this);
			if (_selector != null)
				UIView.SetAnimationDidStopSelector (_selector);

			UIView.CommitAnimations ();
		}
		#endregion
	}
}


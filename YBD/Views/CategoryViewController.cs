using System;
using CoreGraphics;
using Foundation;
using UIKit;
using ObjCRuntime;
using CoreGraphics;
using CoreAnimation;

namespace YBD
{
	public partial class CategoryViewController : UIViewController
	{
		double animationTime = 1.0f;

		UIButton[] btnNavArr = new UIButton[Global.NUM_FAVORITE_MAX];
		UIButton[] btnNavDelArr = new UIButton[Global.NUM_FAVORITE_MAX];
		UILabel[] lblNavArr = new UILabel[Global.NUM_FAVORITE_MAX];

		public CategoryViewController () : base ("CategoryViewController", null)
		{
			Global.categoryController = this;
		}

		public CategoryViewController(IntPtr ptr) : base(ptr)
		{
			Global.categoryController = this;
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
			btnNavArr [0] = _btnFav1;
			btnNavArr [1] = _btnFav2;
			btnNavArr [2] = _btnFav3;
			btnNavArr [3] = _btnFav4;

			btnNavDelArr [0] = _btnFav1Del;
			btnNavDelArr [1] = _btnFav2Del;
			btnNavDelArr [2] = _btnFav3Del;
			btnNavDelArr [3] = _btnFav4Del;

			lblNavArr [0] = _lblFav1;
			lblNavArr [1] = _lblFav2;
			lblNavArr [2] = _lblFav3;
			lblNavArr [3] = _lblFav4;

			preflipCards (false);
		}

		#region BUTTON EVENTS
		partial void btnCalming_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_TYPE_CALMING, Global.TAB_EXPLORE);
			}
		}

		partial void btnFav1_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_FIRST, Global.TAB_EXPLORE);
			}
		}

		partial void btnFav2_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_SECOND, Global.TAB_EXPLORE);
			}
		}

		partial void btnFav3_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_THIRD, Global.TAB_EXPLORE);
			}
		}

		partial void btnFav4_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_FAV_FOURTH, Global.TAB_EXPLORE);
			}
		}

		partial void btnFocus_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_TYPE_FOCUS, Global.TAB_EXPLORE);
			}
		}

		partial void btnStrength_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_TYPE_STRENGTH, Global.TAB_EXPLORE);
			}
		}

		partial void btnWarmUp_Clicked (Foundation.NSObject sender)
		{
			if (Global.mainController != null)
			{
				Global.mainController.selectCategory(0, Global.VIDEO_TYPE_WARMUP, Global.TAB_EXPLORE);
			}
		}

		partial void btnFav1Del_Clicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(0);
		}

		partial void btnFav2Del_Clicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(1);
		}

		partial void btnFav3Del_Clicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(2);
		}

		partial void btnFav4Del_Clicked (Foundation.NSObject sender)
		{
			Global.mainController.deleteFavorite(3);
		}

		partial void btnEditFav_Clicked (Foundation.NSObject sender)
		{
			Global.mainController.changeEditFavorite();
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
			doAnimation(_btnWarmUp, _btnTempFifteen, new Selector("AnimationFinishedWarmUp"));
			doAnimation(_btnStrength, _btnTempThirty, new Selector("AnimationFinishedStrength"));
			doAnimation(_btnFocus, _btnTempFourtyFive, new Selector("AnimationFinishedFocus"));
			doAnimation(_btnCalming, _btnTempHour, new Selector("AnimationFinishedCalming"));

			doAnimation (_txtWarmUp, _txtTempFifteen, null);
			doAnimation (_txtStrength, _txtTempThirty, null);
			doAnimation (_txtFocus, _txtTempFourtyFive, null);
			doAnimation (_txtCalming, _txtTempOneHour, null);
		}
		#endregion

		#region ANIMATIONS
		int i = 0;
		void preAnimation(bool bAnimation)
		{
			i = 0;

			if (bAnimation) {
				_btnWarmUp.Hidden = false;
				_btnStrength.Hidden = false;
				_btnFocus.Hidden = false;
				_btnCalming.Hidden = false;

				_txtWarmUp.Hidden = false;
				_txtStrength.Hidden = false;
				_txtFocus.Hidden = false;
				_txtCalming.Hidden = false;

				_btnTempFifteen.Hidden = false;
				_btnTempThirty.Hidden = false;
				_btnTempFourtyFive.Hidden = false;
				_btnTempHour.Hidden = false;

				_txtTempFifteen.Hidden = false;
				_txtTempThirty.Hidden = false;
				_txtTempFourtyFive.Hidden = false;
				_txtTempOneHour.Hidden = false;

				_btnWarmUp.Alpha = 1.0f;
				_btnStrength.Alpha = 1.0f;
				_btnFocus.Alpha = 1.0f;
				_btnCalming.Alpha = 1.0f;

				_txtWarmUp.Alpha = 1.0f;
				_txtStrength.Alpha = 1.0f;
				_txtFocus.Alpha = 1.0f;
				_txtCalming.Alpha = 1.0f;

				_btnTempFifteen.Alpha = 0.0f;
				_btnTempThirty.Alpha = 0.0f;
				_btnTempFourtyFive.Alpha = 0.0f;
				_btnTempHour.Alpha = 0.0f;

				_txtTempFifteen.Alpha = 0.0f;
				_txtTempThirty.Alpha = 0.0f;
				_txtTempFourtyFive.Alpha = 0.0f;
				_txtTempOneHour.Alpha = 0.0f;

			} else {
				_btnWarmUp.Hidden = false;
				_btnStrength.Hidden = false;
				_btnFocus.Hidden = false;
				_btnCalming.Hidden = false;

				_txtWarmUp.Hidden = false;
				_txtStrength.Hidden = false;
				_txtFocus.Hidden = false;
				_txtCalming.Hidden = false;

				_btnTempFifteen.Hidden = true;
				_btnTempThirty.Hidden = true;
				_btnTempFourtyFive.Hidden = true;
				_btnTempHour.Hidden = true;

				_txtTempFifteen.Hidden = true;
				_txtTempThirty.Hidden = true;
				_txtTempFourtyFive.Hidden = true;
				_txtTempOneHour.Hidden = true;

				_btnWarmUp.Alpha = 1.0f;
				_btnStrength.Alpha = 1.0f;
				_btnFocus.Alpha = 1.0f;
				_btnCalming.Alpha = 1.0f;

				_txtWarmUp.Alpha = 1.0f;
				_txtStrength.Alpha = 1.0f;
				_txtFocus.Alpha = 1.0f;
				_txtCalming.Alpha = 1.0f;
			}
		}

		[Export("AnimationFinishedWarmUp")]
		void AnimationFinishedWarmUp ()
		{
			_btnTempFifteen.Hidden = true;
			_txtTempFifteen.Hidden = true;
		}

		[Export("AnimationFinishedStrength")]
		void AnimationFinishedStrength ()
		{
			_btnTempThirty.Hidden = true;
			_txtTempThirty.Hidden = true;
		}

		[Export("AnimationFinishedFocus")]
		void AnimationFinishedFocus ()
		{
			_btnTempFourtyFive.Hidden = true;
			_txtTempFourtyFive.Hidden = true;
		}

		[Export("AnimationFinishedCalming")]
		void AnimationFinishedCalming ()
		{
			_btnTempHour.Hidden = true;
			_txtTempOneHour.Hidden = true;
			Global.mainController.finishCardAnimation (Global.TAB_YOURFLOW);
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


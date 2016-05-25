
using System;
using CoreGraphics;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace YBD
{
	public partial class VideoTableCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("VideoTableCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("VideoTableCell");

		private static String BUTTON_IMAGE_SELECTED = "checkmark1.png";
		private VideoRecord _data;

		public VideoTableCell() : base()
		{
		}

		public VideoTableCell (IntPtr handle) : base (handle)
		{
		}

		public static VideoTableCell Create ()
		{
			return (VideoTableCell)Nib.Instantiate (null, null) [0];
		}

		public void UpdateWithData(VideoRecord data)
		{
			_data = data;

			_txtVideoName.Text = data.name;
			_txtDescription.Text = data.description;
			_txtCategory.Text = Global.GetCategoryName(data.type);
			_txtCategory.TextColor = GetColorWithCategory (data.type);

			_btnCheck.SetImage (UIImage.FromFile (BUTTON_IMAGE_SELECTED), UIControlState.Selected);
			_imgSelected.Hidden = true;
			_imgSnapshot.Image = UIImage.FromFile ("./snapshots/" + data.snapshot);

			if (Global.selectController.getNumHours () <= 0)
				_btnCheck.Hidden = true;

			String selectionState = Global.selectController.GetImageSelectionState (data.itemId);
			if (String.IsNullOrEmpty (selectionState) == false) {
				_btnCheck.Selected = true;
				_imgSelected.Hidden = false;
				_imgSelected.Image = UIImage.FromFile (selectionState);
			}
		}

		#region BUTTON_EVENTS
		partial void btnCheckClicked (Foundation.NSObject sender)
		{
			if (_btnCheck.Selected == true)
			{
				Global.selectController.removeItemFromSelList(_data.itemId);
				_btnCheck.Selected = false;
				_imgSelected.Hidden = true;
			}
			else
			{
				String selImageName = Global.selectController.addItemToSelList(_data.itemId);
				if (selImageName.Equals("") == false)
				{
					_btnCheck.Selected = true;
					_imgSelected.Hidden = false;
					_imgSelected.Image = UIImage.FromFile(selImageName);
				}
			}
			//_btnCheck.Selected = !_btnCheck.Selected;
		}

		partial void btnStartPlayClicked (Foundation.NSObject sender)
		{
			List<String> playList = new List<String>();
			playList.Add(_data.video);
			Global.mainController.startVideoPlay(playList);
		}
		#endregion

		#region PRIVATE METHODS
		private UIColor GetColorWithCategory(int type)
		{
			UIColor txtColor = UIColor.Black;
			switch (type) {
			case 1:
				txtColor = UIColor.FromRGB(237, 120, 117);
				break;
			case 2:
				txtColor = UIColor.FromRGB(251, 179, 141);
				break;
			case 3:
				txtColor = UIColor.FromRGB(87, 202, 171);
				break;
			case 4:
				txtColor = UIColor.FromRGB(133, 186, 236);
				break;
			}

			return txtColor;
		}
		#endregion
	}
}


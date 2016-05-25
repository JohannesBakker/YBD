using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;

namespace YBD
{
	public class VideoTableDataSource : UITableViewSource
	{
		private List<VideoRecord> _testData = new List<VideoRecord>();
		private int _category = -1;

		public VideoTableDataSource (int category)
		{
			_category = category;

			int selCategory = 0;
			if (category >= Global.VIDEO_TYPE_WARMUP && category <= Global.VIDEO_TYPE_CALMING)
				selCategory = category + 1;

			_testData = Global.dbMgr.getVideoList (selCategory);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			VideoTableCell cell = tableView.DequeueReusableCell ("VideoTableCell") as VideoTableCell;
			//if (cell == null) {
				var views = NSBundle.MainBundle.LoadNib ("VideoTableCell", tableView, null);
				cell = Runtime.GetNSObject (views.ValueAt (0)) as VideoTableCell;
			//}

			cell.UpdateWithData (_testData [indexPath.Row]);

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return _testData.Count;
		}
	}
}


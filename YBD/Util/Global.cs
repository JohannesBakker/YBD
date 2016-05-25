using System;

namespace YBD
{
	public class Global
	{
		public const int TAB_YOURFLOW = 0;
		public const int TAB_EXPLORE = 1;
		public const int TAB_VIDEOLIST = 2;
		public const int TAB_QUICKSTART = 3;
		public const int TAB_ABOUTUS = 4;
		public const int TAB_CONTACTUS = 5;

		public const String TAB_SELECTED_IMGNAME = "tabselectedrollover1.png";

		public const String SEL_VIDEO_BG_TYPE_WARMUP = "warmupslideout1.png";
		public const String SEL_VIDEO_BG_TYPE_STRENGTH = "strengthslideout1.png";
		public const String SEL_VIDEO_BG_TYPE_FOCUS = "focusslideout1.png";
		public const String SEL_VIDEO_BG_TYPE_CALMING = "calmingslideout1.png";

		public const String SEL_VIDEO_BG_LEN_FIFTEEN = "flowslideoutfifteen1.png";
		public const String SEL_VIDEO_BG_LEN_THIRTY = "flowslideoutthirty1.png";
		public const String SEL_VIDEO_BG_LEN_FOURTYFIVE = "flowslideoutfortyfive1.png";
		public const String SEL_VIDEO_BG_LEN_ONEHOUR = "flowslideouthour1.png";

		public const String ENTERFLOW_BUTTON_OFF_IMGNAME = "donebuttonoff1.png";
		public const String ENTERFLOW_EDITTEXT_ON_IMGNAME = "enterflownametextboxclicked1.png";
		public const String ENTERFLOW_EDITTEXT_OFF_IMGNAME = "enterflownametextboxunclicked1.png";

		public const int VIDEO_TYPE_WARMUP = 0;
		public const int VIDEO_TYPE_STRENGTH = 1;
		public const int VIDEO_TYPE_FOCUS = 2;
		public const int VIDEO_TYPE_CALMING = 3;

		public const int VIDEO_LENTYPE_FIFTEEN = 4;
		public const int VIDEO_LENTYPE_THIRTY = 5;
		public const int VIDEO_LENTYPE_FOURTYFIVE = 6;
		public const int VIDEO_LENTYPE_ONEHOUR = 7;

		public const int VIDEO_FAV_FIRST = 8;
		public const int VIDEO_FAV_SECOND = 9;
		public const int VIDEO_FAV_THIRD = 10;
		public const int VIDEO_FAV_FOURTH = 11;

		public const int NUM_SELECT_MAX = 4;
		public const int NUM_FAVORITE_MAX = 4;

		public const String DATABASE_FILENAME = "ybd.sqlite";
		public const String DOCUMENT_DB_FILENAME = "/document/ybd.sqlite";
		public const String VERSION_FILENAME = "version.txt";
		public const int CURRENT_APP_VERSION = 1;

		public static DatabaseMgr dbMgr = null;
		public static FavoriteMgr favMgr = null;
		public static YBDViewController mainController = null;
		public static SelectVideoViewController selectController = null;
		public static StartVideoViewController startController = null;

		public static MainViewController flowController = null;
		public static CategoryViewController categoryController = null;
		public static StartVideoViewController startVideoController = null;
		public static bool bEditingFavorite = false;

		public static string GetCategoryName(int type)
		{
			string category = "";

			switch (type) {
			case 1:
				category = "WARM-UP";
				break;
			case 2:
				category = "STRENGTH";
				break;
			case 3:
				category = "FOCUS";
				break;
			case 4:
				category = "CALMING";
				break;
			}

			return category;
		}
	}
}


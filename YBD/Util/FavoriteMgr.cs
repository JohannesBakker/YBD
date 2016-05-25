using System;
using System.Collections.Generic;

namespace YBD
{
	public class FavoriteMgr
	{
		int[] favIdList = new int[Global.NUM_FAVORITE_MAX];

		public FavoriteMgr ()
		{
			Global.favMgr = this;
		}

		#region PUBLIC METHODS
		public void initialize()
		{
			for (int i = 0; i < Global.NUM_FAVORITE_MAX; i++) {
				FavoriteItem favItem = Global.dbMgr.getFavoriteItem (i);
				if (favItem.name.Equals ("") == true) {
					favIdList [i] = -1;
				} else {
					favIdList [i] = i;
				}
			}
		}

		public FavoriteItem getFavoriteFromId(int id)
		{
			if (favIdList [id] == -1)
				return null;

			return Global.dbMgr.getFavoriteItem (id);
		}

		public int getNewFavId()
		{
			for (int i = 0; i < Global.NUM_FAVORITE_MAX; i++) {
				if (favIdList [i] == -1)
					return i;
			}

			return -1;
		}

		public void addNewFavorite(int id, String name)
		{
			if (id == -1)
				return;

			favIdList [id] = id;
			Global.dbMgr.addNewFavorite (id, name);
		}

		public void addVideoIdToFavItem(int favid, int videoid)
		{
			Global.dbMgr.addIdToFavorite (favid, videoid);
		}

		public void removeFavorite(int favid)
		{
			favIdList [favid] = -1;
			Global.dbMgr.removeFavFromTable (favid);
		}
		#endregion

		#region STATIC METHODS
		public static String getFavoriteImgName(int id)
		{
			switch (id)
			{
			case 0:
				return "favoriteboxfilledblue1.png";
				break;
			case 1:
				return "favoriteboxfilledgreen1.png";
				break;
			case 2:
				return "favoriteboxfilledorange1.png";
				break;
			case 3:
				return "favoriteboxfilledred1.png";
				break;
			default:
				break;
			}

			return "";
		}
		#endregion
	}
}


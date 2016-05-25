using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace YBD
{
	public class VideoRecord
	{
		[PrimaryKey, AutoIncrement]
		public int itemId { get; set; }

		[MaxLength(128)]
		public String name { get; set; }

		public int type { get; set; }

		[MaxLength(512)]
		public String description { get; set; }

		[MaxLength(128)]
		public String video { get; set; }

		[MaxLength(128)]
		public String snapshot { get; set; }
	}

	public class FavoriteRecord
	{
		[PrimaryKey, AutoIncrement]
		public int itemId { get; set; }

		public int favId { get; set; }
		public int videoId { get; set; }
	}

	public class FavoriteCategory
	{
		[PrimaryKey, AutoIncrement]
		public int itemId { get; set; }

		public int favId{ get; set; }

		[MaxLength(256)]
		public String favName { get; set; }
	}

	public class FavoriteItem
	{
		public int itemId = -1;
		public int itemCount = 0;
		public String name = "";
		public int[] itemArr = new int[Global.NUM_SELECT_MAX];
	}

	public class DatabaseMgr : SQLiteConnection
	{
		public DatabaseMgr(String path) : base(path)
		{
			Global.dbMgr = this;
		}

		#region PRIVATE FUNCTIONS
		private IEnumerable<VideoRecord> QueryVideos (int type)
		{
			if (type != 0)
				return	from s in Table<VideoRecord> ()
					where s.type == type
					select s;
			else
				return	from s in Table<VideoRecord> ()
					orderby s.type, s.name ascending
					select s;
		}

		private IEnumerable<FavoriteRecord> QueryFavorites (int index)
		{
			return	from s in Table<FavoriteRecord> ()
					where s.favId == index
					select s;
		}

		private String GetFavoriteName(int favid)
		{
			FavoriteCategory category = (from s in Table<FavoriteCategory> ()
				where s.favId == favid
				select s).FirstOrDefault ();

			if (category == null)
				return "";

			return category.favName;
		}

		private VideoRecord QueryVideo (int id)
		{
			return	(from s in Table<VideoRecord> ()
				where s.itemId == id
				select s).FirstOrDefault ();
		}

		private void DeleteFavorite(int index)
		{
			var remove = (from s in Table<FavoriteRecord>() 
			where s.favId == index
			select s); 

			if(remove != null)
			{
				List<FavoriteRecord> recordList = remove.ToList ();
				for (int i = 0; i < recordList.Count; i++)
					Delete<FavoriteRecord> (recordList [i].itemId);
			}

			var removeCategory = (from s in Table<FavoriteCategory> ()
			                      where s.favId == index
			                      select s);

			if (removeCategory != null) {
				List<FavoriteCategory> recordList = removeCategory.ToList ();
				for (int i = 0; i < recordList.Count; i++)
					Delete<FavoriteCategory> (recordList [i].itemId);
			}
		}

		private void InsertFavorite(int favid, int videoid)
		{
			FavoriteRecord newItem = new FavoriteRecord { favId = favid, videoId = videoid };
			Insert (newItem);
		}

		private void InsertFavCategory(int favid, String favname)
		{
			FavoriteCategory newItem = new FavoriteCategory { favId = favid, favName = favname };
			Insert (newItem);
		}

		#endregion

		#region EXPORT FUNCTIONS
		public FavoriteItem getFavoriteItem(int nIndex)
		{
			FavoriteItem resultData = new FavoriteItem ();
			resultData.itemId = nIndex;
			resultData.name = GetFavoriteName(nIndex);

			for (int i = 0; i < Global.NUM_SELECT_MAX; i++) {
				resultData.itemArr [i] = -1;
			}

			// Get Data from database
			List<FavoriteRecord> retList = QueryFavorites (nIndex).ToList();
			for (int i = 0; i < retList.Count; i++)
				resultData.itemArr [i] = retList [i].videoId;

			return resultData;
		}

		public String getVideoFromId(int id)
		{
			String videoName = "";

			// Get Data from database
			VideoRecord record = QueryVideo (id);
			if (record != null) {
				videoName = record.video;
			}

			return videoName;
		}

		public void removeFavFromTable(int navId)
		{
			DeleteFavorite (navId);
		}

		public void addNewFavorite(int navId, String favName)
		{
			InsertFavCategory (navId, favName);
		}

		public void addIdToFavorite(int navId, int videoId)
		{
			InsertFavorite (navId, videoId);
		}

		public List<VideoRecord> getVideoList(int type)
		{
			// Get Video List
			List<VideoRecord> resultList = QueryVideos (type).ToList();

			return resultList;
		}
		#endregion
	}
}


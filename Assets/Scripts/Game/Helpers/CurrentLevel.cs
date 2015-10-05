using UnityEngine;

namespace Ph.Bouncer 
{
	public static class CurrentLevel
	{
		private static int currentLevelIndex = -1;

		public static int GetIndex()
		{
			if(currentLevelIndex >= 0)
				return currentLevelIndex;

			Set(Application.loadedLevelName);

			Debug.Log("CurrentLevelIndex set to " + currentLevelIndex);
			return currentLevelIndex;
		}

		public static int GetNumber()
		{
			return GetIndex() + 1;
		}

		public static string GetName()
		{
			return LevelOrderHelper.GetLevelNameByIndex(GetIndex());
		}

		public static void Set(string levelName)
		{
			int levelIndex = LevelOrderHelper.GetLevelIndexByName(GetLevelNameWithoutAspect(levelName));
			
			if(levelIndex >= 0)
				currentLevelIndex = levelIndex;
		}
		
		public static void Set(int levelNumber)
		{
			currentLevelIndex = levelNumber - 1;
		}

		public static void Increment()
		{
			currentLevelIndex++;
		}

		public static string GetLevelNameWithoutAspect(string levelName)
		{
			if(levelName.Contains("-")) // Bit of a rubbish check but will do for now
				return levelName.Split('-')[0];
			
			return levelName;
		}
	}

}

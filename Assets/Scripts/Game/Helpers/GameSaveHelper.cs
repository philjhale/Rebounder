using UnityEngine;

namespace Ph.Bouncer
{
	public static class GameSaveHelper
	{
		public static int GetCurrentLevel()
		{
			int currentLevel = PlayerPrefs.GetInt(GameSaveKeys.CurrentLevel);

			if(currentLevel <= 0)
				return 1;

			return currentLevel;
		}

		public static void SetCurrentLevel(int currentLevel)
		{
			SetInt(GameSaveKeys.CurrentLevel, currentLevel);
		}


		public static bool GetAllLevelsComplete()
		{
			return GetBool(GameSaveKeys.AllLevelsComplete);
		}
		
		public static void SetAllLevelsComplete(bool allLevelsComplete)
		{
			SetBool(GameSaveKeys.AllLevelsComplete, allLevelsComplete);
		}


		public static bool GetFirstLaunch()
		{
			return GetBool(GameSaveKeys.GameFirstLaunch);
		}
		
		public static void SetFirstLaunch()
		{
			SetBool(GameSaveKeys.GameFirstLaunch, true);
		}

		// Using JSON would have been nicer here but I'm not too bothered
		public static bool GetLevelFirstLaunch(string paddedLevelNumber)
		{
			return GetBool(GameSaveKeys.LevelFirstLaunch + paddedLevelNumber);
		}
		
		public static void SetLevelFirstLaunch(string paddedLevelNumber)
		{
			SetBool(GameSaveKeys.LevelFirstLaunch + paddedLevelNumber, true);
		}

		public static bool GetHasSeenHowToPlay()
		{
			return GetBool(GameSaveKeys.HasSeenHowToPlay);
		}
		
		public static void SetHasSeenHowToPlay()
		{
			SetBool(GameSaveKeys.HasSeenHowToPlay, true);
		}



		private static bool GetBool(string key)
		{
			return PlayerPrefs.GetInt(key) > 0;
		}

		private static void SetBool(string key, bool value)
		{
			// TODO Handle exceptions?
			OutputDebug.Format("Saved key '{0}' with value '{1}'", key, value);
			PlayerPrefs.SetInt(key, value ? 1 : 0);
			PlayerPrefs.Save();
		}

		private static void SetInt(string key, int value)
		{
			// TODO Handle exceptions?
			OutputDebug.Format("Saved key '{0}' with value '{1}'", key, value);
			PlayerPrefs.SetInt(key, value);
			PlayerPrefs.Save();
		}
	}
}


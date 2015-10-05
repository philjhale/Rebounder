using UnityEngine;
using System.Collections;
using System.Linq;
using System;

namespace Ph.Bouncer
{
	public static class LevelLoadHelper 
	{
		public static void Load(int levelNumber)
		{
			CurrentLevel.Set(levelNumber);

			if(LoadAllLevelsCompleteIfNecessary()) return;

			Load(LevelOrderHelper.GetLevelNameByIndex(levelNumber-1));
		}

		public static void Load(string levelName)
		{
			string levelNameMinusRatio = CurrentLevel.GetLevelNameWithoutAspect(levelName);
			CurrentLevel.Set(levelNameMinusRatio);

			if(LoadAllLevelsCompleteIfNecessary()) return;

			string levelNameWithRatio = string.Format("{0}-{1}", levelNameMinusRatio, AspectRatioHelper.GetAspectRatioString());

			string levelNameToLoad = LevelRequiresAspectRatioSpecificScenes(levelNameMinusRatio) ? levelNameWithRatio : levelNameMinusRatio;

			GAHelper.LevelLoaded(CurrentLevel.GetNumber(), levelNameMinusRatio, levelNameWithRatio);
			GAHelper.LevelFirstLoaded(levelNameMinusRatio, CurrentLevel.GetNumber());

			OutputDebug.Format("Loading level {0}", levelNameToLoad);
			Application.LoadLevel(levelNameToLoad);
		}

		public static void Reload()
		{
			Load(Application.loadedLevelName);
		}

		public static void NextLevel()
		{
			CurrentLevel.Increment();

			if(LoadAllLevelsCompleteIfNecessary()) return;

			GameSaveHelper.SetAllLevelsComplete(false);

			LevelLoadHelper.Load(CurrentLevel.GetName());
		}

		public static void LoadTitleScreen()
		{
			Application.LoadLevel("TitleScreen");
		}

		public static void LoadCreditsScreen()
		{
			Application.LoadLevel("Credits");
		}

		public static void LoadAllLevelsCompleteScreen()
		{
			Application.LoadLevel("CompletedAllLevels");
		}

		public static void LoadHowToPlayScreen()
		{
			Application.LoadLevel("HowToPlay");
		}

		public static void LoadResolutionTest()
		{
			Application.LoadLevel("ResolutionTest");
		}



		private static bool HasCompletedAllLevels()
		{
			OutputDebug.Format("HasCompletedAllLevels - Current {0}, Total {1}", CurrentLevel.GetNumber(), LevelOrderHelper.GetNumberOfLevels());
			return CurrentLevel.GetNumber() > LevelOrderHelper.GetNumberOfLevels();
		}

		private static bool LoadAllLevelsCompleteIfNecessary()
		{
			bool hasCompletedAllLevels = HasCompletedAllLevels();

			if(hasCompletedAllLevels) LoadAllLevelsCompleteScreen();

			return hasCompletedAllLevels;
		}
		
		private static bool LevelRequiresAspectRatioSpecificScenes(string levelName)
		{
			Level level = LevelOrderHelper.GetLevelByName(levelName);
			
			if(level == null)
				throw new InvalidOperationException(string.Format("Parameter levelName with value '{0}' does not exist", levelName));
				
			return level.DoesRequireAspectRatioSpecificScenes();
		}
	}
}

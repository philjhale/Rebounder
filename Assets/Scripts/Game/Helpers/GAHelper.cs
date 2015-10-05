using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	// http://support.gameanalytics.com/hc/en-us/articles/200841636-Creating-meaningful-custom-events
	// This stuff is tracked by default http://support.gameanalytics.com/hc/en-us/articles/200841006-Core-GameAnalytics-metrics
	// Value param http://support.gameanalytics.com/hc/en-us/articles/200841506-Design-event-structure
	public static class GAHelper
	{
		// ************ Levels

		// On which level do players stop playing? Where is there a difficulty spike?
		public static void LevelFirstLoaded(string levelNameWithoutAspect, int levelNumber)
		{
			string paddedLevelNumber = GetPaddedLevelNumber(levelNumber);

			if(!GameSaveHelper.GetLevelFirstLaunch(paddedLevelNumber))
			{
				GA.API.Design.NewEvent(string.Format("Level:LevelFirstLoaded:{0}:{1}", paddedLevelNumber, levelNameWithoutAspect));
				GameSaveHelper.SetLevelFirstLaunch(paddedLevelNumber);
			}
		}

		// How many times was a level attempted? Which levels are the most difficult?
		public static void LevelLoaded(int levelNumber, string levelNameMinusAspect, string levelNameWithAspect)
		{
			GA.API.Design.NewEvent(string.Format("Level:Loaded:{0}:{1}:{2}", GetPaddedLevelNumber(levelNumber), levelNameMinusAspect, levelNameWithAspect));
		}

		// Which levels to people end up in a mess? Does the level need a redesign?
		public static void LevelRestarted()
		{
			GA.API.Design.NewEvent(GetGenericLevelEventString("Restarted"));
		}

		// Do people know you can tap the launcher? What levels does it occur more times?
		public static void LevelLauncherShortTap()
		{
			GA.API.Design.NewEvent(GetGenericLevelEventString("LauncherShortTap"));
		}

		public static void LevelLauncherLongTap()
		{
			GA.API.Design.NewEvent(GetGenericLevelEventString("LauncherLongTap"));
		}

		// How many lines are drawn when completing levels?
		public static void LevelLineCreated(Colour colour)
		{
			GA.API.Design.NewEvent(GetLineEventString("LineCreated", colour));
		}
		
		// How many lines are deletd when completing levels?
		public static void LevelLineDeleted(Colour colour)
		{
			GA.API.Design.NewEvent(GetLineEventString("LineDeleted", colour));
		}

		private static string GetLineEventString(string eventName, Colour colour)
		{
			string paddedLevelNumber = GetPaddedLevelNumber(CurrentLevel.GetNumber());

			return string.Format("Level:{0}:{1}:{2}", eventName, paddedLevelNumber, colour);
		}


		// ************ Levels
		public static void GameFirstLaunch()
		{
			if(!GameSaveHelper.GetFirstLaunch())
			{
				GA.API.Design.NewEvent("FirstLaunch");
				GameSaveHelper.SetFirstLaunch();
			}
		}

		private static string GetPaddedLevelNumber(int levelNumber)
		{
			return levelNumber.ToString().PadLeft(3, '0');
		}


		/// <returns>Level:{eventName}:{paddedLevelNumber}:{levelNameMinusAspect}:{levelNameWithAspect}</returns>
		private static string GetGenericLevelEventString(string eventName)
		{
			string paddedLevelNumber = GetPaddedLevelNumber(CurrentLevel.GetNumber());
			string levelNameMinusAspect = CurrentLevel.GetName();
			// TODO Probably should move this logic to one place
			string levelNameWithAspect = string.Format("{0}-{1}", levelNameMinusAspect, AspectRatioHelper.GetAspectRatioString());

			return string.Format("Level:{0}:{1}:{2}:{3}", eventName, paddedLevelNumber, levelNameMinusAspect, levelNameWithAspect);
		}
	}
}

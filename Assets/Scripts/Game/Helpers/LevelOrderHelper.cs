using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Ph.Bouncer
{
	public static class LevelOrderHelper 
	{
		private static List<Level> levels;
		
		public static List<Level> GetLevels()
		{
			if(levels != null && levels.Count > 0)
				return levels;
			
			levels = new List<Level>();


			// Beginner levels
			Add(new Level("Scene1"));
			Add(new Level("Scene2a"));
			Add(new Level("Scene2"));
			Add(new Level("Scene4"));
			Add(new Level("TheSpiral"));
			Add(new Level("Scene3").SetRequiresAspectRatioSpecificScenes());
			Add(new Level("Scene5").SetRequiresAspectRatioSpecificScenes());
			Add(new Level("ThreeVerticalLines"));
			Add(new Level("UseTheObstacle"));
			// Get a bit more interesting
			Add(new Level("FillInTheBlanks")); // 10








			// Introduce second target
			Add(new Level("FirstTwoTarget"));
			Add(new Level("SecondTwoTarget"));
			Add(new Level("Diagonal"));
			Add(new Level("CrossingStreams"));
			Add(new Level("LeftAndDown")); // 15




			// Introduce teleporter
			Add(new Level("FirstTeleporter"));
			Add(new Level("OneUpOneDown"));
			Add(new Level("OneDownOneUp"));
			Add(new Level("OtherSideOfTheWall"));
			Add(new Level("BackDoor")); // 20




			// Introduce colour changer
			Add(new Level("FirstColourChanger"));
			Add(new Level("HorizontalColourTunnel"));
			Add(new Level("ColourTunnel"));
			Add(new Level("AvoidTheALine"));
			Add(new Level("ColourCross")); //25




			// Introduce coloured lines
			Add(new Level("FirstColouredLine"));
			Add(new Level("SecondColouredLine"));
			Add(new Level("Opposites"));
			Add(new Level("TwoColouredLines"));
			Add(new Level("ColouredLineFive"));


			Add(new Level("DownUpDownUp1")); // 31
			Add(new Level("TwoUprights")); // Two target 2
			Add(new Level("DownAndAround").SetRequiresAspectRatioSpecificScenes()); // Two 2
			Add(new Level("BackDoor2")); // Teleporter 2
			Add(new Level("DownUpDownUp2")); // One 3

			Add(new Level("UseTheBalls")); // 36 Two target 2
			Add(new Level("CrossCollision").SetRequiresAspectRatioSpecificScenes()); // Two 2
			Add(new Level("LargeGap")); // Two target 2
			Add(new Level("ThreeVerticalColouredLines")); // Coloured line 2
			Add(new Level("GiantEye").SetRequiresAspectRatioSpecificScenes()); // Two target 3

			Add(new Level("LevelColumns")); // 41 One target 2
			Add(new Level("FillTheGap")); // One 2
			Add(new Level("Splits")); // Two target 2
			Add(new Level("AwkwardAngle")); // Teleporter 2
			Add(new Level("TheX")); // Two target 3

			Add(new Level("RandomColumns")); // 46 One 2
			Add(new Level("InTheCorner")); // Two target 2
			Add(new Level("ThroughTheGap")); // Coloured line 2
			Add(new Level("TwoGaps")); // Colour changer 2
			Add(new Level("UseTheObstacle2")); // One 3

			Add(new Level("CrossOver")); // 51 Two target 2
			Add(new Level("BounceTarget")); // Two target 2 (need intro)
			Add(new Level("TargetBounceCross").SetRequiresAspectRatioSpecificScenes()); // Two target (bounce off target) 3
			Add(new Level("ThreeDiagonalLines")); // Coloured line 2
			Add(new Level("ParallelSlant")); // One 3

			Add(new Level("ThreeShootersUp")); // 56 Colour 2
			Add(new Level("TeleBounce").SetRequiresAspectRatioSpecificScenes()); // Teleporter 2
			Add(new Level("Roundabout")); // Teleporter 2
			Add(new Level("DownAndAround2").SetRequiresAspectRatioSpecificScenes()); // Two 3
			Add(new Level("SmallT")); // Two target 3

			// Not using just now
			//Add(new Level("UpDraft")); // One 2
			//Add(new Level("OverTheEdge"));

			return levels;
		}

		private static void Add(Level newLevel)
		{
			if(levels.Any(x => x.Name.ToLower() == newLevel.Name.ToLower()))
				throw new InvalidOperationException(string.Format("Attempting to add level {0} which already exists in the levels list", newLevel.Name));

			levels.Add(newLevel);
		}

		public static int GetNumberOfLevels()
		{
			return GetLevels().Count;
		}

		public static string GetLevelNameByIndex(int index)
		{
			return GetLevels()[index].Name;
		}

		public static int GetLevelIndexByName(string name)
		{
			return GetLevels().FindIndex(x => x.Name.ToLower() == name.ToLower());
		}

		public static Level GetLevelByName(string name)
		{
			return GetLevels().Where(x => x.Name.ToLower() == name.ToLower()).SingleOrDefault();
		}
	}
}
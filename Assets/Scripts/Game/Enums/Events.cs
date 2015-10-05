using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public static class Events 
	{
		public static string TargetHit = "TargetHit";
		public static string TargetDrain = "TargetDrain";
		public static string LevelComplete = "LevelComplete";
		
		// Lines
		public static string LineManipulationStarted = "LineManipulationStarted";
		public static string LineManipulationFinished = "LineManipulationFinished";
		public static string LineDrawn = "LineDrawn";
		public static string LineDeleted = "LineDeleted";
		
		// GUI
		public static string ColourSelected = "ColourSelected";

		// Pausing
		public static string GamePaused = "GamePaused";
		public static string GameResumed = "GameResumed";

		public static string OpenDebugMenu = "OpenDebugMenus";
		public static string CloseDebugMenu = "CloseDebugMenus";
	}
}

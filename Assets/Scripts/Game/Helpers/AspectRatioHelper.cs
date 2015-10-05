using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ph.Bouncer
{
	public static class AspectRatioHelper 
	{
		public static float aspect = -1;

		public static float GetAspectRatio()
		{
			Camera firstCamera = Camera.main ?? Camera.allCameras[0];

			if(aspect < 0)
				aspect = firstCamera.aspect;

			return aspect;
		}

		public static string GetAspectRatioString()
		{
			double currentAspectRatio = Math.Round(GetAspectRatio(), 2);

			var supportedAspectRatios = GetSupportedAspectRatios();

			AspectRatio nearestAspectRatio = supportedAspectRatios
				.Select(p => new { Value = p, Difference = Math.Abs(p.Aspect - currentAspectRatio) })
				.OrderBy(p => p.Difference)
				.First().Value;

			return nearestAspectRatio.AspectString;
		}

		public static bool IsCurrentSceneCorrectAspect()
		{
			if(!Application.loadedLevelName.Contains("-"))
				return true; // There is no aspect specific version of the level

			string currentSceneAspect = Application.loadedLevelName.Split('-')[1];

			return currentSceneAspect == GetAspectRatioString();
		}

		private static List<AspectRatio> GetSupportedAspectRatios()
		{
			var aspects = new List<AspectRatio>();
			aspects.Add(new AspectRatio(1.78f, "16by9"));
            aspects.Add(new AspectRatio(1.5f, "3by2"));
			return aspects;
		}
	}
}

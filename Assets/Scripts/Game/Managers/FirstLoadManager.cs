using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class FirstLoadManager : MonoBehaviour 
	{	
		// SetReferenceAtlas
		void Awake() 
		{
			Set2dToolkitResolution();

			GAHelper.GameFirstLaunch();

			LevelLoadHelper.LoadTitleScreen();
		}

		static void Set2dToolkitResolution()
		{
			ResolutionSize size = ResolutionHelper.GetSize();

			string currentPlatform;

			switch (size) 
			{
				case ResolutionSize.One:
					currentPlatform = "1x";
					break;
				case ResolutionSize.Two:
					currentPlatform = "2x";
					break;
				default:
					currentPlatform = "4x";
					break;
			}

			tk2dSystem.CurrentPlatform = currentPlatform;
			OutputDebug.Format("2dtk current platform = {0}", tk2dSystem.CurrentPlatform);
		}
	}
}
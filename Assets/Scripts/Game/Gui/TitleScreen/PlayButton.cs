using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class PlayButton : MonoBehaviour 
	{
		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				if(GameSaveHelper.GetHasSeenHowToPlay())
				{
					LevelLoadHelper.Load(GameSaveHelper.GetCurrentLevel());
				}
				else
				{
					LevelLoadHelper.LoadHowToPlayScreen();
					GameSaveHelper.SetHasSeenHowToPlay();
				}
			}
		}
	}
}
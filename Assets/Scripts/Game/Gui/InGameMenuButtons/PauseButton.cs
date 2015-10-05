using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class PauseButton : MonoBehaviour
	{
		private float pressStartTime;
		private bool isLevelComplete;

		void OnPress(bool isDown)
		{
			if (isLevelComplete)
				return;

			if(isDown && pressStartTime == 0)
				pressStartTime = Time.time;

			if(!isDown && Input.GetMouseButtonUp(0)) 
			{
				if(ShouldOpenDebugMenu())
					Messenger.Broadcast(Events.OpenDebugMenu);
				else
					Messenger.Broadcast(Events.GamePaused);

				pressStartTime = 0;
			}
		}

		private const float DEBUG_MENU_HOLD_TIME_SECONDS = 10;

		private bool ShouldOpenDebugMenu()
		{
			return Time.time - pressStartTime > DEBUG_MENU_HOLD_TIME_SECONDS;
		}

		public void OnLevelComplete()
		{
			isLevelComplete = true;
		}
		
		void OnEnable()
		{
			Messenger.AddListener(Events.LevelComplete, OnLevelComplete);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.LevelComplete, OnLevelComplete);
		}
	}
}
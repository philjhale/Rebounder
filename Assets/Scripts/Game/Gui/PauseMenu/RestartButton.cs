using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class RestartButton : MonoBehaviour 
	{
		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				GAHelper.LevelRestarted();
				LevelLoadHelper.Reload();
				Messenger.Broadcast(Events.GameResumed);
			}
		}
	}
}
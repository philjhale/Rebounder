using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class ResumeButton : MonoBehaviour 
	{
		void OnPress(bool isDown)
		{
			if(!isDown)
				Messenger.Broadcast(Events.GameResumed);
		}
	}
}
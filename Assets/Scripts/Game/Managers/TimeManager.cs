using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class TimeManager : MonoBehaviour 
	{
		public void OnPause()
		{
			Time.timeScale = 0;
		}
		
		public void OnResume()
		{
			Time.timeScale = 1;
		}
		
		void OnEnable()
		{
			Messenger.AddListener(Events.GamePaused, OnPause);
			Messenger.AddListener(Events.GameResumed, OnResume);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.GamePaused, OnPause);
			Messenger.RemoveListener(Events.GameResumed, OnResume);
		}
	}
}
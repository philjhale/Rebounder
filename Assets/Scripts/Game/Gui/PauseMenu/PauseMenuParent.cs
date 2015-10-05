using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	// Script must be on parent which is always active. Events aren't received on inactive objects
	public class PauseMenuParent : MonoBehaviour 
	{
		public void OnPause()
		{
			NGUITools.SetActiveChildren(gameObject, true);	
		}
		
		public void OnResume()
		{
			NGUITools.SetActiveChildren(gameObject, false);	
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
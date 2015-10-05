using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class DebugMenu : MonoBehaviour 
	{
		public void OnOpenDebugMenu()
		{
			NGUITools.SetActiveChildren(gameObject, true);
		}

		public void OnCloseDebugMenu()
		{
			NGUITools.SetActiveChildren(gameObject, false);
		}
		
		void OnEnable()
		{
			Messenger.AddListener(Events.OpenDebugMenu, OnOpenDebugMenu);
			Messenger.AddListener(Events.CloseDebugMenu, OnCloseDebugMenu);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.OpenDebugMenu, OnOpenDebugMenu);
			Messenger.RemoveListener(Events.CloseDebugMenu, OnCloseDebugMenu);
		}
	}
}

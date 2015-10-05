using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class LevelCompleteMenu : MonoBehaviour 
	{
		public void OnLevelComplete()
		{
			NGUITools.SetActiveChildren(gameObject, true);
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
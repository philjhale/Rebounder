using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class GameProgressSaveManager : MonoBehaviour 
	{
		public void OnLevelComplete()
		{
			int nextLevelNumber = CurrentLevel.GetNumber() + 1;
			GameSaveHelper.SetCurrentLevel(nextLevelNumber);
			Debug.Log("Game saved: level " + nextLevelNumber);
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
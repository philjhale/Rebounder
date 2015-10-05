using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class CheckForNewLevels : MonoBehaviour 
	{
		public GameObject NewLevelsLabel;

		void Start () 
		{
			if(!AreNewLevelsAvailable())
			{
				NGUITools.SetActive(NewLevelsLabel, false);
			}
		}

		private bool AreNewLevelsAvailable()
		{
			OutputDebug.Format("GetAllLevelsComplete {0}, GetNumberOfLevels {1}, GetCurrentLevel {2}", GameSaveHelper.GetAllLevelsComplete(), LevelOrderHelper.GetNumberOfLevels(), GameSaveHelper.GetCurrentLevel()); 
			return GameSaveHelper.GetAllLevelsComplete() && LevelOrderHelper.GetNumberOfLevels() > GameSaveHelper.GetCurrentLevel();
		}
	}
}
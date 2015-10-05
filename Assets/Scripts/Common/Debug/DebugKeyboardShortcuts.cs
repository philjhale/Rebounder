using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class DebugKeyboardShortcuts : MonoBehaviour 
	{
		void Update () 
		{
			if(Input.GetKeyDown(KeyCode.N))
			   LevelLoadHelper.NextLevel();
			else if(Input.GetKeyDown(KeyCode.D))
			{
				PlayerPrefs.DeleteAll();
				PlayerPrefs.Save();
			}
		}
	}
}
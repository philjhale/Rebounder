using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class NextLevelButton : MonoBehaviour 
	{
		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				LevelLoadHelper.NextLevel();
			}
		}
	}
}
using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class BackButton : MonoBehaviour 
	{
		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				LevelLoadHelper.LoadTitleScreen();
			}
		}
	}
}
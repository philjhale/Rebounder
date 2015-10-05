using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class HomeButton : MonoBehaviour 
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
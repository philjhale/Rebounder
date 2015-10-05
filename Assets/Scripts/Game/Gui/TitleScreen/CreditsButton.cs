using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class CreditsButton : MonoBehaviour 
	{
		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				LevelLoadHelper.LoadCreditsScreen();
			}
		}
	}
}

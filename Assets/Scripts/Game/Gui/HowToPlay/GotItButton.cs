using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class GotItButton : MonoBehaviour 
	{
		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				LevelLoadHelper.Load(GameSaveHelper.GetCurrentLevel());
			}
		}
	}
}
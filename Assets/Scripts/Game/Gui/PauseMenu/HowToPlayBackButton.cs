using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class HowToPlayBackButton : MonoBehaviour 
	{
		public GameObject HowToPlayParent;

		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				NGUITools.SetActiveChildren(HowToPlayParent, false);	
			}
		}
	}
}
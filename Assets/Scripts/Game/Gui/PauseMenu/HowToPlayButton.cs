using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class HowToPlayButton : MonoBehaviour 
	{
		public GameObject HowToPlayParent;

		void OnPress(bool isDown)
		{
			if(!isDown)
			{
				NGUITools.SetActiveChildren(HowToPlayParent, true);	
			}
		}
	}
}
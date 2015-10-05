using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class HighlightButtonOnPress : MonoBehaviour 
	{
		public GameObject ButtonHighlight;

		void OnPress(bool isDown)
		{
			ButtonHighlight.SetActive(isDown);
		}
	}
}
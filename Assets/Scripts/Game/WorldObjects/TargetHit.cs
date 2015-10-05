using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class TargetHit : MonoBehaviour 
	{
		private tk2dSprite sprite;

		void Awake()
		{
			sprite = GetComponent<tk2dSprite>();
		}

		public void SetColour(Colour colour)
		{
			if(sprite != null)
				sprite.SetSprite("TargetHit" + colour.ToString());
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
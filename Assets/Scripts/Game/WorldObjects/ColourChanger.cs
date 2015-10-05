using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class ColourChanger : MonoBehaviour 
	{
		public Colour Colour;
		private tk2dSprite sprite;
		private tk2dSpriteAnimator spriteAnimator;
		
		void Start()
		{
			sprite = GetComponent<tk2dSprite>();
			spriteAnimator = GetComponent<tk2dSpriteAnimator>();
			SetColour(Colour);
		}
		
		void OnTriggerEnter(Collider other) 
		{
			var ballScript = other.collider.GetComponent<Ball>();
			
			if(!ballScript)
				return;
			
			ballScript.SetColour(Colour);
		}

		public void SetColour(Colour colour)
		{
			Colour = colour;

			// This doesn't work. Not sure why
			if(sprite != null)
				sprite.SetSprite("ColourChanger" + colour.ToString() + "F1");

			if(spriteAnimator != null)
				spriteAnimator.Play("ColourChanger" + colour.ToString());

		}
	}
}
using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class ChangeBackground : MonoBehaviour 
	{
		private tk2dTiledSprite sprite;

		void Start() 
		{
			sprite = GetComponent<tk2dTiledSprite>();
		}
		
		public void SetBackground(int number)
		{
			if(sprite == null)
				return;

			sprite.SetSprite("Background" + number);
		}
	}
}
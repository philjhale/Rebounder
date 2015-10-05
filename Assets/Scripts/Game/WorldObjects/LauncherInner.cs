using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class LauncherInner : MonoBehaviour 
	{
		private tk2dSprite sprite;
		private const float ANIMATION_FIRE_TIME = 0.05f;
		private const float ANIMATION_Y_DISTANCE = 0.7f;
		private float returnTime;
		private float delayTime;

		public void Fire(float secondsBetweenShots)
		{
			// Ensure animation always completes before next fire
			returnTime = (secondsBetweenShots - ANIMATION_FIRE_TIME) * 0.5f; 
			delayTime = returnTime * 0.5f;
			iTween.MoveAdd(gameObject, iTween.Hash("y", ANIMATION_Y_DISTANCE, "time", ANIMATION_FIRE_TIME, "easetype", iTween.EaseType.linear, "oncomplete", "Return"));
		}

		public void Return()
		{
			iTween.MoveAdd(gameObject, iTween.Hash("y", -ANIMATION_Y_DISTANCE, "delay", delayTime, "time", returnTime));
		}

		public void SetColour(Colour colour)
		{
			tk2dSprite sprite = GetComponent<tk2dSprite>();
			sprite.SetSprite("LauncherInner" + colour.ToString());
		}
	}
}
using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public static class InputHelper 
	{
		public static bool IsTouchDevice()
		{
			return Application.platform == RuntimePlatform.IPhonePlayer
				|| Application.platform == RuntimePlatform.Android;
		}
		
		public static bool IsFirstTouch()
		{
			if(IsTouchDevice())
			{
				return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
			}
			else
			{
				return Input.GetMouseButtonDown(0);
			}
		}
		
		public static bool IsTouchEnded()
		{
			if(IsTouchDevice())
			{
				return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
			}
			else
			{
				return Input.GetMouseButtonUp(0);
			}
		}	
	}
}

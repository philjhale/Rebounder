using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public static class AngleHelper
	{
		public static float AngleFromVertical(Vector2 angleTo)
		{
			float angle = Vector2.Angle(Vector2.up, angleTo);

			if(angleTo.x < 0)
				angle = 360 - angle;

			return angle;
		}
	}
}
using UnityEngine;

namespace Ph.Bouncer
{
	public static class Vector3Extensions
	{
		public static Vector3 SetX(this Vector3 vector, float x)
		{
			vector = new Vector3(x, vector.y, vector.z);
			return vector;
		}
	}
}


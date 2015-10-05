using UnityEngine;

namespace Ph.Bouncer
{
	public static class ColorExtensions
	{
		public static Color SetAlpha(this Color colour, float alpha)
		{
			colour.a = alpha;
			return colour;
		}

		public static Color SetAllColours(this Color colour, float value)
		{
			colour.r = value;
			colour.g = value;
			colour.b = value;
			return colour;
		}
	}
}


using UnityEngine;
using System.Collections;
using System;

namespace Ph.Bouncer
{
	public static class LineHelper
	{
		public static Line Create(Vector2 startPoint, Colour colour)
		{
			// This is a shit way of ensuring there is no graphical weirdness when lines overlap
			// causing by lines between on the same Z value
			System.Random rnd = new System.Random((int)(Time.time * 1000));
			float randomZ = (float)rnd.Next(0, 60) / 100f;
			Vector3 startPointWithRandomZ = new Vector3(startPoint.x, startPoint.y, randomZ * -1);

			GameObject testLineObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Line"), startPointWithRandomZ, Quaternion.identity);
			Line testLineScript = testLineObject.GetComponent<Line>();
			testLineScript.Colour = colour;
			return testLineScript;
		}

		public static int GetLayerForColour(Colour colour)
		{
			switch(colour)
			{
				case Colour.Purple:
					return (int)Layers.PurpleLine;
				case Colour.Green:
					return (int)Layers.GreenLine;
				case Colour.Blue:
					return (int)Layers.BlueLine;
			}

			return (int)Layers.Default;
		}
	}
}
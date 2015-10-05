using UnityEngine;
using System.Collections;
using System;

namespace Ph.Bouncer
{
	public static class GuiColours
	{	
		private static Color NormalRed = new Color(1f, 0.5f, 0.5f, 1f);
		private static Color NormalYellow = new Color(1f, 1f, 0.5f, 1f);
		private static Color NormalGreen = new Color(0.5f, 1f, 0.5f, 1f);
		private static Color NormalBlue = new Color(0.5f, 0.5f, 1f, 1f);
		private static Color NormalNone = new Color(0.2f, 0.2f, 0.2f);
		
		private const float hoverDifference = -0.3f;
		private const float pressDifference = -0.5f;
		
		public static Color GetNormal(Colour colour)
		{
			switch(colour)
			{
				case Colour.Orange: return NormalRed;
				case Colour.Purple: return NormalYellow;
				case Colour.Green: return NormalGreen;
				case Colour.Blue: return NormalBlue;
				case Colour.None: return NormalNone;
			}
			
			throw new InvalidOperationException(string.Format("Unknown colour - {0}", colour));
		}
		
		public static Color GetHover(Colour colour)
		{
			return GetNormal(colour) + GetColourDifference(colour, hoverDifference);
		}
		
		public static Color GetPressed(Colour colour)
		{
			return GetNormal(colour) + GetColourDifference(colour, hoverDifference);
		}
		
		private static Color GetColourDifference(Colour colour, float hoverOrPressColourDifference)
		{
			switch(colour)
			{
				case Colour.Orange: return new Color(0, hoverOrPressColourDifference, hoverOrPressColourDifference);
				case Colour.Purple: return new Color(0, 0, hoverOrPressColourDifference);
				case Colour.Green: return new Color(hoverOrPressColourDifference, 0, hoverOrPressColourDifference);
				case Colour.Blue: return new Color(hoverOrPressColourDifference, hoverOrPressColourDifference, 0);
				case Colour.None: return new Color(hoverOrPressColourDifference, hoverOrPressColourDifference, hoverOrPressColourDifference);
			}
			
			throw new InvalidOperationException(string.Format("Unknown colour - {0}", colour));
		}
	}
}
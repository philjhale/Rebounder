using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class LineCounts : MonoBehaviour
	{
		// Something in here breaks the prefab link. Not sure why
		public int StartingOrangeLines; // Will collide with everything. Should probably give a better name
		public int StartingBlueLines;
		public int StartingGreenLines;
		public int StartingPurpleLines;

		public int OrangeLines { get; private set; } // Will collide with everything. Should probably give a better name
		public int BlueLines { get; private set; }
		public int GreenLines { get; private set; }
		public int PurpleLines { get; private set; }

		void Awake()
		{
			OrangeLines = StartingOrangeLines;
			BlueLines = StartingBlueLines;
			GreenLines = StartingGreenLines;
			PurpleLines = StartingPurpleLines;
		}

		public bool HasLinesLeftToDraw()
		{
			return OrangeLines > 0 || BlueLines > 0 || GreenLines > 0 || PurpleLines > 0;	
		}

		public void Decrement(Colour colour)
		{
			//Debug.Log ("LineDrawn: " + (lineColour.HasValue ? lineColour.ToString() : "null"));
			if(colour == Colour.Purple)
				PurpleLines--;
			else if(colour == Colour.Green)
				GreenLines--;
			else if(colour == Colour.Blue)
				BlueLines--;
			else
				OrangeLines--;
		}

		public void Increment(Colour colour)
		{
			if(colour == Colour.Purple)
				PurpleLines++;
			else if(colour == Colour.Green)
				GreenLines++;
			else if(colour == Colour.Blue)
				BlueLines++;
			else
				OrangeLines++;
		}

		public int GetLineCountForColour(Colour lineColour)
		{
			switch(lineColour)
			{
			case Colour.Orange: return OrangeLines;
			case Colour.Blue: return BlueLines;	
			case Colour.Green: return GreenLines;	
			case Colour.Purple: return PurpleLines;	
			default: return 0;
			}
		}

		public bool IsLineCountGreaterThanZero(Colour colour)
		{
			switch(colour)
			{
			case Colour.Orange: return OrangeLines > 0;
			case Colour.Blue: return BlueLines > 0;	
			case Colour.Green: return GreenLines > 0;	
			case Colour.Purple: return PurpleLines > 0;	
			}
			
			return false;
		}

		public int NumberOfColoursAvailableAtStart()
		{
			int count = 0;

			if(StartingOrangeLines > 0)
				count++;
			if(StartingBlueLines > 0)
				count++;
			if(StartingGreenLines > 0)
				count++;
			if(StartingPurpleLines > 0)
				count++;

			return count;
		}
	}
}
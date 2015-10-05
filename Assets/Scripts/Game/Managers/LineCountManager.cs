using UnityEngine;
using System.Collections;
using System;

namespace Ph.Bouncer
{
	public class LineCountManager : MonoBehaviour 
	{	
		private Colour? playerSelectedNextColour = Colour.Orange;
		private LineCounts lineCounts;

		void Awake()
		{
			// TODO Null check?
			var levelScripts = GameObject.Find("LevelScripts");
			lineCounts = (LineCounts)levelScripts.GetComponent<LineCounts>();
		}

		public bool CanDrawAnotherLine()
		{
			return lineCounts.HasLinesLeftToDraw();
		}
		
		public void LineDrawn()
		{
			LineDrawn(Colour.Orange);
		}

		public void LineDrawn(Colour colour)
		{
			//Debug.Log ("LineDrawn: " + (lineColour.HasValue ? lineColour.ToString() : "null"));
			lineCounts.Decrement(colour);

			Messenger<Colour, int>.Broadcast(Events.LineDrawn, colour, GetLineCountForColour(colour));
			GAHelper.LevelLineCreated(colour);
		}
	
		public void LineDeleted()
		{
			LineDeleted(Colour.Orange);
		}
		
		public void LineDeleted(Colour lineColour)
		{
			lineCounts.Increment(lineColour);
			
			Messenger<Colour, int>.Broadcast(Events.LineDeleted, lineColour, GetLineCountForColour(lineColour));
			GAHelper.LevelLineDeleted(lineColour);
		}
		
		public Colour GetNextColour()
		{
			if(playerSelectedNextColour.HasValue && CanDrawColour(playerSelectedNextColour.Value))
			{
				return playerSelectedNextColour.Value;
			}
			
			playerSelectedNextColour = null;
			
			if(lineCounts.OrangeLines > 0)
				playerSelectedNextColour = Colour.Orange;
			else if(lineCounts.BlueLines > 0)
				playerSelectedNextColour = Colour.Blue;
			else if(lineCounts.GreenLines > 0)
				playerSelectedNextColour = Colour.Green;
			else if(lineCounts.PurpleLines > 0)
				playerSelectedNextColour = Colour.Purple;
			else
				playerSelectedNextColour = Colour.None;
					
			return playerSelectedNextColour.Value;
		}
				
		public int GetLineCountForColour(Colour lineColour)
		{
			return lineCounts.GetLineCountForColour(lineColour);
		}
		
		public int GetLineCountForNextColour()
		{
			return GetLineCountForColour(GetNextColour());	
		}

		public int NumberOfColoursAvailableAtStart()
		{
			return lineCounts.NumberOfColoursAvailableAtStart();
		}
		
		public void OnColourSelected(Colour newColour, int remainingLineCount)
		{
			//Debug.Log("Colour selected = " + newColour);
			playerSelectedNextColour = newColour;
		}
		
		void OnEnable()
	    {
	        Messenger<Colour, int>.AddListener(Events.ColourSelected, OnColourSelected);
	    }
		
		void OnDisable()
	    {
	        Messenger<Colour, int>.RemoveListener(Events.ColourSelected, OnColourSelected);
	    }
		
		
		
		
		private bool CanDrawColour(Colour colour)
		{
			return lineCounts.IsLineCountGreaterThanZero(colour);
		}
	}
}


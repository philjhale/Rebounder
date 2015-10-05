using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class RemainingLinesButton : MonoBehaviour 
	{
		public Colour Colour;

		private UILabel countLabel; 
		private int remainingLineCount;
		private LineCountManager lineCountManager;
		private TweenPosition tweenPosition;

		void Start()
		{
			lineCountManager = FindObjectOfType<LineCountManager>();

			SetRemaining(lineCountManager.GetLineCountForColour(Colour));

			tweenPosition = (TweenPosition)GetComponent<TweenPosition>();

			if(lineCountManager.GetNextColour() == Colour)
				PlayForward();
		}


		void OnPress(bool isDown)
		{
			if(!isDown && Input.GetMouseButtonUp(0) && remainingLineCount > 0) 
			{
				// TODO Remove count, don't think it's needed
				// TODO Highlight button in some way, move possibly. Also move unselected button
				Messenger<Colour, int>.Broadcast(Events.ColourSelected, Colour, 0);
				PlayForward();
			}
		}

		public void SetRemaining(int count)
		{
			GetCountLabel().text = count.ToString();
			remainingLineCount = count;
		}

		private UILabel GetCountLabel()
		{
			if(countLabel == null) 
				countLabel = (UILabel)gameObject.GetComponentInChildren<UILabel>();

			return countLabel;
		}

		public void OnLineDrawnOrDeleted(Colour colour, int remainingLines)
		{
			// TODO Trigger selection of next button if lines = 0
			// TODO Make unselectable if lines = 0
			//contextInGameButton.SetColour(lineCountManager.GetNextColour());
			//contextInGameButton.SetCount(lineCountManager.GetLineCountForNextColour());
			if(colour == Colour)
			{
				SetRemaining(remainingLines);

				if(remainingLines == 0)
					PlayReverse();
			}

			if(lineCountManager.GetNextColour() == Colour)
				PlayForward();
		}

		public void OnColourSelected(Colour newColour, int remainingLineCount)
		{
			if(newColour != Colour)
				PlayReverse();
		}

		void OnEnable()
		{
			Messenger<Colour, int>.AddListener(Events.LineDrawn, OnLineDrawnOrDeleted);
			Messenger<Colour, int>.AddListener(Events.LineDeleted, OnLineDrawnOrDeleted);
			Messenger<Colour, int>.AddListener(Events.ColourSelected, OnColourSelected);
		}

		void OnDisable()
		{
			Messenger<Colour, int>.RemoveListener(Events.LineDrawn, OnLineDrawnOrDeleted);
			Messenger<Colour, int>.RemoveListener(Events.LineDeleted, OnLineDrawnOrDeleted);
			Messenger<Colour, int>.RemoveListener(Events.ColourSelected, OnColourSelected);
		}

		private void PlayForward()
		{
			if(CanAnimate())
				tweenPosition.PlayForward();
		}

		private void PlayReverse()
		{
			if(CanAnimate())
				tweenPosition.PlayReverse();
		}

		private bool CanAnimate()
		{
			return lineCountManager.NumberOfColoursAvailableAtStart() > 1;
		}
	}
}
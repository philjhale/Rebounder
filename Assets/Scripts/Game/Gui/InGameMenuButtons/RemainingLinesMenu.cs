using UnityEngine;
using System.Collections;
using System.Linq;

namespace Ph.Bouncer 
{
	public class RemainingLinesMenu : MonoBehaviour 
	{
		private RemainingLinesButton[] buttons;
		private LineCountManager lineCounts;

		void Start() 
		{
			buttons = (RemainingLinesButton[])GetComponentsInChildren<RemainingLinesButton>();
			lineCounts = (LineCountManager)FindObjectOfType<LineCountManager>();

			foreach(var button in buttons)
				SetButtonVisibilityForColour(button.Colour);
		}

		private void SetButtonVisibilityForColour(Colour colour)
		{
			if(lineCounts.GetLineCountForColour(colour) > 0)
				return;

			var button = buttons.Where(x => x.Colour == colour).Single();
			NGUITools.SetActive(button.gameObject, false);
		}
	}
}
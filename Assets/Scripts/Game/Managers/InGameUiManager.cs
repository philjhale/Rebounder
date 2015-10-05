using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class InGameUiManager : MonoBehaviour 
	{
		public UICamera InGameUiCamera;

		// Ensure no UI stuff happens while drawing a line
		void OnEnable()
		{
			Messenger.AddListener(Events.LineManipulationStarted, DisableUiCamera);
			Messenger.AddListener(Events.LineManipulationFinished, EnableUiCamera);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.LineManipulationStarted, DisableUiCamera);
			Messenger.RemoveListener(Events.LineManipulationFinished, EnableUiCamera);
		}

		private void EnableUiCamera()
		{
			InGameUiCamera.enabled = true;
		}

		private void DisableUiCamera()
		{
			InGameUiCamera.enabled = false;
		}
	}
}
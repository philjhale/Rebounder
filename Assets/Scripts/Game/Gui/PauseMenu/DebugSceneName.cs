using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class DebugSceneName : MonoBehaviour 
	{
		void Start() 
		{
			UILabel label = GetComponent<UILabel>();

			label.text = Application.loadedLevelName;
		}
	}
}
using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class HidePanelOnStart : MonoBehaviour 
	{
		// There MUST be a better way to do this
		void Start() 
		{
			NGUITools.SetActive(gameObject, false);	

			this.enabled = false;
		}
	}
}
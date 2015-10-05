using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class LineTouch : MonoBehaviour 
	{
		private BoxCollider touchCollider;

		void Start()
		{
			touchCollider = (BoxCollider)GetComponent<BoxCollider>();
		}

		public void Flicked()
		{
			touchCollider.enabled = false;
		}
	}
}
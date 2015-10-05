using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class KeyPressManager : MonoBehaviour 
	{
		void Update()
		{
			// This is so back button on Android quits
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
		}
	}
}
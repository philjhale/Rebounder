using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class SceneNumber : MonoBehaviour 
	{
		void Start() 
		{
			UILabel label = GetComponent<UILabel>();
			
			label.text = string.Format("Level {0}", CurrentLevel.GetNumber());
		}
	}
}
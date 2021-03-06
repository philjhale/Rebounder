﻿using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class LevelCompleteTitle : MonoBehaviour 
	{
		void Start() 
		{
			UILabel label = GetComponent<UILabel>();
			
			label.text = string.Format("Level {0} complete!", CurrentLevel.GetNumber());
		}
	}
}
using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class AllLevelsComplete : MonoBehaviour 
	{
		void Start()
		{
			GameSaveHelper.SetAllLevelsComplete(true);
		}
	}
}
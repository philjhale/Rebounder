using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class TitleAnimation : MonoBehaviour 
	{
		void Start() 
		{
			// NGUI animation movement must be multipled by UI root scale
			// http://www.tasharen.com/forum/index.php?topic=671.0
			iTween.MoveBy(gameObject, iTween.Hash("y", -400f * 0.003125f,
			                                       "time", 2f, 
			                                       "easetype", iTween.EaseType.easeOutBounce));

			enabled = false;
		}
	}
}